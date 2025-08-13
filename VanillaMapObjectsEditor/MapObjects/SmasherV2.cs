using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(SmasherV2Data), "Smasher v2", Category = "Smashers")]
    public sealed class EditorSmasherV2 : SmasherV2
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
        }
    }
}
