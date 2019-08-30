using System;
using Harmony;

namespace DisableInstantBuildOnSavegameLoad
{
    internal class DisableInstantBuildOnSavegameLoadPatches
    {
        public static class ModInfo
        {
            public static string Name = "Disable Instant Build On Savegame Load";
            public static string Version = "1.0.0";
        }

        public static class OnModLoad
        {
            public static void OnLoad()
            {
                Console.WriteLine($"{Timestamp()} Loading Mod: \"{ModInfo.Name}\" Version: {ModInfo.Version}");
            }
        }

        [HarmonyPatch(typeof(LoadScreen))]
        [HarmonyPatch("OnKeyUp")]
        public class LoadScreen_OnKeyUp_Patch
        {
            public static void Prefix()
            {
                //Console.WriteLine($"{Timestamp()} **** Entered LoadScreen_OnKeyUp_Patch.Prefix ****");
                if (!DebugHandler.enabled)
                    return;
                DebugHandler.InstantBuildMode = false;
            }
        }

        private static string Timestamp() => System.DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
    }
}