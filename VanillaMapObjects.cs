using BepInEx;
using HarmonyLib;
using Jotunn.Utils;
using UnityEngine;

namespace VanillaMapObjects
{
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("io.olavim.rounds.mapsextended")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class VanillaMapObjects : BaseUnityPlugin
    {
        private const string ModId = "com.rsmind.rounds.vanillamapobjects";
        private const string ModName = "VanillaMapObjects";
        public const string Version = "1.1.0";

        public static VanillaMapObjects instance { get; private set; }

        public static AssetBundle MapObjectAssets = AssetUtils.LoadAssetBundleFromResources("smashers", typeof(VanillaMapObjects).Assembly);

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            instance = this;
        }

        [HarmonyPatch(typeof(MapObjet_Rope), "AddJoint")]
        static class RopePatch_AddJoint
        {
            public static void Postfix(MapObjet_Rope __instance, SpringJoint2D ___joint)
            {
                __instance.JointAdded(___joint);
            }
        }
    }
}