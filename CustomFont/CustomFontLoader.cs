using TMPro;
using UnityEngine;

namespace CustomFont
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class CustomFontLoader : MonoBehaviour
    {
        public static void Log(string s, params object[] m) => Debug.Log(string.Format("[CustomFont] " + s, m));

        void Awake()
        {
            Log("UISkinManager.TMPFont List:");
            PrintFontInfo(UISkinManager.TMPFont);   // KSP default font: Noto Sans

            FontLoader fontLoader = GameObject.FindObjectOfType<FontLoader>();
            if (fontLoader == null)
            {
                Log("FontLoader is null");
                return;
            }

            Log("FontLoader List: Count={0}", fontLoader.LoadedFonts.Count);
            foreach (var f in fontLoader.LoadedFonts)
            {
                PrintFontInfo(f);
            }

            TMP_FontAsset[] fonts = fontLoader.LoadedFonts.ToArray();

            // stock font set: en-us es-es ru ko ja zh-cn zh-tw
            fontLoader.AddGameSubFont("ja", false, fonts);
            fontLoader.AddGameSubFont("zh-cn", false, fonts);
            fontLoader.AddGameSubFont("zh-tw", false, fonts);
            fontLoader.AddGameSubFont("ko", false, fonts);

            KSP.Localization.Localizer.SwitchToLanguage("ja");  // Update Localization engine
            fontLoader.SwitchFontLanguage("ja");                // Update Main Menu Font
        }

        private void PrintFontInfo(TMP_FontAsset font, string offset = "")
        {
            if (font == null)
            {
                Log("TMP_FontAsset is null");
                return;
            }

            Log(offset + $"AssetName:'{font.name}' FontName:'{font.fontInfo.Name}' FontAssetType:{font.fontAssetType} Atlas:{font.atlas.width}x{font.atlas.height} Count:{font.characterDictionary.Count}");

            // fallbackFontAssetsに対し再帰処理
            foreach (var f in font.fallbackFontAssets)
            {
                PrintFontInfo(f, " " + offset);
            }
        }
    }
}
