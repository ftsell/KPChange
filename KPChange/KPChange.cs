using System;
using System.Windows.Forms;
using KeePass.Plugins;

namespace KPChange
{
    public class KPChangeExt : Plugin
    {
        
        internal IPluginHost Host;
        internal ToolChain ToolChain;
        internal ChangerProvider ChangerProvider;
        
        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            Host = host;
            
            ChangerProvider = new ChangerProvider();
            ToolChain = new ToolChain(this);
            
            AddToolsItems();
            
            return base.Initialize(host);
        }
        
        private void AddToolsItems()
        {
            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection tsMenu = Host.MainWindow.ToolsMenu.DropDownItems;
            
            // Menu Item "Change all expired passwords"
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Change all expired passwords";
            tsmi.Click += ToolChain.ChangeExpiredPasswords;
            tsMenu.Add(tsmi);
        }

    }
}