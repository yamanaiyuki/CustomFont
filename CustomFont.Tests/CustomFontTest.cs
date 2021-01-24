using UnityEngine;

namespace CustomFont.Tests
{
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
}
