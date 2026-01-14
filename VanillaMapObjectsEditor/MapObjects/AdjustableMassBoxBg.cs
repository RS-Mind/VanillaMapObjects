using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(AdjustableMassBoxBgData), "Variable Box (Background)", Category = "Advanced")]
    public sealed class EditorAdjustableMassBoxBg: AdjustableMassBoxBg
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
        }
    }
}
