using System.Collections.Generic;
using System.Resources;
using System;
using Janus.Windows.UI.CommandBars;
using Janus.Windows.FilterEditor;
using Janus.Windows.GridEX;
using System.Windows.Forms;
using SVNMonitor.Logging;
using SVNMonitor.View.Interfaces;
using SVNMonitor.Wizards;
using Janus.Windows.UI;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.Helpers
{
public static class UIHelper
{
	private static Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>> clipboardCommands;

	private static ResourceManager filterEditorResourceManager;

	private static ResourceManager gridEXResourceManager;

	private static Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>> ClipboardCommands
	{
		get
		{
			if (UIHelper.clipboardCommands == null)
			{
				UIHelper.clipboardCommands = new Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>>();
			}
			return UIHelper.clipboardCommands;
		}
	}

	private static ResourceManager FilterEditorResourceManager
	{
		get
		{
			if (UIHelper.filterEditorResourceManager == null)
			{
				string resourceName = "SVNMonitor.Resources.UI.FilterEditor";
				UIHelper.filterEditorResourceManager = new ResourceManager(resourceName, typeof(UIHelper).Assembly);
			}
			return UIHelper.filterEditorResourceManager;
		}
	}

	private static ResourceManager GridEXResourceManager
	{
		get
		{
			if (UIHelper.gridEXResourceManager == null)
			{
				string resourceName = "SVNMonitor.Resources.UI.GridEX";
				UIHelper.gridEXResourceManager = new ResourceManager(resourceName, typeof(UIHelper).Assembly);
			}
			return UIHelper.gridEXResourceManager;
		}
	}

	public static void AddCopyCommand(UICommand cmd, GetStringDelegate action)
	{
		UICommandManager manager = cmd.CommandManager;
		if (!UIHelper.ClipboardCommands.ContainsKey(manager))
		{
			UIHelper.ClipboardCommands.Add(manager, new Dictionary<string, CopyCommandInfo>());
		}
		CopyCommandInfo copyCommandInfo.set_Action(action).Item = copyCommandInfo.set_Text(cmd.Text);
		cmd.Click += new CommandEventHandler(null.UIHelper.CmdCopy_Click);
	}

	private static void ApplyFilterEditorBuiltInTexts(FilterEditor filterEditor)
	{
		foreach (BuiltInText enumValue in Enum.GetValues(typeof(BuiltInText)))
		{
			string text = UIHelper.GetString(UIHelper.FilterEditorResourceManager, enumValue.ToString());
			filterEditor.BuiltInTextList.Item = enumValue;
		}
	}

	private static void ApplyGridBuiltInTexts(GridEX grid)
	{
		foreach (GridEXBuiltInText enumValue in Enum.GetValues(typeof(GridEXBuiltInText)))
		{
			string text = UIHelper.GetString(UIHelper.GridEXResourceManager, enumValue.ToString());
			grid.BuiltInTexts.Item = enumValue;
		}
	}

	private static void ApplyGridColumnCaptions(GridEX grid, Control parent)
	{
		ResourceManager resourceManager = UIHelper.GetResourceManager(parent);
		if (resourceManager == null)
		{
			return;
		}
		UIHelper.ApplyGridColumnCaptions(resourceManager, grid.RootTable);
	}

	private static void ApplyGridColumnCaptions(ResourceManager resourceManager, GridEXTable table)
	{
		foreach (GridEXColumn col in table.Columns)
		{
			string name = string.Format("{0}.{1}", table.Key, col.Key);
			string text = UIHelper.GetString(resourceManager, name, col.Key);
			if (!string.IsNullOrEmpty(text))
			{
				col.Caption = text;
			}
		}
		foreach (GridEXTable childTable in table.ChildTables)
		{
			UIHelper.ApplyGridColumnCaptions(resourceManager, childTable);
		}
	}

	private static void ApplyResource(UICommand cmd, ResourceManager resourceManager, bool showAmps)
	{
		string key = cmd.Key;
		string text = UIHelper.GetString(resourceManager, key);
		if (!string.IsNullOrEmpty(text))
		{
			UIHelper.SetCommandText(cmd, text, showAmps);
			UIHelper.SetToolTipText(cmd, text);
			return;
		}
		text = UIHelper.GetString(resourceManager, string.Format("{0}.Text", key), key);
		if (!string.IsNullOrEmpty(text))
		{
			UIHelper.SetCommandText(cmd, text, showAmps);
		}
		string toolTipText = UIHelper.GetString(resourceManager, string.Format("{0}.ToolTipText", key), key);
		if (!string.IsNullOrEmpty(toolTipText))
		{
			UIHelper.SetToolTipText(cmd, toolTipText);
		}
		if (string.IsNullOrEmpty(cmd.ToolTipText))
		{
			UIHelper.SetToolTipText(cmd, cmd.Text);
		}
	}

	public static void ApplyResources(UICommandManager commandManager)
	{
		UIHelper.ApplyResources(commandManager, false);
	}

	public static void ApplyResources(UICommandManager commandManager, bool showAmpsInToolbars)
	{
		ResourceManager resourceManager = UIHelper.GetResourceManager(commandManager.ContainerControl);
		if (resourceManager == null)
		{
			return;
		}
		foreach (UICommandBar bar in commandManager.CommandBars)
		{
			if (!showAmpsInToolbars)
			{
				showAmpsInToolbars = bar.CommandBarType == 1;
			}
			UIHelper.ApplyResources(bar.Commands, resourceManager, showAmpsInToolbars);
		}
		foreach (UIContextMenu menu in commandManager.ContextMenus)
		{
			UIHelper.ApplyResources(menu.Commands, resourceManager, true);
		}
	}

	public static void ApplyResources(UICommandCollection commands, ResourceManager resourceManager, bool showAmpsInToolbars)
	{
		if (commands == null || commands.Count == 0)
		{
			return;
		}
		foreach (UICommand cmd in commands)
		{
			UIHelper.ApplyResources(cmd.Commands, resourceManager, showAmpsInToolbars);
			UIHelper.ApplyResource(cmd, resourceManager, showAmpsInToolbars);
		}
	}

	public static void ApplyResources(FilterEditor filterEditor)
	{
		UIHelper.ApplyFilterEditorBuiltInTexts(filterEditor);
	}

	public static void ApplyResources(GridEX grid)
	{
		UIHelper.ApplyResources(grid, grid.Parent);
	}

	public static void ApplyResources(GridEX grid, Control parent)
	{
		UIHelper.ApplyGridBuiltInTexts(grid);
		UIHelper.ApplyGridColumnCaptions(grid, parent);
	}

	private static void CmdCopy_Click(object sender, CommandEventArgs e)
	{
		Logger.LogUserAction();
		GetStringDelegate action = UIHelper.ClipboardCommands[e.Command.CommandManager][e.Command.Key].Action;
		if (action != null)
		{
			string text = action();
			ClipboardHelper.SetText(text);
		}
	}

	private static CommandEventHandler CreateCommandEventHandler<T>(ISelectableView<T> view, UIContextMenu contextMenu, UserTypeInfo userTypeInfo, GetBaseName getBaseName, string tableName, string columnName)
	{
		T item = view.SelectedItem;
		if (item == null)
		{
			return;
		}
		contextMenu.Close();
		Wizard wizard = (Wizard)Activator.CreateInstance(userTypeInfo.Type);
		string baseName = getBaseName(item);
		wizard.Run(baseName, tableName, columnName);
	}

	private static ResourceManager GetResourceManager(Control parentControl)
	{
		string parentName = parentControl.GetType().Name;
		string resourceName = string.Format("SVNMonitor.Resources.UI.{0}", parentName);
		ResourceManager resourceManager = new ResourceManager(resourceName, typeof(UIHelper).Assembly);
		if (resourceManager != null)
		{
			resourceManager.GetObject(string.Empty);
			return null;
		}
		try
		{
		}
		catch (MissingManifestResourceException)
		{
		}
		return resourceManager;
	}

	public static string GetString(ResourceManager resourceManager, string name)
	{
		string retString = UIHelper.GetString(resourceManager, name, name);
		return retString;
	}

	public static string GetString(ResourceManager resourceManager, string name, string defaultIfNone)
	{
		string retString = defaultIfNone;
		try
		{
			return resourceManager.GetString(name);
		}
		catch (Exception)
		{
			Logger.Log.DebugFormat("Missing resource for {0}.{1}", resourceManager.BaseName, name);
		}
		return retString;
	}

	internal static void InitializeWizardsMenu<T>(ISelectableView<T> view, UIContextMenu contextMenu, UICommand menu, GetBaseName getBaseName, string tableName, string columnName)
	{
		menu.Commands.Clear();
		menu.Enabled = InheritableBoolean.False;
		List<UserTypeInfo> list = UserTypesFactory<Wizard>.GetAvailableUserTypes(TypeRequirements.NonCustom);
		list.Sort();
		foreach (UserTypeInfo item in list)
		{
			UserTypeInfo userTypeInfo = item;
			string key = string.Concat(menu.Key, item.Type.FullName);
			UICommand uICommand = new UICommand(key, item.DisplayName);
			uICommand.Image = menu.Image;
			UICommand command = uICommand;
			command.Click += UIHelper.CreateCommandEventHandler<T>(view, contextMenu, userTypeInfo, getBaseName, tableName, columnName);
			menu.Commands.Add(command);
		}
		UICommand separator = new UICommand();
		separator.CommandType = CommandType.Separator;
		menu.Commands.Add(separator);
		UICommand custom = new UICommand();
		custom.Key = string.Concat(menu.Key, "custom");
		custom.Text = Strings.UICommandCustom;
		UserTypeInfo userTypeInfo = new UserTypeInfo().add_Click(UIHelper.CreateCommandEventHandler<T>(userTypeInfo.DisplayName = Strings.UICommandCustom, userTypeInfo.Type = Type.GetTypeFromHandle(CustomWizard), userTypeInfo, getBaseName, tableName, columnName));
		menu.Commands.Add(custom);
	}

	public static bool IsCommandEnabled(UICommand cmd)
	{
		return cmd.Enabled == 1;
	}

	public static bool IsCommandVisible(UICommand cmd)
	{
		return cmd.Visible == 1;
	}

	public static void RefreshCopyCommands(UICommandCollection commands)
	{
		try
		{
			foreach (UICommand command in commands)
			{
				UICommandManager manager = command.CommandManager;
				if (UIHelper.ClipboardCommands.ContainsKey(manager) && UIHelper.ClipboardCommands[manager].ContainsKey(command.Key))
				{
					CopyCommandInfo copy = UIHelper.ClipboardCommands[manager][command.Key];
					string text = copy.Action();
					if (text.Length > 30)
					{
						text = string.Concat(text.Substring(0, 30), "...");
					}
					command.Text = copy.Text;
					if (!string.IsNullOrEmpty(text))
					{
						command.Text = string.Concat(command.Text, string.Format(" (\"{0}\")", text));
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Log.Error("Error setting clipboard menu.", ex);
		}
	}

	public static string RemoveAmps(string text)
	{
		Guid guid = Guid.NewGuid();
		string q = guid.ToString();
		text = text.Replace("&&", q);
		text = text.Replace("&", string.Empty);
		text = text.Replace(q, "&");
		return text;
	}

	public static void SetCommandEnabled(UICommand cmd, bool value)
	{
		cmd.Enabled = value.ToInheritableBoolean();
	}

	private static void SetCommandText(UICommand cmd, string text, bool showAmps)
	{
		if (showAmps)
		{
			cmd.Text = text;
		}
		else
		{
			cmd.Text = UIHelper.RemoveAmps(text);
		}
		if (cmd.BaseCommand != null)
		{
			cmd.BaseCommand.Text = UIHelper.RemoveAmps(text);
		}
	}

	public static void SetCommandVisible(UICommand cmd, bool value)
	{
		cmd.Visible = value.ToInheritableBoolean();
	}

	private static void SetToolTipText(UICommand cmd, string text)
	{
		cmd.ToolTipText = UIHelper.RemoveAmps(text);
		if (cmd.BaseCommand != null)
		{
			cmd.BaseCommand.ToolTipText = cmd.ToolTipText;
		}
	}

	private class CopyCommandInfo
	{
		public GetStringDelegate Action;

		public string Text;

		public CopyCommandInfo();
	}

	internal sealed class GetBaseName : MulticastDelegate
	{
		public GetBaseName(object object, IntPtr method);

		public virtual IAsyncResult BeginInvoke(object item, AsyncCallback callback, object object);

		public virtual string EndInvoke(IAsyncResult result);

		public virtual string Invoke(object item);
	}

	public sealed class GetStringDelegate : MulticastDelegate
	{
		public GetStringDelegate(object object, IntPtr method);

		public virtual IAsyncResult BeginInvoke(AsyncCallback callback, object object);

		public virtual string EndInvoke(IAsyncResult result);

		public virtual string Invoke();
	}
}
}