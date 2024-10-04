using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReflectionUtility;
using UnityEngine;
using UnityEngine.UI;
using static Config;
using UnityEngine.Events;
using ai;
using NeoModLoader.constants;
using System.Reflection.Emit;

namespace BeerBoxNCMS
{
    class BeerBoxClass
    {
        public static Harmony harmony = new Harmony("jean.worldbox.mods.dvarfbeermodncms");
        public static void init()
        {
            harmony.Patch(AccessTools.Method(typeof(BuildingLibrary), nameof(BuildingLibrary.addResource)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.addResource_Prefix))));

            harmony.Patch(AccessTools.Method(typeof(ItemAsset), nameof(ItemAsset.setCost)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.setCost_Prefix))));

            //  harmony.Patch(AccessTools.Method(typeof(BuildingBiomeFoodProducer), nameof(BuildingBiomeFoodProducer.update)),
            //      prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.update_Prefix))));

            harmony.Patch(AccessTools.Method(typeof(CityStorage), nameof(CityStorage.change)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.change_Prefix))));

            harmony.Patch(AccessTools.Method(typeof(CityStorage), nameof(CityStorage.addNew)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.addNew_Prefix))));

            //  harmony.Patch(AccessTools.Method(typeof(City), nameof(City.hasEnoughResourcesFor)),
            //      prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.hasEnoughResourcesFor_Prefix))));

            //  harmony.Patch(AccessTools.Method(typeof(City), nameof(City.getCitizenJob)),
            //      prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.getCitizenJob_Prefix))));

            //  harmony.Patch(AccessTools.Method(typeof(CityStorage), nameof(CityStorage.getMaterialForItem)),
            //      prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.getMaterialForItem_Prefix))));

            //  harmony.Patch(AccessTools.Method(typeof(Actor), nameof(Actor.addToInventory)),
            //      prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.addToInventory_Prefix))));

            harmony.Patch(AccessTools.Method(typeof(TooltipLibrary), nameof(TooltipLibrary.showResource)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.showResource_Prefix))));

            harmony.Patch(AccessTools.Method(typeof(ActorBagExtensions), nameof(ActorBagExtensions.add)),
                prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.add_Prefix))));

            var beer = AssetManager.resources.get(SR.ale);
            beer.maximum = 9999;
            beerLocalization("ar", beer.id, "البيرة");
            beerLocalization("boat", beer.id, "Beeroat");
            beerLocalization("br", beer.id, "Cerveja");
            beerLocalization("by", beer.id, "Піва");
            beerLocalization("ch", beer.id, "啤酒");
            beerLocalization("cs", beer.id, "Pivo");
            beerLocalization("cz", beer.id, "啤酒");
            beerLocalization("da", beer.id, "Øl");
            beerLocalization("de", beer.id, "Bier");
            beerLocalization("en", beer.id, "Beer");
            beerLocalization("es", beer.id, "Cerveza");
            beerLocalization("fa", beer.id, "آبجو");
            beerLocalization("fn", beer.id, "Olut");
            beerLocalization("fr", beer.id, "Bière");
            beerLocalization("gr", beer.id, "Μπύρα");
            beerLocalization("he", beer.id, "בירה");
            beerLocalization("hi", beer.id, "बीयर");
            beerLocalization("hr", beer.id, "Pivo");
            beerLocalization("hu", beer.id, "Sör");
            beerLocalization("id", beer.id, "Bir");
            beerLocalization("it", beer.id, "Birra");
            beerLocalization("ja", beer.id, "ビール");
            beerLocalization("ka", beer.id, "ლუდი");
            beerLocalization("ko", beer.id, "맥주");
            beerLocalization("lb", beer.id, "Béier");
            beerLocalization("nl", beer.id, "Bier");
            beerLocalization("no", beer.id, "Øl");
            beerLocalization("ph", beer.id, "Serbesa");
            beerLocalization("pl", beer.id, "Piwo");
            beerLocalization("pt", beer.id, "Cerveja");
            beerLocalization("ro", beer.id, "Bere");
            beerLocalization("ru", beer.id, "Пиво");
            beerLocalization("sk", beer.id, "Pivo");
            beerLocalization("sv", beer.id, "Öl");
            beerLocalization("th", beer.id, "เบียร์");
            beerLocalization("tr", beer.id, "Bira");
            beerLocalization("ua", beer.id, "Пиво");
            beerLocalization("vn", beer.id, "Bia");
        }

        public static void beerLocalization(string planguage, string id, string name)
        {
            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
            string templanguage;

            templanguage = language;

            if (planguage == templanguage)
            {
                Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
                localizedText.Remove("ale");
                localizedText.Add(id, name);
            }
        }
    }

    public static class Patches
    {
        public static void addResource_Prefix(BuildingLibrary __instance, string pID, int pAmount, bool pNewList = false)
        {
            pID = SR.ale;

            //  BuildingAsset t = Reflection.GetField(__instance.GetType(), __instance, "t") as BuildingAsset;
            //  
            //  if (t.resources_given == null || pNewList)
            //  {
            //      t.resources_given = new List<ResourceContainer>();
            //  }
            //  t.resources_given.Add(new ResourceContainer
            //  {
            //      id = SR.ale,
            //      amount = pAmount
            //  });
            //  return false;
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

            //  __instance.cost_gold = pGoldCost;
            //  if (pResourceID_1 != "none")
            //  {
            //      __instance.cost_resource_id_1 = SR.ale;
            //  }
            //  else
            //  {
            //      __instance.cost_resource_id_1 = pResourceID_1;
            //  }
            //  
            //  if (pResourceID_2 != "none")
            //  {
            //      __instance.cost_resource_id_2 = SR.ale;
            //  }
            //  else
            //  {
            //      __instance.cost_resource_id_2 = pResourceID_2;
            //  }
            //  
            //  __instance.cost_resource_1 = pCostResource_1;
            //  __instance.cost_resource_2 = pCostResource_2;
            //  return false;
        }

        //  public static bool update_Prefix(BuildingBiomeFoodProducer __instance, ref Building ___building, ref float ___timer, float pElapsed)
        //  {
        //      ListPool<WorldTile> tiles = (ListPool<WorldTile>)Reflection.GetField(___building.GetType(), ___building, "tiles");
        //  
        //      if (___building.city == null)
        //      {
        //          return false;
        //      }
        //      if (!___building.isUsable())
        //      {
        //          return false;
        //      }
        //      if (___timer > 0f)
        //      {
        //          ___timer -= pElapsed;
        //          return false;
        //      }
        //      ___timer = 90f;
        //      WorldTile random = tiles.GetRandom<WorldTile>();
        //      string food_resource = SR.ale;
        //      if (string.IsNullOrEmpty(food_resource))
        //      {
        //          food_resource = SR.ale;
        //      }
        //      if (string.IsNullOrEmpty(food_resource))
        //      {
        //          return false;
        //      }
        //      if (___building.city.data.storage.get(food_resource) >= 10)
        //      {
        //          return false;
        //      }
        //      ___building.city.data.storage.change(food_resource, 1);
        //      return false;
        //  }

        public static void change_Prefix(CityStorage __instance, ref int __result, string pRes, int pAmount = 1)
        {
            pRes = SR.ale;

            //  if (DebugConfig.isOn(DebugOption.CityInfiniteResources))
            //  {
            //      pAmount = 999;
            //  }
            //  int num;
            //  if (!__instance.resources.ContainsKey(pRes))
            //  {
            //      num = (int)Reflection.CallMethod(__instance, "addNew", pRes, pAmount);
            //  }
            //  else
            //  {
            //      CityStorageSlot cityStorageSlot = __instance.resources[pRes];
            //      cityStorageSlot.amount += pAmount;
            //      if (cityStorageSlot.amount > cityStorageSlot.asset.maximum)
            //      {
            //          __instance.resources[pRes].amount = cityStorageSlot.asset.maximum;
            //      }
            //      num = cityStorageSlot.amount;
            //  }
            //  __result = num;
            //  return false;
        }

        public static void addNew_Prefix(CityStorage __instance, ref int __result, string pResID, int pAmount)
        {
            pResID = SR.ale;

            //  CityStorageSlot cityStorageSlot = new CityStorageSlot();
            //  cityStorageSlot.id = pResID;
            //  cityStorageSlot.amount = pAmount;
            //  cityStorageSlot.create();
            //  Reflection.CallMethod(__instance, "putToDict", cityStorageSlot);
            //  __result = cityStorageSlot.amount;
            //  return false;
        }

        //  public static bool hasEnoughResourcesFor_Prefix(City __instance, ref bool __result, ConstructionCost pCost)
        //  {
        //      int beer = pCost.wood + pCost.common_metals + pCost.stone + pCost.gold;
        //  
        //      __result = DebugConfig.isOn(DebugOption.CityInfiniteResources) || (__instance.data.storage.g    et(SR.ale) >= beer);
        //      return false;
        //  }

        //  public static bool getCitizenJob_Prefix(City __instance, ref float ____timer_warrior, ref int ____last_checked_job_id, Actor pActor)
        //  {
        //      if (!__instance.isGettingCaptured() && ____timer_warrior <= 0f && (bool)Reflection.CallMethod(pActor, "isProfession", UnitProfession.Unit) && __instance.data.storage.get(SR.ale) > 10 && __instance.isEnoughFoodForArmy() && (bool)Reflection.CallMethod(__instance, "tryToMakeWarrior", pActor))
        //      {
        //          return false;
        //      }
        //      if ((bool)Reflection.CallMethod(__instance, "checkCitizenJobList", AssetManager.citizen_job_library.list_priority_high, pActor))
        //      {
        //          return false;
        //      }
        //      if (!__instance.hasAnyFood() && (bool)Reflection.CallMethod(__instance, "checkCitizenJobList", AssetManager.citizen_job_library.list_priority_high_food, pActor))
        //      {
        //          return false;
        //      }
        //      List<CitizenJobAsset> list_priority_normal = AssetManager.citizen_job_library.list_priority_normal;
        //      for (int i = 0; i < list_priority_normal.Count; i++)
        //      {
        //          ____last_checked_job_id++;
        //          if (____last_checked_job_id > list_priority_normal.Count - 1)
        //          {
        //              ____last_checked_job_id = 0;
        //          }
        //          CitizenJobAsset citizenJobAsset = list_priority_normal[____last_checked_job_id];
        //          if ((citizenJobAsset.ok_for_king || !pActor.isKing()) && (citizenJobAsset.ok_for_leader || !pActor.isCityLeader()) && (bool)Reflection.CallMethod(__instance, "checkCitizenJob", citizenJobAsset, __instance, pActor))
        //          {
        //              return false;
        //          }
        //      }
        //      return false;
        //  }

        //  public static bool getMaterialForItem_Prefix(CityStorage __instance, ref ItemAsset __result, ItemAsset pItemAsset, ItemAssetLibrary<ItemAsset> pLib, City pCity, bool pCheckCost = true)
        //  {
        //      Culture culture = pCity.getCulture();
        //      if (culture == null)
        //      {
        //          __result = null;
        //          return false;
        //      }
        //      for (int num = pLib.list.Count - 1; num >= 0; num--)
        //      {
        //          ItemAsset itemAsset = pLib.list[num];
        //          if (pItemAsset.materials.Contains(itemAsset.id) && (!pCheckCost || ((string.IsNullOrEmpty(itemAsset.tech_needed) || culture.hasTech(itemAsset.tech_needed)) && itemAsset.cost_gold <= __instance.get(SR.ale) && (!(itemAsset.cost_resource_id_1 != "none") || itemAsset.cost_resource_1 <= __instance.get(itemAsset.cost_resource_id_1)) && (itemAsset.minimum_city_storage_resource_1 == 0 || __instance.get(itemAsset.cost_resource_id_1) >= itemAsset.minimum_city_storage_resource_1) && (!(itemAsset.cost_resource_id_2 != "none") || itemAsset.cost_resource_2 <= __instance.get(itemAsset.cost_resource_id_2)))))
        //          {
        //              __result = itemAsset;
        //              return false;
        //          }
        //      }
        //      __result = null;
        //      return false;
        //  }

        // public static bool addToInventory_Prefix(Actor __instance, ref bool ___dirty_sprite_item, string pID, int pAmount)
        // {
        //     pID = SR.ale;
        // 
        //     __instance.inventory = __instance.inventory.add(pID, pAmount);
        //     if (__instance.asset.use_items)
        //     {
        //         ___dirty_sprite_item = true;
        //     }
        //     return false;
        // }

        public static void add_Prefix(this ActorBag pBag, string pID, int pAmount)
        {
            pID = SR.ale;
        }

        //  public static bool showResource_Prefix(Tooltip pTooltip, string pType, TooltipData pData = default(TooltipData))
        //  {
        //      try
        //      {
        //          ResourceAsset resource = pData.resource;
        //          pTooltip.name.text = LocalizedTextManager.getText(resource.id, null);
        //          Reflection.CallMethod(pTooltip, "addLineIntText", "amount", Config.selectedCity.data.storage.get(resource.id), null);
        //          if (resource.id == SR.ale)
        //          {
        //              Reflection.CallMethod(pTooltip, "addItemText", "yearly_gain", (float)Config.selectedCity.gold_change, false, true, true, "#43FF43", false);
        //              Reflection.CallMethod(pTooltip, "addLineBreak", "");
        //              Reflection.CallMethod(pTooltip, "addItemText", "tax", (float)Config.selectedCity.gold_in_tax, false, true, true, "#43FF43", false);
        //              Reflection.CallMethod(pTooltip, "addLineBreak", "---");
        //              if (Config.selectedCity.gold_out_army != 0)
        //              {
        //                  Reflection.CallMethod(pTooltip, "addItemText", "upkeep_army", (float)(-(float)Config.selectedCity.gold_out_army), false, true, true, "#43FF43", false);
        //              }
        //              if (Config.selectedCity.gold_out_buildings != 0)
        //              {
        //                  Reflection.CallMethod(pTooltip, "addItemText", "upkeep_buildings", (float)(-(float)Config.selectedCity.gold_out_buildings), false, true, true, "#43FF43", false);
        //              }
        //              if (Config.selectedCity.gold_out_homeless != 0)
        //              {
        //                  Reflection.CallMethod(pTooltip, "addItemText", "upkeep_homeless", (float)(-(float)Config.selectedCity.gold_out_homeless), false, true, true, "#43FF43", false);
        //              }
        //          }
        //      }
        //      catch (Exception)
        //      {
        //          Debug.Log("0?? " + ScrollWindow.currentWindows[0].screen_id);
        //          string str = "1?? ";
        //          ResourceAsset resource2 = pData.resource;
        //          Debug.Log(str + ((resource2 != null) ? resource2.ToString() : null) == null);
        //          string str2 = "2?? ";
        //          ResourceAsset resource3 = pData.resource;
        //          Debug.Log(str2 + ((resource3 != null) ? resource3.id : null));
        //          throw;
        //      }
        //      return false;
        //  }

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

            // if (!__instance.resources.ContainsKey(pRes))
            // {
            //     __result = 0;
            //     return false;
            // }
            // __result = __instance.resources[pRes].amount;
            // return false;
        }
    }
}
