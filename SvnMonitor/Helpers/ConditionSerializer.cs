namespace SVNMonitor.Helpers
{
    using Janus.Windows.Common.Layouts;
    using Janus.Windows.GridEX;
    using SVNMonitor.Logging;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class ConditionSerializer
    {
        private const string XmlRoot = "FilterCondition";

        public static GridEXFilterCondition Deserialize(ConditionSerializationContext context)
        {
            Logger.Log.DebugFormat("context={0}", context);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(context.ConditionXml);
            GridEXFilterCondition condition = (GridEXFilterCondition) JanusLayoutReader.FromParentNode(doc).GetValue("FilterCondition", typeof(GridEXFilterCondition));
            ColumnInfo key = context.NextColumnKey();
            condition.Column = Grid.Tables[key.TableKey].Columns[key.ColumnKey];
            SetConditionColumns(context, condition.Conditions, Grid);
            context.Reset();
            return condition;
        }

        public static ConditionSerializationContext Serialize(GridEXFilterCondition condition)
        {
            Logger.Log.DebugFormat("condition={0}", condition);
            JanusLayoutWriter writer = new JanusLayoutWriter();
            ((IJanusXmlLayoutsSupport) condition).Serialize(writer);
            ConditionSerializationContext context = new ConditionSerializationContext(string.Format("<{0}>{1}</{0}>", "FilterCondition", writer));
            if (condition.Column != null)
            {
                ColumnInfo tempLocal0 = new ColumnInfo {
                    ColumnKey = condition.Column.Key,
                    TableKey = condition.Column.Table.Key
                };
                ColumnInfo key = tempLocal0;
                context.AddColumnKey(key);
                SetContextColumns(context, condition.Conditions);
            }
            return context;
        }

        private static void SetConditionColumns(ConditionSerializationContext context, GridEXFilterConditionCollection conditions, Janus.Windows.GridEX.GridEX grid)
        {
            if (conditions != null)
            {
                Logger.Log.DebugFormat("conditions={0}", conditions.Count);
                foreach (GridEXFilterCondition condition in conditions)
                {
                    ColumnInfo key = context.NextColumnKey();
                    if (key != null)
                    {
                        condition.Column = grid.Tables[key.TableKey].Columns[key.ColumnKey];
                        SetConditionColumns(context, condition.Conditions, grid);
                    }
                }
            }
        }

        private static void SetContextColumns(ConditionSerializationContext context, GridEXFilterConditionCollection conditions)
        {
            Logger.Log.DebugFormat("conditions={0}", conditions.Count);
            foreach (GridEXFilterCondition condition in conditions)
            {
                if (condition.Column != null)
                {
                    ColumnInfo tempLocal1 = new ColumnInfo {
                        ColumnKey = condition.Column.Key,
                        TableKey = condition.Column.Table.Key
                    };
                    ColumnInfo key = tempLocal1;
                    context.AddColumnKey(key);
                }
                SetContextColumns(context, condition.Conditions);
            }
        }

		public static Janus.Windows.GridEX.GridEX Grid { get; set; }
    }
}

