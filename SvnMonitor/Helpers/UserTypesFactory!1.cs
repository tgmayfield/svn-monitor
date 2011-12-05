namespace SVNMonitor.Helpers
{
    using SVNMonitor.Logging;
    using SVNMonitor.Resources;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    internal class UserTypesFactory<T>
    {
        private static readonly Dictionary<System.Type, string> userTypeDisplayNamesCache;

        static UserTypesFactory()
        {
            UserTypesFactory<T>.userTypeDisplayNamesCache = new Dictionary<System.Type, string>();
        }

        public static List<UserTypeInfo> GetAvailableUserTypes(TypeRequirements requirements)
        {
            List<UserTypeInfo> currentAssemblyUserTypes = UserTypesFactory<T>.GetAvailableUserTypes((Assembly) null, requirements);
            string[] moreAssemblies = new string[] { FileSystemHelper.AppData, Path.Combine(FileSystemHelper.AppData, "Addins"), Application.StartupPath, Path.Combine(Application.StartupPath, "Addins") };
            List<UserTypeInfo> localFolderUserTypes = UserTypesFactory<T>.GetAvailableUserTypes(moreAssemblies, requirements);
            currentAssemblyUserTypes.AddRange(localFolderUserTypes);
            return currentAssemblyUserTypes;
        }

        public static List<UserTypeInfo> GetAvailableUserTypes(string[] paths, TypeRequirements requirements)
        {
            List<UserTypeInfo> list = new List<UserTypeInfo>();
            foreach (string path in paths)
            {
                if (FileSystemHelper.DirectoryExists(path))
                {
                    List<UserTypeInfo> newList = UserTypesFactory<T>.GetAvailableUserTypes(path, requirements);
                    list.AddRange(newList);
                }
            }
            return list;
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
                System.Type[] types = assembly.GetTypes();
                Logger.Log.DebugFormat("types={0}", types.Length);
                foreach (System.Type type in types)
                {
                    bool isInterface = type.IsInterface;
                    bool isAbstract = type.IsAbstract;
                    bool isT = typeof(T).IsAssignableFrom(type);
                    bool hasEmptyCtor = null != type.GetConstructor(new System.Type[0]);
                    bool isSerializable = type.IsSerializable;
                    bool isCustom = SVNMonitor.Helpers.CustomAttribute.IsCustom(type);
                    if ((((!isInterface && !isAbstract) && (isT && hasEmptyCtor)) && (!EnumHelper.IsFlagged(requirements, TypeRequirements.Serializable) || isSerializable)) && (!EnumHelper.IsFlagged(requirements, TypeRequirements.NonCustom) || !isCustom))
                    {
                        string displayName = UserTypesFactory<T>.GetUserTypeDisplayName(type);
                        Logger.Log.DebugFormat("displayName={0}", displayName);
                        UserTypeInfo <>g__initLocal0 = new UserTypeInfo {
                            Type = type,
                            DisplayName = displayName
                        };
                        list.Add(<>g__initLocal0);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(string.Format("Error trying to load types from {0}", assembly), ex);
            }
            return list;
        }

        public static List<UserTypeInfo> GetAvailableUserTypes(string path, TypeRequirements requirements)
        {
            Logger.Log.DebugFormat("path={0}", path);
            List<UserTypeInfo> list = new List<UserTypeInfo>();
            string[] dllFiles = FileSystemHelper.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);
            Logger.Log.DebugFormat("dllFiles={0}", dllFiles.Length);
            foreach (string dll in dllFiles)
            {
                if (UserTypesFactory<T>.IgnoreFile(dll))
                {
                    Logger.Log.DebugFormat("Ignoring {0}", dll);
                }
                else
                {
                    try
                    {
                        List<UserTypeInfo> newList = UserTypesFactory<T>.GetAvailableUserTypes(Assembly.LoadFile(dll), requirements);
                        list.AddRange(newList);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log.Error(string.Format("Can't load dll: {0}", dll), ex);
                    }
                }
            }
            return list;
        }

        public static string GetUserTypeDisplayName(System.Type userType)
        {
            if (UserTypesFactory<T>.userTypeDisplayNamesCache.ContainsKey(userType))
            {
                return UserTypesFactory<T>.userTypeDisplayNamesCache[userType];
            }
            string displayName = userType.Name;
            ResourceProviderAttribute attribute = (ResourceProviderAttribute) Attribute.GetCustomAttribute(userType, typeof(ResourceProviderAttribute));
            if (attribute != null)
            {
                object resourceManagerObject = attribute.ResourceType.InvokeMember("ResourceManager", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Static, null, null, null);
                object resourceTextObject = resourceManagerObject.GetType().InvokeMember("GetString", BindingFlags.InvokeMethod, null, resourceManagerObject, new object[] { attribute.ResourceName });
                if (resourceTextObject != null)
                {
                    displayName = (string) resourceTextObject;
                }
                else
                {
                    displayName = attribute.ResourceName;
                }
            }
            UserTypesFactory<T>.userTypeDisplayNamesCache.Add(userType, displayName);
            return displayName;
        }

        public static string GetUserTypeDisplayName(T userTypeItem)
        {
            return UserTypesFactory<T>.GetUserTypeDisplayName(userTypeItem.GetType());
        }

        private static bool IgnoreFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            if ((!fileInfo.Name.StartsWith("Janus", StringComparison.InvariantCultureIgnoreCase) && !fileInfo.Name.StartsWith("log4net", StringComparison.InvariantCultureIgnoreCase)) && !fileInfo.Name.StartsWith("SharpSvn", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }
    }
}

