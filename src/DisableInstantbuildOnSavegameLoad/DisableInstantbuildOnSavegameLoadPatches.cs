using BokLib.Log;
using BokLib.Tools;
using Harmony;

namespace DisableInstantBuildOnSavegameLoad
{
    internal class DisableInstantBuildOnSavegameLoadPatches
    {
        private const string Name = "Disable Instant Build On Savegame Load";
        private const string Version = "1.0.1.0";
        private static readonly BokModInfo bokmodinfo = new BokModInfo(Name, Version);

        internal static class OnModLoad
        {
            public static void OnLoad()
            {
                LogTools.Initialize(bokmodinfo);
            }
        }

        [HarmonyPatch(typeof(LoadScreen))]
        [HarmonyPatch("OnKeyUp")]
        public class LoadScreen_OnKeyUp_Patch
        {
            public static void Prefix()
            {
                LogTools.Debug(bokmodinfo, "Entered LoadScreen_OnKeyUp_Patch.Prefix");
                if (!DebugHandler.enabled)
                {
                    LogTools.Debug(bokmodinfo, "Debug not enabled, exiting ...");
                    return;
                }

                if (!DebugHandler.InstantBuildMode)
                {
                    LogTools.Debug(bokmodinfo, "InstantBuildMode not enabled, exiting ...");
                    return;
                }
                else
                {
                    LogTools.Debug(bokmodinfo, "Setting InstantBuildMode to false!");
                    DebugHandler.InstantBuildMode = false;
                }
            }
        }

        private static string Timestamp() => System.DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
    }
}