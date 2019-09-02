using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BokLib.Log;
using BokLib.Tools;
using STRINGS;
using UnityEngine;
using Harmony;

namespace InsulatedFarmTiles
{
    public static class InsulatedFarmTilesPatches
    {
        private const string Name = "Insulated Farm Tiles";
        private const string Version = "1.0.0.0";
        private static readonly BokModInfo bokmodinfo = new BokModInfo(Name, Version);

        public static class OnModLoad
        {
            public static void OnLoad()
            {
                LogTools.Initialize(bokmodinfo);
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        internal class InsulatedFarmTilesGeneratedBuildingsLoadGeneratedBuildings
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
        public class InsulatedFarmTilesDbInitialize
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
    }
}
