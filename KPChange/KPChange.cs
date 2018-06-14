using System;
using KeePass.Plugins;

namespace KPChange
{
    public class KPChangeExt : Plugin
    {
        
        private IPluginHost _host;
        private GlobalChanger _globalChanger;
        
        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            _host = host;
            
            _globalChanger = new GlobalChanger(host);
            
            return base.Initialize(host);
        }

        public override void Terminate()
        {
            _host = null;
            
            base.Terminate();
        }
    }
}