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
            /*
            [CustomFont] UISkinManager.TMPFont List:
            [CustomFont] AssetName:"NotoSans-Regular SDF" FontName:"Noto Sans" FontAssetType:SDF Atlas:2048x2048 Count:1139
            [CustomFont]  AssetName:"NotoSansCJK-K01-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5593
            [CustomFont]  AssetName:"NotoSansCJK-K02-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5593
            [CustomFont]  AssetName:"NotoSansCJK01-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5260
            [CustomFont]  AssetName:"NotoSansCJK02-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5235
            [CustomFont]  AssetName:"NotoSansCJK03-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5235
            [CustomFont]  AssetName:"NotoSansCJK04-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:4096x4096 Count:5235
            [CustomFont]  AssetName:"NotoSansCJK-J-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:1024x1024 Count:260
            [CustomFont]  AssetName:"NotoSansCJK-Jext-Regular SDF" FontName:"Noto Sans CJK TC" FontAssetType:SDF Atlas:1024x512 Count:145
            */

            FontLoader fontLoader = GameObject.FindObjectOfType<FontLoader>();

            Log("FontLoader List: Count={0}", fontLoader.LoadedFonts.Count);
            foreach (var f in fontLoader.LoadedFonts)
            {
                PrintFontInfo(f);
            }
            /*
            [CustomFont] FontLoader List: Count=3
            [CustomFont] AssetName:"NotoSansJP-RegularC2 SDF" FontName:"Noto Sans JP Regular" FontAssetType:SDF Atlas:4096x4096 Count:3340
            [CustomFont] AssetName:"NotoSansJP-RegularC1 SDF" FontName:"Noto Sans JP Regular" FontAssetType:SDF Atlas:4096x4096 Count:4256
            [CustomFont] AssetName:"NotoEmoji-Regular SDF" FontName:"Noto Emoji" FontAssetType:SDF Atlas:1024x512 Count:125
            */

            //*
            TMP_FontAsset[] fonts = fontLoader.LoadedFonts.ToArray();

            //stock font set: en-us es-es ru ko ja zh-cn zh-tw
            fontLoader.AddGameSubFont("ja", false, fonts);
            fontLoader.AddGameSubFont("zh-cn", false, fonts);
            fontLoader.AddGameSubFont("zh-tw", false, fonts);
            fontLoader.AddGameSubFont("ko", false, fonts);

            KSP.Localization.Localizer.SwitchToLanguage("ja");  // Update Localization engine
            fontLoader.SwitchFontLanguage("ja");                // Update Main Menu Font
            /**/
        }

        private void PrintFontInfo(TMP_FontAsset font, string offset = "")
        {
            if (font == null)
            {
                Log("TMP_FontAsset is null");
                return;
            }

            Log(offset + $"AssetName:\"{font.name}\" FontName:\"{font.fontInfo.Name}\" FontAssetType:{font.fontAssetType} Atlas:{font.atlas.width}x{font.atlas.height} Count:{font.characterDictionary.Count}");

            // fallbackFontAssetsに対し再帰処理
            foreach (var f in font.fallbackFontAssets)
            {
                PrintFontInfo(f, " " + offset);
            }
        }
    }

    /*
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class CustomFontTest : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            ScreenMessages.PostScreenMessage("1234567890ABCDEFGHIJKLMabcdefghijklm\n飴鰯葛祇経削鯖終噌蛸直辻道認箸庖覧\nあのイーハトーヴォのすきとほつた風、\n夏でも底に冷たさをもつ青いそら、\nうつくしい森で飾られたモーリオ市、\n郊外のぎらぎらひかる草の波。", 0);
        }
    }
    /**/
}
