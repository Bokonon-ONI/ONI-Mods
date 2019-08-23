using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;

namespace InsulatedFarmTilePatches
{
	[HarmonyPatch(typeof(Game), "OnPrefabInit")]
	internal class TemplateMod_Game_OnPrefabInit
	{
		private static void Postfix(Game __instance)
		{
			Debug.Log(" === TemplateMod_Game_OnPrefabInit Postfix === ");
		}
	}
}
