using EventPlatform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnityTCoreControl.ViewModels;

namespace UnityTCoreControl.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class TCoreViewA : UserControl
    {
        public TCoreViewA()
        {
            InitializeComponent();
            this.DataContext = new TCoreViewAViewModel( EventHandlerFactory<PrismEventAggregator>.FetchEventHandler("tcore"),
                                                        EventHandlerFactory<PrismEventAggregator>.FetchEventHandler("tcore1"));
        }
    }
}
