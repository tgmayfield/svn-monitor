namespace SVNMonitor.View.Controls
{
    using SVNMonitor.Entities;
    using SVNMonitor.Resources.Text;
    using System;
    using System.Runtime.CompilerServices;

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
            if (this.ChangesClick != null)
            {
                this.ChangesClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnConflictsClick()
        {
            if (this.ConflictsClick != null)
            {
                this.ConflictsClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnPathClick()
        {
            if (this.PathClick != null)
            {
                this.PathClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnSyncdClick()
        {
            if (this.SyncdClick != null)
            {
                this.SyncdClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnUnversionedClick()
        {
            if (this.UnversionedClick != null)
            {
                this.UnversionedClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnUpdatesClick()
        {
            if (this.UpdatesClick != null)
            {
                this.UpdatesClick(this, EventArgs.Empty);
            }
        }

        protected virtual void OnUrlClick()
        {
            if (this.UrlClick != null)
            {
                this.UrlClick(this, EventArgs.Empty);
            }
        }

        protected override string NoEntitiesString
        {
            get
            {
                return Strings.NoSourcesAreDefined;
            }
        }
    }
}

