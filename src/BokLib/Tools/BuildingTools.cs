using System.Collections.Generic;
using BokLib.Log;
using BokLib.Utils;
using Database;

namespace BokLib.Tools
{
    public static class BuildingTools
    {
        /*
         *  public static Dictionary<string, string[]> TECH_GROUPING = new Dictionary<string, string[]>() {
         * { "FarmingTech", new string[4]{ "AlgaeHabitat", "PlanterBox", "RationBox", "Compost" } },
         * { "FineDining", new string[4]{ "DiningTable", "FarmTile", "CookingStation", "EggCracker" } },
         * { "FoodRepurposing", new string[1]{ "Juicer" } }, { "FinerDining", new string[1]{ "GourmetCookingStation" } },
         * { "Agriculture", new string[5]{ "FertilizerMaker", "HydroponicFarm", "Refrigerator", "FarmStation", "ParkSign" } },
         * { "Ranching", new string[7]{ "CreatureDeliveryPoint", "FishDeliveryPoint", "CreatureFeeder", "FishFeeder", "RanchStation", "ShearingStation", "FlyingCreatureBait" } },
         * { "AnimalControl", new string[5]{ "CreatureTrap", "FishTrap", "AirborneCreatureLure", "EggIncubator", LogicCritterCountSensorConfig.ID } },
         * { "ImprovedOxygen", new string[2]{ "Electrolyzer", "RustDeoxidizer" } },
         * { "GasPiping", new string[4]{ "GasConduit", "GasPump", "GasVent", "GasConduitBridge" } },
         * { "ImprovedGasPiping", new string[7]{ "InsulatedGasConduit", LogicPressureSensorGasConfig.ID, "GasVentHighPressure", "GasLogicValve", "GasBottler", "GasConduitPreferentialFlow", "GasConduitOverflow" } },
         * { "PressureManagement", new string[4]{ "LiquidValve", "GasValve", "ManualPressureDoor", "GasPermeableMembrane" } },
         * { "DirectedAirStreams", new string[3]{ "PressureDoor", "AirFilter", "CO2Scrubber" } },
         * { "LiquidFiltering", new string[2]{ "OreScrubber", "Desalinator" } },
         * { "MedicineI", new string[1]{ "Apothecary" } },
         * { "MedicineII", new string[2]{ "DoctorStation", "HandSanitizer" } },
         * { "MedicineIII", new string[3]{ LogicDiseaseSensorConfig.ID, GasConduitDiseaseSensorConfig.ID, LiquidConduitDiseaseSensorConfig.ID } },
         * { "MedicineIV", new string[2]{ "AdvancedDoctorStation", "HotTub" } },
         * { "LiquidPiping", new string[4]{ "LiquidConduit", "LiquidPump", "LiquidVent", "LiquidConduitBridge" } },
         * { "ImprovedLiquidPiping", new string[6]{ "InsulatedLiquidConduit", LogicPressureSensorLiquidConfig.ID, "LiquidLogicValve", "LiquidConduitPreferentialFlow", "LiquidConduitOverflow", "LiquidReservoir" } },
         * { "PrecisionPlumbing", new string[1]{ "EspressoMachine" } },
         * { "SanitationSciences", new string[4]{ "WashSink", "FlushToilet", ShowerConfig.ID, "MeshTile" } },
         * { "FlowRedirection", new string[1]{ "MechanicalSurfboard" } },
         * { "AdvancedFiltration", new string[2]{ "GasFilter", "LiquidFilter" } },
         * { "Distillation", new string[4]{ "WaterPurifier", "AlgaeDistillery", "EthanolDistillery", "BottleEmptierGas" } },
         * { "Catalytics", new string[3]{ "OxyliteRefinery", "SupermaterialRefinery", "SodaFountain" } },
         * { "PowerRegulation", new string[3]{ SwitchConfig.ID, "BatteryMedium", "WireBridge" } },
         * { "AdvancedPowerRegulation", new string[5]{ "HydrogenGenerator", "HighWattageWire", "WireBridgeHighWattage", "PowerTransformerSmall", LogicPowerRelayConfig.ID } },
         * { "PrettyGoodConductors", new string[5]{ "WireRefined", "WireRefinedBridge", "WireRefinedHighWattage", "WireRefinedBridgeHighWattage", "PowerTransformer" } },
         * { "RenewableEnergy", new string[4]{ "SteamTurbine", "SteamTurbine2", "SolarPanel", "Sauna" } },
         * { "Combustion", new string[2]{ "Generator", "WoodGasGenerator" } },
         * { "ImprovedCombustion", new string[3]{ "MethaneGenerator", "OilRefinery", "PetroleumGenerator" } },
         * { "InteriorDecor", new string[3]{ "FlowerVase", "FloorLamp", "CeilingLight" } },
         * { "Artistry", new string[7]{ "CrownMoulding", "CornerMoulding", "SmallSculpture", "IceSculpture", "ItemPedestal", "FlowerVaseWall", "FlowerVaseHanging" } },
         * { "Clothing", new string[2]{ "ClothingFabricator", "CarpetTile" } },
         * { "Acoustics", new string[3]{ "Phonobox", "BatterySmart", "PowerControlStation" } },
         * { "FineArt", new string[2]{ "Canvas", "Sculpture" } },
         * { "EnvironmentalAppreciation", new string[1]{ "BeachChair" } },
         * { "Luxury", new string[3]{ LuxuryBedConfig.ID, "LadderFast", "PlasticTile" } },
         * { "RefractiveDecor", new string[2]{ "MetalSculpture", "CanvasWide" } },
         * { "GlassFurnishings", new string[3]{ "GlassTile", "FlowerVaseHangingFancy", "SunLamp" } },
         * { "RenaissanceArt", new string[5]{ "MarbleSculpture", "CanvasTall", "MonumentBottom", "MonumentMiddle", "MonumentTop" } },
         * { "Plastics", new string[2]{ "Polymerizer", "OilWellCap" } },
         * { "ValveMiniaturization", new string[2]{ "LiquidMiniPump", "GasMiniPump" } },
         * { "Suits", new string[5]{ "ExteriorWall", "SuitMarker", "SuitLocker", "SuitFabricator", "SuitsOverlay" } },
         * { "Jobs", new string[2]{ "RoleStation", "WaterCooler" } },
         * { "AdvancedResearch", new string[3]{ "AdvancedResearchCenter", "BetaResearchPoint", "ResetSkillsStation" } },
         * { "BasicRefinement", new string[2]{ "RockCrusher", "Kiln" } },
         * { "RefinedObjects", new string[2]{ "ThermalBlock", "FirePole" } },
         * { "Smelting", new string[2]{ "MetalRefinery", "MetalTile" } },
         * { "HighTempForging", new string[3]{ "GlassForge", "BunkerTile", "BunkerDoor" } },
         * { "TemperatureModulation", new string[5]{ "LiquidCooledFan", "IceCooledFan", "IceMachine", "SpaceHeater", "InsulationTile" } },
         * { "HVAC", new string[6]{ "AirConditioner", LogicTemperatureSensorConfig.ID, "GasConduitRadiant", GasConduitTemperatureSensorConfig.ID, GasConduitElementSensorConfig.ID, "GasReservoir" } },
         * { "LiquidTemperature", new string[5]{ "LiquidHeater", "LiquidConditioner", "LiquidConduitRadiant", LiquidConduitTemperatureSensorConfig.ID, LiquidConduitElementSensorConfig.ID } },
         * { "LogicControl", new string[5]{ "LogicWire", "LogicDuplicantSensor", LogicSwitchConfig.ID, "LogicWireBridge", "AutomationOverlay" } },
         * { "GenericSensors", new string[6]{ LogicTimeOfDaySensorConfig.ID, "FloorSwitch", LogicElementSensorGasConfig.ID, LogicElementSensorLiquidConfig.ID, "BatterySmart", "LogicGateNOT" } },
         * { "LogicCircuits", new string[4]{ "LogicGateAND", "LogicGateOR", "LogicGateBUFFER", "LogicGateFILTER" } },
         * { "DupeTrafficControl", new string[5]{ "Checkpoint", LogicMemoryConfig.ID, "ArcadeMachine", "CosmicResearchCenter", "LogicGateXOR" } },
         * { "SkyDetectors", new string[3]{ CometDetectorConfig.ID, "Telescope", "AstronautTrainingCenter" } },
         * { "TravelTubes", new string[4]{ "TravelTubeEntrance", "TravelTube", "TravelTubeWallBridge", "VerticalWindTunnel" } },
         * { "SmartStorage", new string[4]{ "StorageLockerSmart", "SolidTransferArm", "ObjectDispenser", "ConveyorOverlay" } },
         * { "SolidTransport", new string[7]{ "SolidConduit", "SolidConduitBridge", "SolidConduitInbox", "SolidConduitOutbox", "SolidVent", "SolidLogicValve", "AutoMiner" } },
         * { "BasicRocketry", new string[4]{ "CommandModule", "SteamEngine", "ResearchModule", "Gantry" } },
         * { "CargoI", new string[1]{ "CargoBay" } },
         * { "CargoII", new string[2]{ "LiquidCargoBay", "GasCargoBay" } },
         * { "CargoIII", new string[2]{ "TouristModule", "SpecialCargoBay" } },
         * { "EnginesI", new string[1]{ "SolidBooster" } },
         * { "EnginesII", new string[3]{ "KeroseneEngine", "LiquidFuelTank", "OxidizerTank" } },
         * { "EnginesIII", new string[2]{ "OxidizerTankLiquid", "HydrogenEngine" } },
         * { "Jetpacks", new string[3]{ "JetSuit", "JetSuitMarker", "JetSuitLocker" } } };
         */
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