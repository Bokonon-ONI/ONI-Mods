using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using UnityEngine;
using Klei;

namespace DisableInstantbuildOnSavegameLoad
{
    public class DisableInstantbuildOnSavegameLoadConfig
    {
        public static void InitLogs()
        {
            HarmonyInstance.DEBUG = true;

            Harmony.FileLog.Log("*** Entered DisableInstantbuildOnSavegameLoad ***");
            Console.WriteLine("*** Entered DisableInstantbuildOnSavegameLoad ***");
        }

        private static string Timestamp() => System.DateTime.UtcNow.ToString("(HH:mm:ss)");
       
    }

   
}
