using MapsExt.Editor.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjects;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(WreckingBallData), "Wrecking Ball", Category = "Advanced")]
    public sealed class EditorWreckingBall : WreckingBall
    {
        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
        }
    }
}
