using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(MovingPlatformData), "Moving Platform", Category = "Advanced")]
    public sealed class EditorMovingPlatform : MovingPlatform
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
            instance.GetComponentInChildren<MoveSequence>().enabled = false;
        }
    }
}