using TUNING;
using UnityEngine;

namespace InsulatedFarmTiles
{
    public class InsulatedHydroponicFarmConfig : IBuildingConfig
    {
        public const string Id = "InsulatedHydroponicFarm";
        public const string DisplayName = "Insulated Hydroponic Farm Tile";
        public const string Description = "An insulated version of a Hydroponic Farm Tile.";
        public const string Effect = "A hydroponic farm tile insulated to help with temperature regulation.";
        public const string PlanName = "Food";
        public const string TechGroup = "FinerDining";

        public override BuildingDef CreateBuildingDef()
        {
            string id = Id;
            int width = 1;
            int height = 1;
            string anim = "insulatedfarmtilehydroponic_kanim";
            int hitpoints = 100;
            float construction_time = 300f;
            float[] tieR3 = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
            string[] allMetals = MATERIALS.RAW_MINERALS;
            float melting_point = 1600f;
            BuildLocationRule build_location_rule = BuildLocationRule.Tile;
            EffectorValues none = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, tieR3, allMetals, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
            BuildingTemplates.CreateFoundationTileDef(buildingDef);
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.Overheatable = false;
            buildingDef.UseStructureTemperature = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.SceneLayer = Grid.SceneLayer.TileMain;
            buildingDef.ConstructionOffsetFilter = BuildingDef.ConstructionOffsetFilter_OneDown;
            buildingDef.isSolidTile = true;
            buildingDef.PermittedRotations = PermittedRotations.FlipV;
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.UtilityInputOffset = new CellOffset(0, 0);
            buildingDef.ThermalConductivity = 1f / 32f;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<SimCellOccupier>().doReplaceElement = true;
            go.AddOrGet<Insulator>();
            go.AddOrGet<TileTemperature>();
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.consumptionRate = 1f;
            conduitConsumer.capacityKG = 5f;
            conduitConsumer.capacityTag = GameTags.Liquid;
            conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
            go.AddOrGet<Storage>();
            PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
            plantablePlot.AddDepositTag(GameTags.CropSeed);
            plantablePlot.AddDepositTag(GameTags.WaterSeed);
            plantablePlot.occupyingObjectRelativePosition.y = 1f;
            plantablePlot.SetFertilizationFlags(true, true);
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.Farm;
            BuildingTemplates.CreateDefaultStorage(go, false).SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
            go.AddOrGet<PlanterBox>();
            go.AddOrGet<AnimTileable>();
            go.AddOrGet<DropAllWorkable>();
            Prioritizable.AddRef(go);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            FarmTileConfig.SetUpFarmPlotTags(go);
            go.GetComponent<KPrefabID>().AddTag(GameTags.FarmTiles, false);
            go.GetComponent<RequireInputs>().requireConduitHasMass = false;
        }
    }

}
