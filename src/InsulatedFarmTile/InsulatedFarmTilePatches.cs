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
        private static readonly BokModInfo modinfo = new BokModInfo(Name, Version);

        public static class OnModLoad
        {
            public static void OnLoad()
            {
                LogTools.Initialize(modinfo);
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class InsulatedFarmTilesGeneratedBuildingsLoadGeneratedBuildings
        {
            private static void Prefix()
            {
                LogTools.Debug(modinfo, "Adding Insulated Farm Tile to Database ...");
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.NAME",
                    InsulatedFarmTileConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.DESC",
                    InsulatedFarmTileConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.EFFECT",
                    InsulatedFarmTileConfig.Effect);

                ModUtil.AddBuildingToPlanScreen("Food", InsulatedFarmTileConfig.ID);

                LogTools.Debug(modinfo, "Adding Insulated Hydroponic Tile to Database ...");
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.NAME",
                    InsulatedHydroponicFarmConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.DESC",
                    InsulatedHydroponicFarmConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.EFFECT",
                    InsulatedHydroponicFarmConfig.Effect);

                ModUtil.AddBuildingToPlanScreen("Food", InsulatedHydroponicFarmConfig.ID);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class InsulatedFarmTilesDbInitialize
        {
            public static string TechGroup = "FinerDining";
            public static void Prefix()
            {
                AddBuildingToTechGroup(TechGroup, InsulatedFarmTileConfig.ID);
                AddBuildingToTechGroup(TechGroup, InsulatedHydroponicFarmConfig.ID);
            }
        }

        public static void AddBuildingToTechGroup(string techgroup, string buildingId)
        {
            LogTools.Debug(modinfo, $"Adding BuildingId '{buildingId}' to TechGroup '{techgroup}' ...");
            var techList = new List<string>(Database.Techs.TECH_GROUPING[techgroup]) { buildingId };
            Database.Techs.TECH_GROUPING[techgroup] = techList.ToArray();
        }
    }
}
