using HarmonyLib;
using UnityEngine.UI;

namespace SaveMyEyes;

[HarmonyPatch]
public static class ChemScannerScrollPatch
{
    private static ScrollRect _scroll;

    [HarmonyPatch(typeof(ChemScannerUIManager), "Awake")]
    [HarmonyPostfix]
    public static void AwakePostfix(ChemScannerUIManager __instance)
    {
        _scroll = ScanWindowScrollFix.Prepare(__instance.contaminantTMP);
    }

    [HarmonyPatch(typeof(ChemScannerUIManager), nameof(ChemScannerUIManager.Open))]
    [HarmonyPostfix]
    public static void OpenPostfix(ChemScannerUIManager __instance)
    {
        ScanWindowScrollFix.Wrap(_scroll, __instance.contaminantTMP);
    }
}
