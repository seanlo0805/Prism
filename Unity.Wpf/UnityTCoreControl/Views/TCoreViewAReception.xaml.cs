﻿using EventPlatform;
using System.Windows.Controls;
using UnityTCoreControl.ViewModels;

namespace UnityTCoreControl.Views
{
    /// <summary>
    /// Interaction logic for TCoreViewAReception
    /// </summary>
    public partial class TCoreViewAReception : UserControl
    {
        public TCoreViewAReception()
        {
            InitializeComponent();
            this.DataContext = new TCoreViewAReceptionViewModel(EventHandlerFactory<PrismEventAggregator>.FetchEventHandler("tcore"));
        }
    }
}
