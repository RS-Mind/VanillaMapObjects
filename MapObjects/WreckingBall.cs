using MapsExt.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class WreckingBallData : SpatialMapObjectData
    {
        [SerializeField] private float _mass;

        public MassProperty Mass { get => new MassProperty(this._mass); set => this._mass = value.Value; }

        public WreckingBallData()
        {
            this.Mass = new MassProperty(500f);
        }
    }

    [MapObject(typeof(WreckingBallData))]
    public class WreckingBall : IMapObject
    {
        public virtual GameObject Prefab => VanillaMapObjects.MapObjectAssets.LoadAsset<GameObject>("Ball_Big");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
