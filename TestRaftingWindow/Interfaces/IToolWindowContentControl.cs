using ActiproSoftware.Windows.Controls.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRaftingWindow.Interfaces
{
    public interface IToolWindowContentControl
    {
        ToolWindow ParentWidnow{get;set;}

        /// <summary>
        /// 當視窗關閉時~~
        /// </summary>
        void OnWindowClose();
    }
}
