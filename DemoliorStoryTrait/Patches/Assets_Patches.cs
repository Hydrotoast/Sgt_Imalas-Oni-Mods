﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilLibs;

namespace DemoliorStoryTrait.Patches
{
    class Assets_Patches
    {
		[HarmonyPatch(typeof(Assets), "OnPrefabInit")]
		public class Assets_OnPrefabInit_Patch
		{
			[HarmonyPriority(Priority.LowerThanNormal)]
			public static void Prefix(Assets __instance)
			{
				InjectionMethods.AddSpriteToAssets(__instance, "CGM_Impactor_icon");
				InjectionMethods.AddSpriteToAssets(__instance, "CGM_Impactor_image");
				AssetUtils.AddSpriteToAssets(__instance, "ImpactorPip", false,UnityEngine.TextureWrapMode.Clamp);
			}
		}		
	}
}
