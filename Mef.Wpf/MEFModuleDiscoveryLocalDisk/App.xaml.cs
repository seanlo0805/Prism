using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MEFModuleDiscoveryLocalDisk
{
    /// <summary>
    /// Example for Registering Model In Code
    /// 
    /// Interaction logic for App.xaml
    /// 
    /// 
    /// reference: https://www.dmcinfo.com/latest-thinking/blog/id/9298/a-properly-pleasing-primer-tutorial-for-wpf-prism--part-3-modules
    /// </summary>
    /// <summary>
    /// <para>Example for Registering Model In Code</para>
    /// <para>Interaction logic for App.xaml</para>
    /// 
    /// <para>When you created project, do more things:</para>
    /// <para>1)  Add Post-Build Event</para>
    /// <para>    IF EXIST $(TargetDir)Modules rmdir /s /q $(TargetDir)Modules</para>
    /// <para>    mkdir $(TargetDir) Modules</para>
    /// <para>    xcopy  $(SolutionDir) MEFTCoreControl\$(OutDir) MEFTCoreControl.dll  $(TargetDir) Modules /h/i/c/k/e/r/y</para>
    /// <para>2)  Build Dependencies. if you add reference MEFTCoreControls project, there were copied one more MEFTCoreControls.dll into $(TargetDir) </para>
    /// <para>    Add MEFTCoreControls</para>
    /// 
    /// <para>reference:  </para>
    /// <para>(1) https://www.dmcinfo.com/latest-thinking/blog/id/9298/a-properly-pleasing-primer-tutorial-for-wpf-prism--part-3-modules </para>
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MEFTCoreBootstrapper bs = new MEFTCoreBootstrapper();
            bs.Run();
        }
    }
}
