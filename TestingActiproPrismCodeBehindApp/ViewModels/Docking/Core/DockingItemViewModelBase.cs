using ActiproSoftware.Windows;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TestingActiproPrismRaftingWindow.ViewModels.Docking.Core
{
    public abstract class DockingItemViewModelBase : BindableBase
    {

        private string description;
        private ImageSource imageSource;
        private bool isActive;
        private bool isOpen;
        private bool isSelected;
        private string serializationId;
        private string title;
        private string windowGroupName;

        public string Description
        {
            get
            {
                return this.description;
            }
            set { SetProperty(ref description, value); }
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set { SetProperty(ref imageSource, value); }
        }

        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set { SetProperty(ref isActive, value); }
        }

        public bool IsOpen
        {
            get
            {
                return this.isOpen;
            }
            set { SetProperty(ref isOpen, value); }
        }

        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }
            set { SetProperty(ref isSelected, value); }
        }

        public abstract bool IsTool { get; }

        public string SerializationId
        {
            get
            {
                return this.serializationId;
            }
            set { SetProperty(ref serializationId, value); }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set { SetProperty(ref title, value); }
        }

        public string WindowGroupName
        {
            get
            {
                return this.windowGroupName;
            }
            set { SetProperty(ref windowGroupName, value); }
        }

    }
}
