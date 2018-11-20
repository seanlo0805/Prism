using System;
using System.Windows.Controls;

namespace TestingActiproPrismRaftingWindow.Views.Docking
{
    /// <summary>
    /// Interaction logic for ViewB
    /// </summary>
    public partial class ViewB : UserControl
    {
        private static object _seqLocker = new object();
        private static int _seq_ = 0;

        private byte[] bigarray = new byte[256*1024 * 1024];
        private string content;
        Action<string> OnMsg;
        public ViewB(Action<string> onMsg)
        {
            content = GetKeyContent();
            OnMsg = onMsg;
            InitializeComponent();
            OnMsg(content + " ==> Add");
        }
        ~ViewB()
        {

            OnMsg(content + " ==> DEL");
        }
        
        string GetKeyContent()
        {
            //return "ViewB_" + Guid.NewGuid().ToString();
            return "ViewB_" + GenSeq();
        }

        static int GenSeq()
        {
            int seq = -1;
            lock(_seqLocker)
            {
                seq = _seq_++;
            }
            return seq;
        }
    }
}
