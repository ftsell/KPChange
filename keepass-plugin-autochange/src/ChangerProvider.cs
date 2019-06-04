using System;
using System.Collections.Generic;
using AutoChange.PasswordChangers;
using KeePassLib;


namespace AutoChange
{
    
    public class ChangerProvider
    {

        private AutoChangeExt _plugin;
        public readonly HashSet<Type> Changers = new HashSet<Type>(new Type[]
        {
            typeof(GoogleChanger),
            typeof(DropboxChanger), 
            typeof(NotChanger),
        });

        public ChangerProvider(AutoChangeExt plugin)
        {
            _plugin = plugin;
        }

        private AbstractPasswordChanger GetAutomaticChanger(PwEntry pwEntry)
        {
            int maxPrio = 0;
            AbstractPasswordChanger bestChanger = null;
            
            foreach (var changer in Changers)
            {

                AbstractPasswordChanger instance = (AbstractPasswordChanger) Activator.CreateInstance(changer);
                
                int prio = instance.DoesHandleEntry(pwEntry);
                if (prio > maxPrio)
                {
                    maxPrio = prio;
                    bestChanger = instance;
                }
            }

            return bestChanger;
        }

        internal AbstractPasswordChanger GetChanger(PwEntry pwEntry)
        {
            // TODO The user should be able to set their own changer manually
            AbstractPasswordChanger changer = GetAutomaticChanger(pwEntry);

            changer.Entry = pwEntry;
            changer.Plugin = _plugin;
            
            return changer;
        }

        internal Dictionary<PwEntry, AbstractPasswordChanger> GetChangers(HashSet<PwEntry> pwEntries)
        {
            Dictionary<PwEntry, AbstractPasswordChanger> result = new Dictionary<PwEntry, AbstractPasswordChanger>();

            foreach (var entry in pwEntries)
                result.Add(entry, GetChanger(entry));
            
            return result;
        }
        
    }
}