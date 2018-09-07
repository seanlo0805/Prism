using MEFTCoreControl.View;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFTCoreControl
{
    /// <summary>
    /// 
    /// 
    /// reference : https://hk.saowen.com/a/9070f19d9e34c41ec7e1a8bdceb587b637937b3ad66d6d1ea8e4b742c414c30e
    /// </summary>
    [ModuleExport(typeof(TCoreControlA))]
    public class TCoreControlA : IModule
    {
        IRegionManager _regionManager;

        // 當Prism加載該模塊時，它將通過MEF實例化該類，MEF將注入一個Region Manager實例
        [ImportingConstructor]
        public TCoreControlA(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 該方法將為模塊啟動提供一個代碼入口點
        // 我們將把MEF容器裏的ViewA注入到MainWindow界面定義的MainRegion中
        public void Initialize()
        {
            IRegionManager manager = _regionManager.RegisterViewWithRegion("MainRegion", typeof(TCoreViewA));
        }
    }
}
