﻿using Janus.Windows.EditControls;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using SVNMonitor.Helpers;

namespace SVNMonitor.View.Controls
{
internal class ActionSelector : UIComboBox, ISupportInitialize
{
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Type SelectedActionType
	{
		get
		{
			return (Type)base.SelectedValue;
		}
		set
		{
			base.SelectedItem = this.Items[value];
		}
	}

	public ActionSelector()
	{
		base.ComboStyle = ComboStyle.DropDownList;
	}

	public void BeginInit()
	{
	}

	public void EndInit()
	{
		if (!base.DesignMode)
		{
			this.SetActionTypes();
		}
	}

	protected virtual void SetActionTypes()
	{
		base.Items.Clear();
		List<UserTypeInfo> list = UserTypesFactory<Action>.GetAvailableUserTypes(TypeRequirements.Serializable);
		list.Sort();
		foreach (UserTypeInfo item in list)
		{
			base.Items.Add(item.DisplayName, item.Type);
		}
	}
}
}