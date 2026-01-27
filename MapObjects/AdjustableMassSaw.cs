using MapsExt;
using MapsExt.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class AdjustableMassSawData : SawDynamicData
    {
        [SerializeField] private float _mass;

        public MassProperty Mass { get => new MassProperty(this._mass); set => this._mass = value.Value; }

        public AdjustableMassSawData()
        {
            this.Mass = new MassProperty(500f);
        }
    }

    [MapObject(typeof(AdjustableMassSawData))]
    public class AdjustableMassSaw : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Saw Dynamic");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
