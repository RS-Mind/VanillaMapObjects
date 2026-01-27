using MapsExt;
using MapsExt.MapObjects;
using MapsExt.Properties;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class SpringyRopeData : MapObjectData
    {
        [SerializeField] private Vector2 _pos1;
        [SerializeField] private Vector2 _pos2;
        [SerializeField] private float _frequency;
        [SerializeField] private float _damping;

        public SpringyRopePositionProperty Position
        {
            get => new SpringyRopePositionProperty(this._pos1, this._pos2);
            set { this._pos1 = value.StartPosition; this._pos2 = value.EndPosition; }
        }

        public SpringyRopeAttributesProperty Attributes
        {
            get => new SpringyRopeAttributesProperty(this._frequency, this._damping);
            set { this._frequency = value.Frequency; this._damping = value.DampingRatio; }
        }

        public SpringyRopeData()
        {
            this.Position = new SpringyRopePositionProperty();
            this.Attributes = new SpringyRopeAttributesProperty();
        }
    }

    [MapObject(typeof(SpringyRopeData))]
    public class SpringyRope : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Rope");

        public virtual void OnInstantiate(GameObject instance)
        {
            instance.GetComponent<MapObjet_Rope>().jointType = MapObjet_Rope.JointType.spring;
        }
    }
}
