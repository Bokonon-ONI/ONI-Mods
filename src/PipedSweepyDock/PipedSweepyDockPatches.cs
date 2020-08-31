using BokLib.Log;
using BokLib.Tools;
using BokLib.Utils;
using Harmony;

namespace PipedSweepyDock
{
    public class PipedSweepyDockPatches
    {
        private const string Name = "Piped Sweepy Dock";

        private static readonly string Version =
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        private static readonly BokModInfo ModInfo = new BokModInfo(Name, Version);

        public static class OnModLoad
        {
            public static void OnLoad()
            {
                LogTools.Initialize(ModInfo);
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class InsulatedFarmTilesGeneratedBuildingsLoadGeneratedBuildings
        {
            private static void Prefix()
            {
                BuildingTools.AddBuildingToStringsAndPlanScreen(ModInfo, PipedSweepyDockConfig.Id,
                    PipedSweepyDockConfig.DisplayName, PipedSweepyDockConfig.Description,
                    PipedSweepyDockConfig.Effect, PipedSweepyDockConfig.PlanName);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class InsulatedFarmTilesDbInitialize
        {
            private static void Prefix()
            {
                BuildingTools.AddBuildingToTechGroup(ModInfo, PipedSweepyDockConfig.TechGroup,
                    PipedSweepyDockConfig.Id);
            }
        }
    }
}