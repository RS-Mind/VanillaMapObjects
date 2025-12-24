using MapsExt;
using MapsExt.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnityEngine;
using UnityEngine.Events;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjects.MapObjects
{
    public class MovingPlatformData : SpatialMapObjectData
    {
        [SerializeField] private Vector2[] _positions;
        [SerializeField] private float _spring;
        [SerializeField] private float _timeAtPos;

        public MoveSequenceProperty Value
        {
            get => new MoveSequenceProperty(this._positions, this._spring, this._timeAtPos); set
            {
                this._positions = value.Positions;
                this._spring = value.Spring;
                this._timeAtPos = value.TimeAtPos;
            }
        }

        public MovingPlatformData()
        {
            this.Value = new MoveSequenceProperty();
        }
    }

    [MapObject(typeof(MovingPlatformData))]
    public class MovingPlatform : IMapObject
    {
        public virtual GameObject Prefab => VanillaMapObjects.MapObjectAssets.LoadAsset<GameObject>("MovingPlatform");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
