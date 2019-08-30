using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;
using Harmony;

namespace InsulatedFarmTiles
{
    public static class InsulatedFarmTilesPatches
    {
        public static class ModInfo
        {
            public static string Name = "Insulated Farm Tiles";
            public static string Version = "1.0.0";
        }

        public static class OnModLoad
        {
            public static void OnLoad()
            {
                Console.WriteLine($"{Timestamp()} ## Loading Mod: \"{ModInfo.Name}\" Version: {ModInfo.Version} ##");
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class InsulatedFarmTiles_GeneratedBuildings_LoadGeneratedBuildings
        {
            private static void Prefix()
            {
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.NAME", InsulatedFarmTileConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.DESC", InsulatedFarmTileConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedFarmTileConfig.ID.ToUpperInvariant()}.EFFECT", InsulatedFarmTileConfig.Effect);

                ModUtil.AddBuildingToPlanScreen("Food", InsulatedFarmTileConfig.ID);

                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.NAME", InsulatedHydroponicFarmConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.DESC", InsulatedHydroponicFarmConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InsulatedHydroponicFarmConfig.ID.ToUpperInvariant()}.EFFECT", InsulatedHydroponicFarmConfig.Effect);

                ModUtil.AddBuildingToPlanScreen("Food", InsulatedHydroponicFarmConfig.ID);
            }
        }

        [HarmonyPatch(typeof(Db), "Initialize")]
        public class InsulatedFarmTiles_Db_Initialize
        {
            public static string TechGroup = "TemperatureModulation";
            public static void Prefix()
            {
                AddBuildingToTechGroup(TechGroup, InsulatedFarmTileConfig.ID);
                AddBuildingToTechGroup(TechGroup, InsulatedHydroponicFarmConfig.ID);
            }
        }

        public static void AddBuildingToTechGroup(string techgroup, string buildingId)
        {
            var techList = new List<string>(Database.Techs.TECH_GROUPING[techgroup]) { buildingId };
            Database.Techs.TECH_GROUPING[techgroup] = techList.ToArray();
        }

        private static string Timestamp() => System.DateTime.UtcNow.ToString("[HH:mm:ss.fff]");
    }
}
