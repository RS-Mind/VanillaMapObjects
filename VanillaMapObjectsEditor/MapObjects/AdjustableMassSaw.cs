using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(AdjustableMassSawData), "Dynamic Saw", Category = "Advanced")]
    public sealed class EditorAdjustableMassSaw : AdjustableMassSaw
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
        }
    }
}
