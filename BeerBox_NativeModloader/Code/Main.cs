using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BeerBox_NativeModloader
{
    internal class Main : MonoBehaviour
    {
        public static Harmony harmony = new Harmony(MethodBase.GetCurrentMethod().DeclaringType.Namespace);
        private bool _initialized = false;

        public void Update()
        {
            if (global::Config.gameLoaded && !_initialized)
            {
                harmony.Patch(AccessTools.Method(typeof(BuildingLibrary), nameof(BuildingLibrary.addResource)),
                    prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.addResource_Prefix))));

                harmony.Patch(AccessTools.Method(typeof(ItemAsset), nameof(ItemAsset.setCost)),
                    prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.setCost_Prefix))));

                harmony.Patch(AccessTools.Method(typeof(CityStorage), nameof(CityStorage.change)),
                    prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.change_Prefix))));

                harmony.Patch(AccessTools.Method(typeof(CityStorage), nameof(CityStorage.addNew)),
                    prefix: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.addNew_Prefix))));

                harmony.Patch(AccessTools.Method(typeof(TooltipLibrary), nameof(TooltipLibrary.showResource)),
                    transpiler: new HarmonyMethod(AccessTools.Method(typeof(Patches), nameof(Patches.showResource_Transpiler))));

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

                _initialized = true;
            }
        }

        public static void beerLocalization(string planguage, string id, string name)
        {
            if (planguage == LocalizedTextManager.instance.language)
            {
                Dictionary<string, string> localizedText = LocalizedTextManager.instance.localizedText;
                localizedText.Remove("ale");
                localizedText.Add(id, name);
            }
        }
    }
}
