using System;
using KeePass.Plugins;

namespace KPChange
{
    public class KPChangeExt : Plugin
    {
        
        private IPluginHost _host;
        private GlobalTools _globalTools;
        private ChangerProvider _changerProvider;
        
        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;
            _host = host;
            
            _changerProvider = new ChangerProvider();
            _globalTools = new GlobalTools(host, _changerProvider);
            
            return base.Initialize(host);
        }

    }
}