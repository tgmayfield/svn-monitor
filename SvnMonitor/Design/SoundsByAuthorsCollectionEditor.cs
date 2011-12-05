using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

using SVNMonitor.Actions;

namespace SVNMonitor.Design
{
	public class SoundsByAuthorsCollectionEditor : CollectionEditor
	{
		public SoundsByAuthorsCollectionEditor()
			: base(typeof(List<SoundByAuthor>))
		{
		}

		protected override CollectionEditor.CollectionForm CreateCollectionForm()
		{
			CollectionEditor.CollectionForm form = base.CreateCollectionForm();
			form.Text = "Sounds-By-Authors Collection";
			return form;
		}
	}
}