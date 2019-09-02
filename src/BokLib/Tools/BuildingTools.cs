using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STRINGS;
using UnityEngine;
using Harmony;

namespace BokLib.Tools
{
    class BuildingTools
    {
        public static void AddBuildingToTechGroup(string techgroup, string buildingId)
        {
            var techList = new List<string>(Database.Techs.TECH_GROUPING[techgroup]) { buildingId };
            Database.Techs.TECH_GROUPING[techgroup] = techList.ToArray();
        }
    }
}
