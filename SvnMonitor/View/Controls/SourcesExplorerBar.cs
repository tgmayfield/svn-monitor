using System;

using SVNMonitor.Entities;
using SVNMonitor.Resources.Text;

namespace SVNMonitor.View.Controls
{
	public class SourcesExplorerBar : UserEntitiesExplorerBar<Source>
	{
		public event EventHandler ChangesClick;

		public event EventHandler ConflictsClick;

		public event EventHandler PathClick;

		public event EventHandler SyncdClick;

		public event EventHandler UnversionedClick;

		public event EventHandler UpdatesClick;

		public event EventHandler UrlClick;

		protected override UserEntityExplorerBarGroup<Source> GetEntityGroup(Source entity)
		{
			return new SourceExplorerBarGroup(entity);
		}

		protected virtual void OnChangesClick()
		{
			if (ChangesClick != null)
			{
				ChangesClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnConflictsClick()
		{
			if (ConflictsClick != null)
			{
				ConflictsClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnPathClick()
		{
			if (PathClick != null)
			{
				PathClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnSyncdClick()
		{
			if (SyncdClick != null)
			{
				SyncdClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnUnversionedClick()
		{
			if (UnversionedClick != null)
			{
				UnversionedClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnUpdatesClick()
		{
			if (UpdatesClick != null)
			{
				UpdatesClick(this, EventArgs.Empty);
			}
		}

		protected virtual void OnUrlClick()
		{
			if (UrlClick != null)
			{
				UrlClick(this, EventArgs.Empty);
			}
		}

		protected override string NoEntitiesString
		{
			get { return Strings.NoSourcesAreDefined; }
		}
	}
}