using MapsExt;
using MapsExt.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class AdjustableMassBoxData : BoxData
    {
        [SerializeField] private float _mass;

        public MassProperty Mass { get => new MassProperty(this._mass); set => this._mass = value.Value; }

        public AdjustableMassBoxData()
        {
            this.Mass = new MassProperty(20f);
        }
    }

    [MapObject(typeof(AdjustableMassBoxData))]
    public class AdjustableMassBox : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
