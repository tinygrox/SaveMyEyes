using HarmonyLib;
using UnityEngine.UI;

namespace SaveMyEyes;

[HarmonyPatch]
public static class WaterScannerScrollPatch
{
    private static ScrollRect _scroll;

    [HarmonyPatch(typeof(WaterScannerUIManager), "Awake")]
    [HarmonyPostfix]
    public static void AwakePostfix(WaterScannerUIManager __instance)
    {
        _scroll = ScanWindowScrollFix.Prepare(__instance.contaminantTMP);
    }

    [HarmonyPatch(typeof(WaterScannerUIManager), nameof(WaterScannerUIManager.Open))]
    [HarmonyPostfix]
    public static void OpenPostfix(WaterScannerUIManager __instance)
    {
        ScanWindowScrollFix.Wrap(_scroll, __instance.contaminantTMP);
    }
}
