using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoChange.PasswordChangers;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;

namespace AutoChange
{
    public class AutoChangeExt : Plugin
    {
        
        internal IPluginHost Host;
        internal ChangerProvider ChangerProvider;
        internal UiManager Ui;

        private DbHelper _dbHelper;
        
        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            Host = host;
            
            ChangerProvider = new ChangerProvider(this);
            Ui = new UiManager(this);
            _dbHelper = new DbHelper(this);
            
            Ui.RegisterMenuItems();
            Ui.RegisterFormTabs();

            return true;
            return base.Initialize(host);
        }

        public void ChangeAllExpiredPasswords()
        {
            if (!Host.Database.IsOpen)
                return;

            HashSet<PwEntry> expEntries = _dbHelper.FindExpiredEntries();
            Dictionary<PwEntry, AbstractPasswordChanger> changers = ChangerProvider.GetChangers(expEntries);
            
            foreach (var changer in changers.Values)
                changer.OnBegin();

            // Main change loop
            foreach (var item in changers)
            {
                //_dbHelper.GenerateNewPassword(item.Key);
                item.Value.ChangePassword(item.Key);
            }

            foreach (var changer in changers.Values)
                changer.OnEnd();
        }

    }
}