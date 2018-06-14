using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Security;


namespace KPChange
{
    
    /// <summary>
    /// Class that handles global KeePass interaction like buttons in the Tools Menu 
    /// </summary>
    public class GlobalTools
    {
        private IPluginHost _host;
        private ChangerProvider _changerProvider;
        
        public GlobalTools(IPluginHost host, ChangerProvider changerProvider)
        {
            _host = host;
            _changerProvider = changerProvider;
            
            AddToolsItems();
        }

        private void AddToolsItems()
        {
            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection tsMenu = _host.MainWindow.ToolsMenu.DropDownItems;

            // Add menu item 'Do Something'
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Change all expired passwords";
            tsmi.Click += this.ChangeExpiredPasswords;
            tsMenu.Add(tsmi);
        }

        public void ChangeExpiredPasswords(Object sender=null, EventArgs e=null)
        {
            if (!_host.Database.IsOpen)
                return;

            HashSet<PwEntry> expEntries = FindExpiredEntries();

            foreach (var entry in expEntries)
            {
                _changerProvider.GetChanger(entry).ChangePassword(entry);
            }
            
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