using TUNING;
using UnityEngine;

namespace InsulatedFarmTiles
{
    public class InsulatedFarmTileConfig : IBuildingConfig
    {
        public const string Id = "InsulatedFarmTile";
        public const string DisplayName = "Insulated Farm Tile";
        public static string Description = "An insulated version of a Farm Tile.";
        public static string Effect = "A farm tile insulated to help with temperature regulation.  Useful for allowing feeding wheezeworts phosphorite from outside an otherwise sealed environment.";
        public static string PlanName = "Food";
        public static string TechGroup = "FinerDining";

        public override BuildingDef CreateBuildingDef()
        {
            string id = Id;
            int width = 1;
            int height = 1;
            string anim = "insulatedfarmtile_kanim";
            int hitpoints = 100;
            float construction_time = 40f;
            float[] tieR3 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
            string[] farmable = MATERIALS.RAW_MINERALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Tile;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR3, farmable, melting_point, build_location_rule, BUILDINGS.DECOR.NONE, none, 0.2f);
            BuildingTemplates.CreateFoundationTileDef(buildingDef);
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.Overheatable = false;
            buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingBack;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
            buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
            buildingDef.PermittedRotations = PermittedRotations.FlipV;
            buildingDef.isSolidTile = false;
            buildingDef.DragBuild = true;
            buildingDef.ThermalConductivity = 1f / 32f;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
            simCellOccupier.doReplaceElement = true;
            simCellOccupier.notifyOnMelt = true;
            go.AddOrGet<Insulator>();
            go.AddOrGet<TileTemperature>();
            BuildingTemplates.CreateDefaultStorage(go, false).SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
            PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
            plantablePlot.occupyingObjectRelativePosition = new Vector3(0.0f, 1f, 0.0f);
            plantablePlot.AddDepositTag(GameTags.CropSeed);
            plantablePlot.AddDepositTag(GameTags.WaterSeed);
            plantablePlot.SetFertilizationFlags(true, false);
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Farm;
            go.AddOrGet<AnimTileable>();
            Prioritizable.AddRef(go);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            GeneratedBuildings.RemoveLoopingSounds(go);
            go.GetComponent<KPrefabID>().AddTag(GameTags.FarmTiles, false);
            FarmTileConfig.SetUpFarmPlotTags(go);
        }
    }
}