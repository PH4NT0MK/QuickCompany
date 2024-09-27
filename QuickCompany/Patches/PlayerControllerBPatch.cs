using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using GameNetcodeStuff;
using UnityEngine;

namespace QuickCompany.Patches
{
    [HarmonyPatch(typeof(PlayerControllerB))]
    internal class PlayerControllerBPatch
    {
        private const int sprintExtraMultiplier = 2;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void patchUpdate(ref float ___sprintMeter, ref float ___sprintMultiplier, ref bool ___isWalking, ref bool ___isSprinting)
        {
            ___sprintMeter = 1f;
            
            if(___isWalking)
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