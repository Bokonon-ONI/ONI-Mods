﻿using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace PipedSweepyDock
{
    public class PipedSweepyDockConfig : IBuildingConfig
    {
        public const string Id = "PipedSweepyDock";
        public const string DisplayName = "Piped Sweepy Dock";
        public const string Description = "Add conveyor output to Sweepy's dock.";

        public const string Effect =
            "Adds a conveyor output to the Sweepy Dock.  It's pretty useless without it ... :P";

        public const string PlanName = "Utilities";
        public const string TechGroup = "SolidTransport";

        public override BuildingDef CreateBuildingDef()
        {
            var id = Id;
            var width = 1;
            var height = 2;
            var anim = "sweep_bot_base_station_kanim";
            var hitpoints = 30;
            var construction_time = 30f;
            var melting_point = 1600f;
            var build_location_rule = BuildLocationRule.OnFloor;

            float[] construction_mass = new float[1]
            {
                BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0] - SweepBotConfig.MASS
            };
            string[] refinedMetals = MATERIALS.REFINED_METALS;
            EffectorValues none = NOISE_POLLUTION.NONE;
            EffectorValues tieR1 = BUILDINGS.DECOR.PENALTY.TIER1;
            EffectorValues noise = none;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time,
                construction_mass, refinedMetals, melting_point, build_location_rule, tieR1, noise, 0.2f);
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.Floodable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.RequiresPowerInput = true;
            buildingDef.EnergyConsumptionWhenActive = 240f;
            buildingDef.ExhaustKilowattsWhenActive = 0.0f;
            buildingDef.SelfHeatKilowattsWhenActive = 1f;
            buildingDef.OutputConduitType = ConduitType.Solid;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            Prioritizable.AddRef(go);
            Storage storage1 = go.AddComponent<Storage>();
            storage1.showInUI = true;
            storage1.allowItemRemoval = false;
            storage1.ignoreSourcePriority = true;
            storage1.showDescriptor = true;
            storage1.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage1.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage1.fetchCategory = Storage.FetchCategory.Building;
            storage1.capacityKg = 25f;
            storage1.allowClearable = false;
            Storage storage2 = go.AddComponent<Storage>();
            storage2.showInUI = true;
            storage2.allowItemRemoval = true;
            storage2.ignoreSourcePriority = true;
            storage2.showDescriptor = true;
            storage2.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage2.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage2.fetchCategory = Storage.FetchCategory.StorageSweepOnly;
            storage2.capacityKg = 1000f;
            storage2.allowClearable = true;
            go.AddOrGet<CharacterOverlay>();
            go.AddOrGet<SweepBotStation>();
            go.AddOrGet<SolidConduitDispenser>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
        }
    }
}