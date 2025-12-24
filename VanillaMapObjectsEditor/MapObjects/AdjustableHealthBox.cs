using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(AdjustableHealthBoxData), "Destructible Box", Category = "Advanced")]
    public sealed class EditorAdjustableHealthBox : AdjustableHealthBox
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
        }
    }
}
