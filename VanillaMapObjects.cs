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
        public const string Version = "1.0.0";

        public static VanillaMapObjects instance { get; private set; }

        public static AssetBundle MapObjectAssets = AssetUtils.LoadAssetBundleFromResources("smashers", typeof(VanillaMapObjects).Assembly);
        public GameObject smasher1;
        public GameObject smasher2;

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();

            //MapObjectAssets = AssetUtils.LoadAssetBundleFromResources("smashers", typeof(VanillaMapObjects).Assembly);
        }

        void Start()
        {
            instance = this;
        }
    }
}