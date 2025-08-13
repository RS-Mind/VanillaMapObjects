using MapsExt.MapObjects;
using UnityEngine;
using UnityEngine.Events;

namespace VanillaMapObjects.MapObjects
{
    public class SmasherV1Data : SpatialMapObjectData { }

    [MapObject(typeof(SmasherV1Data))]
    public class SmasherV1 : IMapObject
    {
        public virtual GameObject Prefab { 
            get { 
                GameObject prefab = VanillaMapObjects.instance.MapObjectAssets.LoadAsset<GameObject>("smasher");
                GameObject obj = new GameObject();
                prefab.transform.parent = obj.transform;
                prefab.transform.localPosition = Vector3.zero;
                return obj;
            } 
        }

        public virtual void OnInstantiate(GameObject instance) 
        {
            
        }
    }
}
