using System;
using System.Collections.Generic;
using KeePassLib;
using KPChange.PasswordChangers;


namespace KPChange
{
    
    public class ChangerProvider
    {
        
        public readonly HashSet<Type> Changers = new HashSet<Type>(new Type[]
        {
            typeof(GoogleChanger),
            typeof(DropboxChanger), 
            typeof(NotChanger),
        });

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

        public AbstractPasswordChanger GetChanger(PwEntry pwEntry)
        {
            // TODO The user should be able to set their own changer manually
            return GetAutomaticChanger(pwEntry);
        }
        
    }
}