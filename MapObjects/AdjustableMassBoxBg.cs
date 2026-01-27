using MapsExt;
using MapsExt.MapObjects;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class AdjustableMassBoxBgData : BoxData
    {
        [SerializeField] private float _mass;

        public MassProperty Mass { get => new MassProperty(this._mass); set => this._mass = value.Value; }

        public AdjustableMassBoxBgData()
        {
            this.Mass = new MassProperty(20f);
        }
    }

    [MapObject(typeof(AdjustableMassBoxBgData))]
    public class AdjustableMassBoxBg : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box Background");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
