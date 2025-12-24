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
    public class SmasherData : SpatialMapObjectData
    {
        [SerializeField] private float _delay;

        public DelayProperty Delay { get => new DelayProperty(this._delay); set => this._delay = value.Value; }

        public SmasherData()
        {
            this.Delay = new DelayProperty();
        }
    }

    [MapObject(typeof(SmasherData))]
    public class Smasher : IMapObject
    {
        public virtual GameObject Prefab => VanillaMapObjects.MapObjectAssets.LoadAsset<GameObject>("smasher 2.0");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
