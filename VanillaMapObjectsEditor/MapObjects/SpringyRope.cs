using MapsExt;
using MapsExt.Editor.MapObjects;
using MapsExt.Visualizers;
using System.Collections.Generic;
using System.Linq;
using UnboundLib;
using UnityEngine;
using VanillaMapObjects.MapObjects;
using VanillaMapObjectsEditor.Visualizers;

namespace VanillaMapObjectsEditor.MapObjects
{
    [EditorMapObject(typeof(SpringyRopeData), "Springy Rope", Category = "Advanced")]
    public sealed class EditorSpringyRope : SpringyRope
    {
        public class RopeInstance : MonoBehaviour
        {
            private List<MapObjectAnchor> _anchors;

            protected virtual void Awake()
            {
                this._anchors = this.gameObject.GetComponentsInChildren<MapObjectAnchor>().ToList();
            }

            public MapObjectAnchor GetAnchor(int index)
            {
                return this._anchors[index];
            }
        }

        public override GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Editor Rope");

        public override void OnInstantiate(GameObject instance)
        {
            base.OnInstantiate(instance);
            var ropeInstance = instance.GetOrAddComponent<RopeInstance>();
            ropeInstance.GetAnchor(0).gameObject.GetOrAddComponent<MapObjectPart>();
            ropeInstance.GetAnchor(1).gameObject.GetOrAddComponent<MapObjectPart>();
            instance.GetOrAddComponent<SpringyRopeVisualizer>();
        }
    }
}
