using System;
using Harmony;

namespace DisableInstantbuildOnSavegameLoad
{
    [HarmonyPatch(typeof(LoadScreen), "DoLoad", new [] { typeof(string) } )]
    //[HarmonyPatch(typeof(LoadScreen))]
    //[HarmonyPatch("Load")]
    //[HarmonyPatch("DoLoad")]
    internal class DisableInstantbuildOnSavegameLoadPatches
    {

        
        public class LoadScreen_DoLoad_Patch
        {
            public static bool Prefix(ref DebugHandler __instance)
            {
                HarmonyInstance.DEBUG = true;

                Harmony.FileLog.Log("Entered DisableInstantbuildOnSavegameLoad");
                Console.WriteLine("Entered DisableInstantbuildOnSavegameLoad");
//                DisableInstantbuildOnSavegameLoadConfig.InitLogs();
                if (!DebugHandler.enabled)
                    return false;
                //if (DebugHandler.InstantBuildMode) {
                //    DebugHandler.InstantBuildMode = false;
                //}
                //                (DebugHandler) __instance.InstantBuildMode = false;
                return false;
            }
        }
    }
}