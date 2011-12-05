using System.ComponentModel.Design;
using System.Collections.Generic;

namespace SVNMonitor.Design
{
public class SoundsByAuthorsCollectionEditor : CollectionEditor
{
	public SoundsByAuthorsCollectionEditor() : base(typeof(List<SoundByAuthor>))
	{
	}

	protected override CollectionForm CreateCollectionForm()
	{
		CollectionForm form = base.CreateCollectionForm();
		form.Text = "Sounds-By-Authors Collection";
		return form;
	}
}
}