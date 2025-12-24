using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(AdjustableMassBoxData), "Variable Box", Category = "Advanced")]
    public sealed class EditorAdjustableMassBox: AdjustableMassBox
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
        }
    }
}
