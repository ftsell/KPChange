using System.Collections.Generic;
using KeePass;
using KeePassLib;
using KPChange.PasswordChangers;


namespace KPChange
{
    
    public class ChangerProvider
    {
        
        public HashSet<IPasswordChanger> Changers = new HashSet<IPasswordChanger>(new []
        {
            new GoogleChanger(),
        });

        public IPasswordChanger GetAutomaticChanger(PwEntry pwEntry)
        {
            foreach (var changer in Changers)
            {
                return changer;    // TODO Improve
            }

            return null;
        }

        public IPasswordChanger GetChanger(PwEntry pwEntry)
        {
            return new GoogleChanger();    // TODO Improve
        }
        
    }
}