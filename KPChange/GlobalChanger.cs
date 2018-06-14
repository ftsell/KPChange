using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Security;


namespace KPChange
{
    public class GlobalChanger
    {
        private IPluginHost _host;
        
        public GlobalChanger(IPluginHost host)
        {
            _host = host;
            
            AddToolsItem();
        }

        private void AddToolsItem()
        {
            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection tsMenu = _host.MainWindow.ToolsMenu.DropDownItems;

            // Add menu item 'Do Something'
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Change all expired passwords";
            tsmi.Click += this.OnMenuClick;
            tsMenu.Add(tsmi);
        }

        private void OnMenuClick(Object sender, EventArgs e)
        {
            if (!_host.Database.IsOpen)
                return;

            HashSet<PwEntry> expEntries = FindExpiredEntries();
        }

        private HashSet<PwEntry> FindExpiredEntries()
        {
            HashSet<PwEntry> result = new HashSet<PwEntry>();

            _host.Database.RootGroup.TraverseTree(TraversalMethod.PreOrder, 
                // Traverse groups
                delegate(PwGroup pg)
                {
                    // TODO Write fancy logic to add all entries of an expired group
                    return true;
                }, 
                
                // Traverse single entries
                delegate(PwEntry pe)
                {
                    if (pe.Expires && pe.Strings.Get("Title").ReadString() != "<TAN>")
                    {
                        result.Add(pe);
                    }
    
                    return true;
                    
                }
            );
            
            return result;
        }
        
    }
}