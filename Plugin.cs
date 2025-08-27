using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace Accurate_IGT_B5;

[BepInPlugin("com.dubblebrick.tr3.accurateigt", MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("TheRoomThree.exe")]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;

    public static bool CurrentlyLoading = false;
    public static float TotalLoadTime = 0;
        
    private void Awake()
    {
        // Plugin startup logic
        Logger = base.Logger;
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

        Harmony.CreateAndPatchAll(typeof(HarmonyXPlugins));
    }

    public static void Log(string msg)
    {
        Logger.LogInfo(msg);
    }
}

class HarmonyXPlugins
{
    [HarmonyPatch(typeof(LoadingScreen), "FadeIn")]
    [HarmonyPostfix]
    static void LoadingFadeIn()
    {
        Plugin.CurrentlyLoading = true;
        Plugin.Log("Game is now loading.");
    }

    [HarmonyPatch(typeof(LoadingScreen), "FadeOut")]
    [HarmonyPostfix]
    static void LoadingFadeOut()
    {
        Plugin.CurrentlyLoading = false;
        Plugin.Log("Game is no longer loading.");
    }

    [HarmonyPatch(typeof(SlotManager), "IncrementActiveSlotTotalPlayedTime")]
    [HarmonyPrefix]
    static bool IncrementTimePrefix()
    {
        if (Plugin.CurrentlyLoading)
        {
            Plugin.TotalLoadTime += Time.deltaTime;
            return false;
        }
        return true;
    }

    [HarmonyPatch(typeof(GameCompleteMenu), "FadeInFinished")]
    [HarmonyPostfix]
    static void EndScreenPostfix()
    {
        Plugin.Log($"Total loading time: {Plugin.TotalLoadTime} seconds.");
    }
}
