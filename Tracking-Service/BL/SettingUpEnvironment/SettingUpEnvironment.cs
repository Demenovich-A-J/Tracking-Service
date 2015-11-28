using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BL.SettingUpEnvironment
{
    public static class SettingUpEnvironment
    {
        public static void SettingUpFolders(IEnumerable<string> folders)
        {
            foreach (var info in folders.Select(folder => new DirectoryInfo(folder)).Where(info => !info.Exists))
            {
                info.Create();
            }
        }
    }
}