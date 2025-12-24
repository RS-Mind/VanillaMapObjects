using System;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using MapsExt;
using UnboundLib;

namespace VanillaMapObjectsEditor
{
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("io.olavim.rounds.mapsextended")]
    [BepInDependency("io.olavim.rounds.mapsextended.editor")]
    [BepInDependency("com.rsmind.rounds.vanillamapobjects")]
    [BepInPlugin(ModId, ModName, Version)]
    [BepInProcess("Rounds.exe")]
    public class VanillaMapObjectsEditor : BaseUnityPlugin
    {
        private const string ModId = "com.rsmind.rounds.vanillamapobjectseditor";
        private const string ModName = "Vanilla Map Objects Editor";
        public const string Version = "1.0.0";

        public static VanillaMapObjectsEditor instance { get; private set; }

        void Awake()
        {
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            instance = this;
        }
    }
}