using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine.UI;

namespace BeerBox_NativeModloader
{
    public static class Patches
    {
        public static void addResource_Prefix(BuildingLibrary __instance, string pID, int pAmount, bool pNewList = false)
        {
            pID = SR.ale;
        }

        public static void setCost_Prefix(ItemAsset __instance, int pGoldCost, string pResourceID_1 = "none", int pCostResource_1 = 0, string pResourceID_2 = "none", int pCostResource_2 = 0)
        {
            if (pResourceID_1 != "none")
            {
                pResourceID_1 = SR.ale;
            }

            if (pResourceID_2 != "none")
            {
                pResourceID_2 = SR.ale;
            }
        }

        public static void change_Prefix(CityStorage __instance, ref int __result, string pRes, int pAmount = 1)
        {
            pRes = SR.ale;
        }

        public static void addNew_Prefix(CityStorage __instance, ref int __result, string pResID, int pAmount)
        {
            pResID = SR.ale;
        }

        public static void add_Prefix(this ActorBag pBag, string pID, int pAmount)
        {
            pID = SR.ale;
        }

        public static IEnumerable<CodeInstruction> showResource_Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].Is(OpCodes.Ldsfld, AccessTools.Field(typeof(SR), nameof(SR.gold))))
                {
                    codes[i].operand = AccessTools.Field(typeof(SR), nameof(SR.ale));

                    break;
                }
            }

            return codes.AsEnumerable();
        }

        public static void get_Prefix(CityStorage __instance, ref int __result, string pRes)
        {
            pRes = SR.ale;
        }
    }
}
