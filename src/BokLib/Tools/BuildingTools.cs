﻿using System.Collections.Generic;
using BokLib.Log;
using BokLib.Utils;
using Database;

namespace BokLib.Tools
{
    public static class BuildingTools
    {
        public static void AddBuildingToStringsAndPlanScreen(BokModInfo modInfo, string id, string name, string desc, string effect, string planScreen)
        {
            LogTools.Debug(modInfo, $"Adding '{id}' to Strings ...");

            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.NAME", name);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.DESC", desc);
            Strings.Add($"STRINGS.BUILDINGS.PREFABS.{id.ToUpperInvariant()}.EFFECT", effect);
            
            LogTools.Debug(modInfo, $"Adding '{id}' to '{planScreen}' Plan Screen ...");
            
            ModUtil.AddBuildingToPlanScreen(planScreen, id);
        }

        public static void AddBuildingToTechGroup(BokModInfo modInfo, string techgroup, string buildingId)
        {
            LogTools.Debug(modInfo, $"Adding '{buildingId}' to TechGroup '{techgroup}' ...");
            var techList = new List<string>(Techs.TECH_GROUPING[techgroup]) {buildingId};
            Techs.TECH_GROUPING[techgroup] = techList.ToArray();
        }
    }
}