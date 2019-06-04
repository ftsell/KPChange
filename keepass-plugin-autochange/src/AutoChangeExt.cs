using System;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePass.UI;

namespace AutoChange
{
    public class AutoChangeExt : Plugin
    {
        
        internal IPluginHost Host;
        internal ToolChain ToolChain;
        internal ChangerProvider ChangerProvider;
        internal UiManager Ui;
        
        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            Host = host;
            
            ChangerProvider = new ChangerProvider(this);
            ToolChain = new ToolChain(this);
            Ui = new UiManager(this);
            
            Ui.RegisterToolsItems();
            Ui.RegisterFormTabs();

            return true;
            return base.Initialize(host);
        }

    }
}