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
    public class SmasherV2Data : SpatialMapObjectData
    {
        [SerializeField] private SmasherDelayProperty _delay = new SmasherDelayProperty();

        public SmasherDelayProperty Delay { get => this._delay; set => this._delay = value; }
    }

    [MapObject(typeof(SmasherV2Data))]
    public class SmasherV2 : IMapObject
    {
        public virtual GameObject Prefab
        {
            get
            {
                GameObject prefab = VanillaMapObjects.instance.MapObjectAssets.LoadAsset<GameObject>("smasher 2.0");

                CurveAnimation anim = prefab.GetComponentInChildren<CurveAnimation>();
                DelayEvent delayEvent = anim.gameObject.GetOrAddComponent<DelayEvent>();
                delayEvent.auto = true;
                delayEvent.repeating = false;
                delayEvent.usedTimeScale = true;
                delayEvent.delayedEvent = new UnityEvent();
                delayEvent.delayedEvent.AddListener(anim.PlayIn);
                return prefab;
            }
}

public virtual void OnInstantiate(GameObject instance) { }
    }
}
