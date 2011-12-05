using System.Collections.Generic;
using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using SVNMonitor.Logging;
using SVNMonitor.Resources;

namespace SVNMonitor.Helpers
{
internal class UserTypesFactory<T>
{
	private readonly static Dictionary<Type, string> userTypeDisplayNamesCache;

	static UserTypesFactory()
	{
		UserTypesFactory<T>.userTypeDisplayNamesCache = new Dictionary<Type, string>();
	}

	public UserTypesFactory()
	{
	}

	public static List<UserTypeInfo> GetAvailableUserTypes(TypeRequirements requirements)
	{
		List<UserTypeInfo> currentAssemblyUserTypes = UserTypesFactory<T>.GetAvailableUserTypes(null, requirements);
		string[] appData = new string[4];
		appData[0] = FileSystemHelper.AppData;
		appData[1] = Path.Combine(FileSystemHelper.AppData, "Addins");
		appData[2] = Application.StartupPath;
		appData[3] = Path.Combine(Application.StartupPath, "Addins");
		string[] moreAssemblies = appData;
		List<UserTypeInfo> localFolderUserTypes = UserTypesFactory<T>.GetAvailableUserTypes(moreAssemblies, requirements);
		currentAssemblyUserTypes.AddRange(localFolderUserTypes);
		return currentAssemblyUserTypes;
	}

	public static List<UserTypeInfo> GetAvailableUserTypes(Assembly assembly, TypeRequirements requirements)
	{
		if (assembly == null)
		{
			assembly = typeof(T).Assembly;
		}
		Logger.Log.DebugFormat("Assembly={0}", assembly.FullName);
		Logger.Log.DebugFormat("TypeRequirements={0}", requirements);
		List<UserTypeInfo> list = new List<UserTypeInfo>();
		try
		{
			Type[] types = assembly.GetTypes();
			Logger.Log.DebugFormat("types={0}", (int)types.Length);
			Type[] typeArray = types;
			foreach (Type type in typeArray)
			{
				bool isAbstract = type.IsAbstract;
				bool hasEmptyCtor = null != type.GetConstructor(new Type[0]);
				bool isCustom = CustomAttribute.IsCustom(type);
				if (!type.IsInterface)
				{
					if (!type.IsAbstract)
					{
						if (typeof(T).IsAssignableFrom(type))
						{
							if (null != type.GetConstructor(new Type[0]))
							{
								if ((EnumHelper.IsFlagged(requirements, 1) || type.IsSerializable) && (EnumHelper.IsFlagged(requirements, 2) || !CustomAttribute.IsCustom(type)))
								{
									string displayName = UserTypesFactory<T>.GetUserTypeDisplayName(type);
									Logger.Log.DebugFormat("displayName={0}", displayName);
									UserTypeInfo userTypeInfo.DisplayName = displayName.Add(userTypeInfo);
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error(string.Format("Error trying to load types from {0}", assembly), ex);
		}
		return list;
	}

	public static List<UserTypeInfo> GetAvailableUserTypes(string[] paths, TypeRequirements requirements)
	{
		List<UserTypeInfo> list = new List<UserTypeInfo>();
		string[] strArrays = paths;
		foreach (string path in strArrays)
		{
			if (FileSystemHelper.DirectoryExists(path))
			{
				List<UserTypeInfo> newList = UserTypesFactory<T>.GetAvailableUserTypes(path, requirements);
				list.AddRange(newList);
			}
		}
		return list;
	}

	public static List<UserTypeInfo> GetAvailableUserTypes(string path, TypeRequirements requirements)
	{
		Logger.Log.DebugFormat("path={0}", path);
		List<UserTypeInfo> list = new List<UserTypeInfo>();
		string[] dllFiles = FileSystemHelper.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
		Logger.Log.DebugFormat("dllFiles={0}", (int)dllFiles.Length);
		string[] strArrays = dllFiles;
		foreach (string dll in strArrays)
		{
			if (UserTypesFactory<T>.IgnoreFile(dll))
			{
				Logger.Log.DebugFormat("Ignoring {0}", dll);
				continue;
			}
			try
			{
				Assembly assembly = Assembly.LoadFile(dll);
				List<UserTypeInfo> newList = UserTypesFactory<T>.GetAvailableUserTypes(assembly, requirements);
				list.AddRange(newList);
			}
			catch (Exception ex)
			{
				Logger.Log.Error(string.Format("Can't load dll: {0}", dll), ex);
			}
		}
		return list;
	}

	public static string GetUserTypeDisplayName(T userTypeItem)
	{
		string displayName = UserTypesFactory<T>.GetUserTypeDisplayName(userTypeItem.GetType());
		return displayName;
	}

	public static string GetUserTypeDisplayName(Type userType)
	{
		object[] objArray;
		if (UserTypesFactory<T>.userTypeDisplayNamesCache.ContainsKey(userType))
		{
			return UserTypesFactory<T>.userTypeDisplayNamesCache[userType];
		}
		string displayName = userType.Name;
		ResourceProviderAttribute attribute = (ResourceProviderAttribute)Attribute.GetCustomAttribute(userType, typeof(ResourceProviderAttribute));
		if (attribute != null)
		{
			object resourceManagerObject = attribute.ResourceType.InvokeMember("ResourceManager", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.GetProperty, null, null, null);
			object resourceTextObject = resourceManagerObject.GetType().InvokeMember("GetString", BindingFlags.InvokeMethod, null, resourceManagerObject, new object[] { attribute.ResourceName });
			if (resourceTextObject != null)
			{
				displayName = (string)resourceTextObject;
			}
			else
			{
				displayName = attribute.ResourceName;
			}
		}
		UserTypesFactory<T>.userTypeDisplayNamesCache.Add(userType, displayName);
		return displayName;
	}

	private static bool IgnoreFile(string file)
	{
		FileInfo fileInfo = new FileInfo(file);
		if (fileInfo.Name.StartsWith("Janus", StringComparison.InvariantCultureIgnoreCase) || fileInfo.Name.StartsWith("log4net", StringComparison.InvariantCultureIgnoreCase) || fileInfo.Name.StartsWith("SharpSvn", StringComparison.InvariantCultureIgnoreCase))
		{
			return true;
		}
		return false;
	}
}
}