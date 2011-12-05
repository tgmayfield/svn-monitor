using System;
using Janus.Windows.GridEX;
using SVNMonitor.Logging;
using System.Xml;
using Janus.Windows.Common.Layouts;

namespace SVNMonitor.Helpers
{
internal class ConditionSerializer
{
	private const string XmlRoot = "FilterCondition";

	public static GridEX Grid
	{
		get
		{
			return ConditionSerializer.<Grid>k__BackingField;
		}
		set
		{
			value;
		}
	}

	public ConditionSerializer()
	{
	}

	public static GridEXFilterCondition Deserialize(ConditionSerializationContext context)
	{
		Logger.Log.DebugFormat("context={0}", context);
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(context.ConditionXml);
		JanusLayoutReader reader = JanusLayoutReader.FromParentNode(doc);
		GridEXFilterCondition condition = (GridEXFilterCondition)reader.GetValue("FilterCondition", typeof(GridEXFilterCondition));
		ColumnInfo key = context.NextColumnKey();
		condition.Column = ConditionSerializer.Grid.Tables[key.TableKey].Columns[key.ColumnKey];
		ConditionSerializer.SetConditionColumns(context, condition.Conditions, ConditionSerializer.Grid);
		context.Reset();
		return condition;
	}

	public static ConditionSerializationContext Serialize(GridEXFilterCondition condition)
	{
		Logger.Log.DebugFormat("condition={0}", condition);
		JanusLayoutWriter writer = new JanusLayoutWriter();
		condition.Serialize(writer);
		string xml = string.Format("<{0}>{1}</{0}>", "FilterCondition", writer);
		ConditionSerializationContext context = new ConditionSerializationContext(xml);
		if (condition.Column != null)
		{
			ColumnInfo columnInfo = new ColumnInfo();
			columnInfo.ColumnKey = condition.Column.Key;
			columnInfo.TableKey = condition.Column.Table.Key;
			ColumnInfo key = columnInfo;
			context.AddColumnKey(key);
			ConditionSerializer.SetContextColumns(context, condition.Conditions);
		}
		return context;
	}

	private static void SetConditionColumns(ConditionSerializationContext context, GridEXFilterConditionCollection conditions, GridEX grid)
	{
		if (conditions == null)
		{
			return;
		}
		Logger.Log.DebugFormat("conditions={0}", conditions.Count);
		foreach (GridEXFilterCondition condition in conditions)
		{
			ColumnInfo key = context.NextColumnKey();
			if (key != null)
			{
				condition.Column = grid.Tables[key.TableKey].Columns[key.ColumnKey];
				ConditionSerializer.SetConditionColumns(context, condition.Conditions, grid);
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
				ColumnInfo columnInfo = new ColumnInfo();
				columnInfo.ColumnKey = condition.Column.Key;
				columnInfo.TableKey = condition.Column.Table.Key;
				ColumnInfo key = columnInfo;
				context.AddColumnKey(key);
			}
			ConditionSerializer.SetContextColumns(context, condition.Conditions);
		}
	}
}
}