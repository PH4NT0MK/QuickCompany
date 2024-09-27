using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BepInEx;
using BepInEx.Logging;
using QuickCompany.Patches;
using HarmonyLib;

namespace QuickCompany
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class QuickCompanyBase : BaseUnityPlugin
    {
        private const string modGUID = "Phantom.QuickCompany";
        private const string modName = "QuickCompany";
        private const string modVersion = "0.0.2";

        private readonly Harmony harmony = new Harmony(modGUID);
        private static QuickCompanyBase Instance;
        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(modGUID);

            mls.LogInfo("Quick Company is working.");

            harmony.PatchAll(typeof(QuickCompanyBase));
            harmony.PatchAll(typeof(PlayerControllerBPatch));
        }
    }
}
