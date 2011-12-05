namespace SVNMonitor.Helpers
{
    using Janus.Windows.FilterEditor;
    using Janus.Windows.GridEX;
    using Janus.Windows.UI;
    using Janus.Windows.UI.CommandBars;
    using SVNMonitor.Logging;
    using SVNMonitor.Resources.Text;
    using SVNMonitor.View.Interfaces;
    using SVNMonitor.Wizards;
    using System;
    using System.Collections.Generic;
    using System.Resources;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public static class UIHelper
    {
        private static Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>> clipboardCommands;
        private static ResourceManager filterEditorResourceManager;
        private static ResourceManager gridEXResourceManager;

        public static void AddCopyCommand(UICommand cmd, GetStringDelegate action)
        {
            UICommandManager manager = cmd.CommandManager;
            if (!ClipboardCommands.ContainsKey(manager))
            {
                ClipboardCommands.Add(manager, new Dictionary<string, CopyCommandInfo>());
            }
            CopyCommandInfo tempLocal5 = new CopyCommandInfo {
                Action = action,
                Text = cmd.Text
            };
            ClipboardCommands[manager][cmd.Key] = tempLocal5;
            cmd.Click += new CommandEventHandler(UIHelper.CmdCopy_Click);
        }

        private static void ApplyFilterEditorBuiltInTexts(Janus.Windows.FilterEditor.FilterEditor filterEditor)
        {
            foreach (BuiltInText enumValue in Enum.GetValues(typeof(BuiltInText)))
            {
                string text = GetString(FilterEditorResourceManager, enumValue.ToString());
                filterEditor.BuiltInTextList[enumValue] = text;
            }
        }

        private static void ApplyGridBuiltInTexts(Janus.Windows.GridEX.GridEX grid)
        {
            foreach (GridEXBuiltInText enumValue in Enum.GetValues(typeof(GridEXBuiltInText)))
            {
                string text = GetString(GridEXResourceManager, enumValue.ToString());
                grid.BuiltInTexts[enumValue] = text;
            }
        }

        private static void ApplyGridColumnCaptions(Janus.Windows.GridEX.GridEX grid, Control parent)
        {
            ResourceManager resourceManager = GetResourceManager(parent);
            if (resourceManager != null)
            {
                ApplyGridColumnCaptions(resourceManager, grid.RootTable);
            }
        }

        private static void ApplyGridColumnCaptions(ResourceManager resourceManager, GridEXTable table)
        {
            foreach (GridEXColumn col in table.Columns)
            {
                string name = string.Format("{0}.{1}", table.Key, col.Key);
                string text = GetString(resourceManager, name, col.Key);
                if (!string.IsNullOrEmpty(text))
                {
                    col.Caption = text;
                }
            }
            foreach (GridEXTable childTable in table.ChildTables)
            {
                ApplyGridColumnCaptions(resourceManager, childTable);
            }
        }

        private static void ApplyResource(UICommand cmd, ResourceManager resourceManager, bool showAmps)
        {
            string key = cmd.Key;
            string text = GetString(resourceManager, key);
            if (!string.IsNullOrEmpty(text))
            {
                SetCommandText(cmd, text, showAmps);
                SetToolTipText(cmd, text);
            }
            else
            {
                text = GetString(resourceManager, string.Format("{0}.Text", key), key);
                if (!string.IsNullOrEmpty(text))
                {
                    SetCommandText(cmd, text, showAmps);
                }
                string toolTipText = GetString(resourceManager, string.Format("{0}.ToolTipText", key), key);
                if (!string.IsNullOrEmpty(toolTipText))
                {
                    SetToolTipText(cmd, toolTipText);
                }
                if (string.IsNullOrEmpty(cmd.ToolTipText))
                {
                    SetToolTipText(cmd, cmd.Text);
                }
            }
        }

        public static void ApplyResources(Janus.Windows.FilterEditor.FilterEditor filterEditor)
        {
            ApplyFilterEditorBuiltInTexts(filterEditor);
        }

        public static void ApplyResources(Janus.Windows.GridEX.GridEX grid)
        {
            ApplyResources(grid, grid.Parent);
        }

        public static void ApplyResources(UICommandManager commandManager)
        {
            ApplyResources(commandManager, false);
        }

        public static void ApplyResources(Janus.Windows.GridEX.GridEX grid, Control parent)
        {
            ApplyGridBuiltInTexts(grid);
            ApplyGridColumnCaptions(grid, parent);
        }

        public static void ApplyResources(UICommandManager commandManager, bool showAmpsInToolbars)
        {
            ResourceManager resourceManager = GetResourceManager(commandManager.ContainerControl);
            if (resourceManager != null)
            {
                foreach (UICommandBar bar in commandManager.CommandBars)
                {
                    if (!showAmpsInToolbars)
                    {
                        showAmpsInToolbars = bar.CommandBarType == CommandBarType.Menu;
                    }
                    ApplyResources(bar.Commands, resourceManager, showAmpsInToolbars);
                }
                foreach (UIContextMenu menu in commandManager.ContextMenus)
                {
                    ApplyResources(menu.Commands, resourceManager, true);
                }
            }
        }

        public static void ApplyResources(UICommandCollection commands, ResourceManager resourceManager, bool showAmpsInToolbars)
        {
            if ((commands != null) && (commands.Count != 0))
            {
                foreach (UICommand cmd in commands)
                {
                    ApplyResources(cmd.Commands, resourceManager, showAmpsInToolbars);
                    ApplyResource(cmd, resourceManager, showAmpsInToolbars);
                }
            }
        }

        private static void CmdCopy_Click(object sender, CommandEventArgs e)
        {
            Logger.LogUserAction();
            GetStringDelegate action = ClipboardCommands[e.Command.CommandManager][e.Command.Key].Action;
            if (action != null)
            {
                ClipboardHelper.SetText(action());
            }
        }

        private static CommandEventHandler CreateCommandEventHandler<T>(ISelectableView<T> view, UIContextMenu contextMenu, UserTypeInfo userTypeInfo, GetBaseName getBaseName, string tableName, string columnName)
        {
            return delegate {
                T item = view.SelectedItem;
                if (item != null)
                {
                    contextMenu.Close();
                    Wizard wizard = (Wizard) Activator.CreateInstance(userTypeInfo.Type);
                    string baseName = getBaseName(item);
                    wizard.Run(baseName, tableName, columnName);
                }
            };
        }

        private static ResourceManager GetResourceManager(Control parentControl)
        {
            string parentName = parentControl.GetType().Name;
            ResourceManager resourceManager = new ResourceManager(string.Format("SVNMonitor.Resources.UI.{0}", parentName), typeof(UIHelper).Assembly);
            if (resourceManager != null)
            {
                try
                {
// ReSharper disable ResourceItemNotResolved
                    resourceManager.GetObject(string.Empty);
// ReSharper restore ResourceItemNotResolved
                }
                catch (MissingManifestResourceException)
                {
                    resourceManager = null;
                }
            }
            return resourceManager;
        }

        public static string GetString(ResourceManager resourceManager, string name)
        {
            return GetString(resourceManager, name, name);
        }

        public static string GetString(ResourceManager resourceManager, string name, string defaultIfNone)
        {
            string retString = defaultIfNone;
            try
            {
                retString = resourceManager.GetString(name);
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
            menu.Enabled = Janus.Windows.UI.InheritableBoolean.False;
            List<UserTypeInfo> list = UserTypesFactory<Wizard>.GetAvailableUserTypes(TypeRequirements.NonCustom);
            list.Sort();
            foreach (UserTypeInfo item in list)
            {
                UserTypeInfo userTypeInfo = item;
                UICommand tempLocal0 = new UICommand(menu.Key + item.Type.FullName, item.DisplayName) {
                    Image = menu.Image
                };
                UICommand command = tempLocal0;
                command.Click += CreateCommandEventHandler<T>(view, contextMenu, userTypeInfo, getBaseName, tableName, columnName);
                menu.Commands.Add(command);
            }
            UICommand separator = new UICommand {
                CommandType = CommandType.Separator
            };
            menu.Commands.Add(separator);
            UICommand custom = new UICommand {
                Key = menu.Key + "custom",
                Text = Strings.UICommandCustom
            };
            UserTypeInfo tempLocal1 = new UserTypeInfo {
                DisplayName = Strings.UICommandCustom,
                Type = typeof(CustomWizard)
            };
            custom.Click += CreateCommandEventHandler<T>(view, contextMenu, tempLocal1, getBaseName, tableName, columnName);
            menu.Commands.Add(custom);
        }

        public static bool IsCommandEnabled(UICommand cmd)
        {
            return (cmd.Enabled == Janus.Windows.UI.InheritableBoolean.True);
        }

        public static bool IsCommandVisible(UICommand cmd)
        {
            return (cmd.Visible == Janus.Windows.UI.InheritableBoolean.True);
        }

        public static void RefreshCopyCommands(UICommandCollection commands)
        {
            try
            {
                foreach (UICommand command in commands)
                {
                    UICommandManager manager = command.CommandManager;
                    if (ClipboardCommands.ContainsKey(manager) && ClipboardCommands[manager].ContainsKey(command.Key))
                    {
                        CopyCommandInfo copy = ClipboardCommands[manager][command.Key];
                        string text = copy.Action();
                        if (text.Length > 30)
                        {
                            text = text.Substring(0, 30) + "...";
                        }
                        command.Text = copy.Text;
                        if (!string.IsNullOrEmpty(text))
                        {
                            command.Text = command.Text + string.Format(" (\"{0}\")", text);
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
            string q = Guid.NewGuid().ToString();
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
                cmd.Text = RemoveAmps(text);
            }
            if (cmd.BaseCommand != null)
            {
                cmd.BaseCommand.Text = RemoveAmps(text);
            }
        }

        public static void SetCommandVisible(UICommand cmd, bool value)
        {
            cmd.Visible = value.ToInheritableBoolean();
        }

        private static void SetToolTipText(UICommand cmd, string text)
        {
            cmd.ToolTipText = RemoveAmps(text);
            if (cmd.BaseCommand != null)
            {
                cmd.BaseCommand.ToolTipText = cmd.ToolTipText;
            }
        }

        private static Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>> ClipboardCommands
        {
            get
            {
                if (clipboardCommands == null)
                {
                    clipboardCommands = new Dictionary<UICommandManager, Dictionary<string, CopyCommandInfo>>();
                }
                return clipboardCommands;
            }
        }

        private static ResourceManager FilterEditorResourceManager
        {
            get
            {
                if (filterEditorResourceManager == null)
                {
                    string resourceName = "SVNMonitor.Resources.UI.FilterEditor";
                    filterEditorResourceManager = new ResourceManager(resourceName, typeof(UIHelper).Assembly);
                }
                return filterEditorResourceManager;
            }
        }

        private static ResourceManager GridEXResourceManager
        {
            get
            {
                if (gridEXResourceManager == null)
                {
                    string resourceName = "SVNMonitor.Resources.UI.GridEX";
                    gridEXResourceManager = new ResourceManager(resourceName, typeof(UIHelper).Assembly);
                }
                return gridEXResourceManager;
            }
        }

        private class CopyCommandInfo
        {
            public UIHelper.GetStringDelegate Action { get; set; }

            public string Text { get; set; }
        }

        internal delegate string GetBaseName(object item);

        public delegate string GetStringDelegate();
    }
}

