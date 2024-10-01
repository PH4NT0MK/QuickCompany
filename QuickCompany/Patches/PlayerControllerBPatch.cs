using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Logging;
using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine;

namespace QuickCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        private const int sprintExtraMultiplier = 2;
        private static float previousSprintMeter = 1f;

        internal static ManualLogSource mls;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void patchUpdate(ref float ___sprintMeter, ref float ___sprintMultiplier, ref bool ___isWalking, ref bool ___isSprinting)
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("PlayerControllerBPatch");

            if (___sprintMeter > previousSprintMeter)
            {
                ___sprintMeter = ___sprintMeter + (___sprintMeter - previousSprintMeter) / 10;
            }
            //else
            //{
            //    ___sprintMeter = ___sprintMeter + (previousSprintMeter - ___sprintMeter) / 10;
            //}

            previousSprintMeter = ___sprintMeter;

            //mls.LogInfo(___sprintMeter + ' ' + previousSprintMeter);

            if (___isWalking)
            {
                if(___isSprinting)
                {
                    ___sprintMultiplier = Mathf.Lerp(___sprintMultiplier, 2.25f * sprintExtraMultiplier, Time.deltaTime * 1f);
                } else
                {
                    ___sprintMultiplier = Mathf.Lerp(___sprintMultiplier, 1f * sprintExtraMultiplier, 10f * Time.deltaTime);
                }
            }
        }
    }
}