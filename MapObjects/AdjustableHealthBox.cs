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
    public class AdjustableHealthBoxData : BoxDestructibleData
    {
        [SerializeField] private float _health;

        public BoxHealthProperty Health { get => new BoxHealthProperty(this._health); set => this._health = value.Value; }

        public AdjustableHealthBoxData()
        {
            this.Health = new BoxHealthProperty(100f);
        }
    }

    [MapObject(typeof(AdjustableHealthBoxData))]
    public class AdjustableHealthBox : IMapObject
    {
        public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box Destructible");

        public virtual void OnInstantiate(GameObject instance) { }
    }
}
