﻿using Rockets_TinyYetBig.Content.Scripts.Buildings.RocketPortAdapters;
using TUNING;
using UnityEngine;

namespace Rockets_TinyYetBig.RocketFueling
{
    public class LoaderTravelTubeAdapterConfig : IBuildingConfig
	{
		public const string ID = "RTB_TravelTubeConnectionAdapter";
		public override string[] GetRequiredDlcIds() => DlcManager.EXPANSION1;
		public override BuildingDef CreateBuildingDef()
		{

			string[] Materials = new string[]
			{
				MATERIALS.REFINED_METAL
			};
			float[] MaterialCosts = new float[] { 300f };

			BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
					ID,
					1,
					2,
					"loader_tube_adapter_kanim",
					200,
					40f,
					MaterialCosts,
					Materials,
					800f,
					BuildLocationRule.Tile,
					noise: NOISE_POLLUTION.NONE,
					decor: BUILDINGS.DECOR.PENALTY.TIER0);

			buildingDef.ObjectLayer = ObjectLayer.Building;
			buildingDef.TileLayer = ObjectLayer.FoundationTile;

			buildingDef.IsFoundation = true;

			buildingDef.AudioCategory = "Plastic";
			buildingDef.Floodable = false;
			buildingDef.Overheatable = false;
			buildingDef.Entombable = false;
			buildingDef.DefaultAnimState = "on";
			buildingDef.AudioCategory = "Metal";
			buildingDef.AudioSize = "small";
			buildingDef.BaseTimeUntilRepair = -1f;

			buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
			buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
			buildingDef.UseStructureTemperature = false;

			buildingDef.CanMove = false;
			return buildingDef;
		}
		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			GeneratedBuildings.MakeBuildingAlwaysOperational(go);
			BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);


			SimCellOccupier simCellOccupier = go.AddOrGet<SimCellOccupier>();
			simCellOccupier.doReplaceElement = true;
			simCellOccupier.notifyOnMelt = true;
			go.AddOrGet<TileTemperature>();
			go.AddOrGet<BuildingHP>().destroyOnDamaged = true;


			KPrefabID component = go.GetComponent<KPrefabID>();
			component.AddTag(BaseModularLaunchpadPortConfig.LinkTag);
			component.AddTag(GameTags.ModularConduitPort);
			component.AddTag(GameTags.NotRocketInteriorBuilding);
			component.AddTag(ModAssets.Tags.SpaceStationOnlyInteriorBuilding);

			ChainedBuilding.Def def = go.AddOrGetDef<ChainedBuilding.Def>();
			def.headBuildingTag = ModAssets.Tags.RocketPlatformTag;
			def.linkBuildingTag = BaseModularLaunchpadPortConfig.LinkTag;
			def.objectLayer = ObjectLayer.Building;
			go.AddOrGet<AnimTileable>();

			go.AddOrGet<VariedSizeTravelTubePiece>();
		}

		public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
		{
			base.DoPostConfigurePreview(def, go);
			AddNetworkLink(go, true);
			go.AddOrGet<BuildingCellVisualizer>();
		}

		public override void DoPostConfigureUnderConstruction(GameObject go)
		{
			base.DoPostConfigureUnderConstruction(go);
			AddNetworkLink(go, true);
			go.AddOrGet<BuildingCellVisualizer>();
		}

		public override void DoPostConfigureComplete(GameObject go)
		{
			go.AddOrGet<BuildingCellVisualizer>();
			AddNetworkLink(go, false);
			go.AddOrGet<KPrefabID>().AddTag(GameTags.TravelTubeBridges);
		}

		protected TravelTubeUtilityNetworkLink AddNetworkLink(GameObject go, bool visualOnly)
		{
			TravelTubeUtilityNetworkLink utilityNetworkLink = go.AddOrGet<TravelTubeUtilityNetworkLink>();
			utilityNetworkLink.link1 = new CellOffset(0, -1);
			utilityNetworkLink.link2 = new CellOffset(0, 2);
			utilityNetworkLink.visualizeOnly = visualOnly;

			return utilityNetworkLink;
		}
	}
}
