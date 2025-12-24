using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(SmasherData), "Smasher", Category = "Advanced")]
    public sealed class EditorSmasher : Smasher
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            instance.GetComponentInChildren<CurveAnimation>().enabled = false;
            instance.GetComponentInChildren<DelayEvent>().enabled = false;
            instance.transform.Find("Anim").localScale = new Vector3(1f, 8f, 1f);
        }
    }
}