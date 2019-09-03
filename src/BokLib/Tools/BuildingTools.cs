using System.Collections.Generic;

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
