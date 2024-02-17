﻿using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace TheRedPlague.Patches;

[HarmonyPatch(typeof(Player))]
public static class BrokenPdaPatch
{
    [HarmonyPatch(nameof(Player.Start))]
    [HarmonyPostfix]
    public static void SpawnGlassCracksPatch(Player __instance)
    {
        UWE.CoroutineHost.StartCoroutine(SpawnGlassCracks(__instance.transform.Find(
            "body/player_view/export_skeleton/head_rig/neck/chest/clav_L/clav_L_aim/shoulder_L/elbow_L/hand_L/attachL/PlayerPDA")));
    }

    private static IEnumerator SpawnGlassCracks(Transform pda)
    {
        var task = CraftData.GetPrefabForTechTypeAsync(TechType.Seamoth);
        yield return task;
        var seamoth = task.GetResult();
        var glassCracks = Object.Instantiate(seamoth.transform
            .Find("SeamothDamageFXSpawn").gameObject.GetComponent<PrefabSpawn>().prefab.transform
            .Find("x_SeamothGlassCracks"), pda, true);
        glassCracks.transform.localPosition = Vector3.zero;
        glassCracks.transform.localEulerAngles = Vector3.up * 270;
        glassCracks.GetComponent<Renderer>().material.color = new Color(0.113505f, 0.066081f, 0.691189f, 1f);
    }
}