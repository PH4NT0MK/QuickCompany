using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx.Logging;
using GameNetcodeStuff;
using HarmonyLib;
using UnityEngine;

namespace QuickCompany.Patches
{
    [HarmonyPatch(typeof(ItemDropship))]
    internal class ItemDropshipPatch
    {

        internal static ManualLogSource mls;

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void patchUpdate(ref float ___shipTimer, ref bool ___deliveringOrder, ref bool ___playersFirstOrder, ref bool ___shipDoorsOpened)
        {
            mls = BepInEx.Logging.Logger.CreateLogSource("ItemDropshipPatch");

            //___playersFirstOrder = true;

            if (!___deliveringOrder)
            {
                if (___playersFirstOrder)
                {
                    ___playersFirstOrder = false;
                    ___shipTimer = 41f;
                }
            }

            if (___shipDoorsOpened)
            {
                ___shipTimer = 31f;
                ___playersFirstOrder = true;
            }

            mls.LogInfo(___shipTimer);
        }
    }
}
