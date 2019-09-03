using System.Collections.Generic;
using BokLib.Log;
using BokLib.Tools;
using Harmony;

namespace InsulatedFarmTiles
{
    public static class InsulatedFarmTilesPatches
    {
        private const string Name = "Insulated Farm Tiles";
        private const string Version = "1.0.1.0";
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
                BuildingTools.AddBuildingToStrings(ModInfo, InsulatedFarmTileConfig.Id, 
                    InsulatedFarmTileConfig.DisplayName,InsulatedFarmTileConfig.Description,
                    InsulatedFarmTileConfig.Effect,InsulatedFarmTileConfig.PlanName);
                BuildingTools.AddBuildingToStrings(ModInfo, InsulatedHydroponicFarmConfig.Id, 
                    InsulatedHydroponicFarmConfig.DisplayName,InsulatedHydroponicFarmConfig.Description,
                    InsulatedHydroponicFarmConfig.Effect,InsulatedHydroponicFarmConfig.PlanName);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class InsulatedFarmTilesDbInitialize
        {
            public static void Prefix()
            {
                BuildingTools.AddBuildingToTechGroup(ModInfo, InsulatedFarmTileConfig.TechGroup, InsulatedFarmTileConfig.Id);
                BuildingTools.AddBuildingToTechGroup(ModInfo, InsulatedHydroponicFarmConfig.TechGroup, InsulatedHydroponicFarmConfig.Id);
            }
        }
    }
}
