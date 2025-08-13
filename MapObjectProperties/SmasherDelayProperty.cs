using MapsExt.Properties;
using System;
using UnboundLib;
using UnityEngine;

namespace VanillaMapObjects.MapObjectProperties
{
    public class SmasherDelayProperty : ValueProperty<float>, IEquatable<SmasherDelayProperty>
    {
        [SerializeField] private readonly float _delay;

        public override float Value => this._delay;

        public SmasherDelayProperty()
        {
            this._delay = 0f;
        }

        public SmasherDelayProperty(float delay)
        {
            this._delay = delay;
        }

        public bool Equals(SmasherDelayProperty other) => this.Value.Equals(other.Value);
        public override bool Equals(object other) => other is SmasherDelayProperty prop && this.Equals(prop);
        public override int GetHashCode() => this.Value.GetHashCode();

        public static bool operator ==(SmasherDelayProperty a, SmasherDelayProperty b) => a.Equals(b);
        public static bool operator !=(SmasherDelayProperty a, SmasherDelayProperty b) => !a.Equals(b);
    }

    [PropertySerializer(typeof(SmasherDelayProperty))]
    public class SmasherDelayPropertySerializer : IPropertyWriter<SmasherDelayProperty>
    {
        public virtual void WriteProperty(SmasherDelayProperty property, GameObject target)
        {
            //if (target.GetComponentInChildren<DelayEvent>() is DelayEvent delayEvent)
            //    delayEvent.time = property.Value;
        }
    }
}
