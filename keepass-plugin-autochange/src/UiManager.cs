using System;
using System.Windows.Forms;
using KeePass.Forms;
using KeePass.UI;
using KeePassLib;
using KeePassLib.Collections;

namespace AutoChange
{
    public class UiManager
    {

        private readonly AutoChangeExt _plugin;

        public UiManager(AutoChangeExt plugin)
        {
            _plugin = plugin;
        }

        public void OpenPasswordEditDialog()
        {
            //_plugin.Host.MainWindow.BeginInvoke((MethodInvoker) delegate
            //{
            //    
            //});

            //KeePass.Forms.PwEntryForm.FromHandle(new IntPtr(1));
            
        }

        internal void RegisterToolsItems()
        {
            // Get a reference to the 'Tools' menu item container
            ToolStripItemCollection tsMenu = _plugin.Host.MainWindow.ToolsMenu.DropDownItems;
            
            // Menu Item "Change all expired passwords"
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Change all expired passwords";
            tsmi.Click += _plugin.ToolChain.ChangeExpiredPasswords;
            tsMenu.Add(tsmi);
        }

        internal void RegisterFormTabs()
        {
            GlobalWindowManager.WindowAdded += delegate(object sender, GwmWindowEventArgs args)
            {
                switch (args.Form)
                {
                    case PwEntryForm ef:
                        ef.Shown += OnEditEntryFormShown;
                        break;
                    case GroupForm gf:
                        gf.Shown += OnEditGroupFormShown;
                        break;
                    case DatabaseSettingsForm dsf:
                        dsf.Shown += OnDbSettingsFormShown;
                        break;
                    case OptionsForm of:
                        of.Shown += OnOptionsFormShown;
                        break;
                }
            };
        }

        private void OnEditEntryFormShown(object sender, EventArgs args)
        {
            PwEntryForm form = (PwEntryForm) sender;
            PwEntry entry = form.EntryRef;

            // Extract gui objects
            Control[] cs = form.Controls.Find("m_tabMain", true);
            if (cs.Length == 0)
                return;
            TabControl mainTabControl = (TabControl) cs[0];
            
            // Attach my userControl to the window
            TabPage kpTabPage = new TabPage("KPChange");
            //kpTabPage.Controls.Add(new PwEntryUserControll(_plugin, entry));
            mainTabControl.TabPages.Add(kpTabPage);
        }

        private void OnEditGroupFormShown(object sender, EventArgs args)
        {
            
        }

        private void OnDbSettingsFormShown(object sender, EventArgs args)
        {
            
        }

        private void OnOptionsFormShown(object sender, EventArgs args)
        {
            
        }
        
    }
}