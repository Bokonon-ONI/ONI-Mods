using BokLib.Log;
using BokLib.Utils;
using Harmony;

namespace DisableInstantBuildOnSavegameLoad
{
    internal class DisableInstantBuildOnSavegameLoadPatches
    {
        private const string Name = "Disable Instant Build On Savegame Load";
        private const string Version = "1.0.4.0";
        private static readonly BokModInfo ModInfo = new BokModInfo(Name, Version);

        internal static class OnModLoad
        {
            public static void OnLoad()
            {
                LogTools.Initialize(ModInfo);
            }
        }

        [HarmonyPatch(typeof(LoadScreen))]
        [HarmonyPatch("OnKeyUp")]
        public class LoadScreen_OnKeyUp_Patch
        {
            public static void Prefix()
            {
                LogTools.Debug(ModInfo, "Entered LoadScreen_OnKeyUp_Patch.Prefix");
                if (!DebugHandler.enabled)
                {
                    LogTools.Debug(ModInfo, "Debug not enabled, exiting ...");
                    return;
                }

                if (!DebugHandler.InstantBuildMode)
                {
                    LogTools.Debug(ModInfo, "InstantBuildMode not enabled, exiting ...");
                    return;
                }
                else
                {
                    LogTools.Debug(ModInfo, "Setting InstantBuildMode to false!");
                    DebugHandler.InstantBuildMode = false;
                }
            }
        }
    }
}