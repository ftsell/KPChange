using System.Collections.Generic;
using KeePassLib;

namespace AutoChange
{
    public class DbHelper
    {
        private readonly AutoChangeExt _plugin;

        public DbHelper(AutoChangeExt plugin)
        {
            _plugin = plugin;
        }

        public HashSet<PwEntry> FindExpiredEntries()
        {
            HashSet<PwEntry> result = new HashSet<PwEntry>();

            _plugin.Host.Database.RootGroup.TraverseTree(TraversalMethod.PreOrder,
                // Traverse groups
                delegate(PwGroup group)
                {
                    // TODO Write fancy logic to add all entries of an expired group
                    return true;
                },

                // Traverse single entries
                delegate(PwEntry entry)
                {
                    if (entry.Expires && entry.Strings.Get("Title").ReadString() != "<TAN>")
                        result.Add(entry);

                    return true;
                }
            );

            return result;
        }
    }
}