using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoChange.PasswordChangers;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Security;


namespace AutoChange
{
    
    /// <summary>
    /// High-Level functionality for performing different actions.
    /// </summary>
    public class ToolChain
    {
        private readonly AutoChangeExt _plugin;
        private readonly ToolUtils _utils;
        
        public ToolChain(AutoChangeExt plugin)
        {
            _plugin = plugin;
            _utils = new ToolUtils(plugin);
        }

        public void ChangeExpiredPasswords(Object sender=null, EventArgs e=null)
        {
            if (!_plugin.Host.Database.IsOpen)
                return;

            HashSet<PwEntry> expEntries = _utils.FindExpiredEntries();
            Dictionary<PwEntry, AbstractPasswordChanger> changers = _plugin.ChangerProvider.GetChangers(expEntries);
            
            foreach (var changer in changers.Values)
                changer.OnBegin();

            // Main change loop
            foreach (var item in changers)
            {
                _utils.GenerateNewPassword(item.Key);
                item.Value.ChangePassword(item.Key);
            }

            foreach (var changer in changers.Values)
                changer.OnEnd();
            
        }
        
    }

    internal class ToolUtils
    {
        
        private AutoChangeExt _plugin;

        public ToolUtils(AutoChangeExt plugin)
        {
            _plugin = plugin;
        }

        internal HashSet<PwEntry> FindExpiredEntries()
        {
            HashSet<PwEntry> result = new HashSet<PwEntry>();

            _plugin.Host.Database.RootGroup.TraverseTree(TraversalMethod.PreOrder, 
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

        internal void GenerateNewPassword(PwEntry pwEntry)
        {
            
        }
        
    }
    
}