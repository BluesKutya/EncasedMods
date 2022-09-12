using HarmonyLib;
using DarkCrystal.Encased.Core;
using DarkCrystal.Encased.Core.ModuleSystem;
using Lifebelt.Configurations;
using DarkCrystal.Encased.Core.RolePlay;

namespace Lifebelt
{
	class Lifebelt
	{


		[HarmonyPatch(typeof(BaseInventoryModule))]
		[HarmonyPatch("Weight", MethodType.Getter)]
		internal class BaseInventoryModule_Weight_Getter__Patch
		{
			[HarmonyPostfix]
			public static void Postfix(BaseInventoryModule __instance, ref float __result)
			{
				if (Configuration.getInstance().isAllItemWeightZero.Value)
					__result = 0.0f;
			}
		}


		[HarmonyPatch(typeof(StatusPointsHelper))]
		[HarmonyPatch(nameof(StatusPointsHelper.Add))]
		internal class StatusPointsHelper_Add_Patch
		{
			[HarmonyPrefix]
			public static bool Prefix(StatusPointsHelper __instance, ref float value)
			{
				bool isDisabled = false;
				StatusPoints spt = __instance.StatusPointsType;

				switch (spt) {
					case StatusPoints.Radiation:
						isDisabled = Configuration.getInstance().isRadiationDisabled.Value;
					break;
					case StatusPoints.Fatigue:
						isDisabled = Configuration.getInstance().isFatigueDisabled.Value;
					break;
					case StatusPoints.Hunger:
						isDisabled = Configuration.getInstance().isHungerDisabled.Value;
					break;
					case StatusPoints.Thirst:
						isDisabled = Configuration.getInstance().isThirstDisabled.Value;
					break;
				}

				if (isDisabled && (value > 0))
					value = 0f;

				return true; //run the orignal code
			}
		}

		[HarmonyPatch(typeof(PocketThieveryAbilityHandler))]
		[HarmonyPatch(nameof(PocketThieveryAbilityHandler.GetBlockReasons))]
		internal class PocketThieveryAbilityHandler_GetBlockReasons_Patch
		{
			[HarmonyPostfix]
			public static void Postfix(PocketThieveryAbilityHandler __instance, ref BlockReasons __result)
			{
				if ((__result == BlockReasons.TargetImmune) && Configuration.getInstance().isRepeatedPickpocketEnabled.Value)
					__result = BlockReasons.None;
			}
		}


	}

}
