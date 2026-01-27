using MapsExt;
using MapsExt.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class AdjustableHealthBoxData : BoxDestructibleData
    {
        [SerializeField] private float _health;
        [SerializeField] private float _mass;

        public BoxHealthProperty Health { get => new BoxHealthProperty(this._health); set => this._health = value.Value; }
        public MassProperty Mass { get => new MassProperty(this._mass); set => this._mass = value.Value; }

        public AdjustableHealthBoxData()
        {
            this.Health = new BoxHealthProperty(100f);
            this.Mass = new MassProperty(20f);
        }
    }

    [MapObject(typeof(AdjustableHealthBoxData))]
    public class AdjustableHealthBox : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box Destructible");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
