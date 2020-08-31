using BokLib.Log;
using BokLib.Tools;
using BokLib.Utils;
using Harmony;

namespace InsulatedFarmTiles
{
    public static class InsulatedFarmTilesPatches
    {
        private const string Name = "Insulated Farm Tiles";

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
                BuildingTools.AddBuildingToStringsAndPlanScreen(ModInfo, InsulatedFarmTileConfig.Id,
                    InsulatedFarmTileConfig.DisplayName, InsulatedFarmTileConfig.Description,
                    InsulatedFarmTileConfig.Effect, InsulatedFarmTileConfig.PlanName);
                BuildingTools.AddBuildingToStringsAndPlanScreen(ModInfo, InsulatedHydroponicFarmConfig.Id,
                    InsulatedHydroponicFarmConfig.DisplayName, InsulatedHydroponicFarmConfig.Description,
                    InsulatedHydroponicFarmConfig.Effect, InsulatedHydroponicFarmConfig.PlanName);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class InsulatedFarmTilesDbInitialize
        {
            private static void Prefix()
            {
                BuildingTools.AddBuildingToTechGroup(ModInfo, InsulatedFarmTileConfig.TechGroup,
                    InsulatedFarmTileConfig.Id);
                BuildingTools.AddBuildingToTechGroup(ModInfo, InsulatedHydroponicFarmConfig.TechGroup,
                    InsulatedHydroponicFarmConfig.Id);
            }
        }
    }
}