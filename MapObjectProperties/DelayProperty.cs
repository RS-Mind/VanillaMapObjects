using MapsExt.Properties;
using System;
using UnboundLib;
using UnityEngine;

namespace VanillaMapObjects.MapObjectProperties
{
    public class DelayProperty : ValueProperty<float>, ILinearProperty<DelayProperty>
    {
        [SerializeField] private readonly float _delay;

        public override float Value => this._delay;

        public DelayProperty() { }

        public DelayProperty(float delay)
        {
            this._delay = delay;
        }

        public override bool Equals(ValueProperty<float> other) => base.Equals(other) || this.Value == other.Value;
        public DelayProperty Lerp(DelayProperty end, float t) => new DelayProperty(Mathf.Lerp(this.Value, end.Value, t));
        public IProperty Lerp(IProperty end, float t) => this.Lerp((DelayProperty)end, t);

        public static implicit operator DelayProperty(float delay) => new DelayProperty(delay);
        public static implicit operator float(DelayProperty prop) => prop.Value;

        public static DelayProperty operator +(DelayProperty a, DelayProperty b) => a.Value + b.Value;
        public static DelayProperty operator -(DelayProperty a, DelayProperty b) => a.Value - b.Value;
    }

    [PropertySerializer(typeof(DelayProperty))]
    public class DelayPropertySerializer : IPropertyWriter<DelayProperty>
    {
        public virtual void WriteProperty(DelayProperty property, GameObject target)
        {
            target.GetOrAddComponent<DelayPropertyInstance>().Delay = property;
            var delayEvent = target.GetComponentInChildren<DelayEvent>()
                ?? throw new System.ArgumentException("GameObject does not have a delay script", nameof(target));
            delayEvent.time = property.Value;
        }
    }
    public class DelayPropertyInstance : MonoBehaviour
    {
        public DelayProperty Delay { get; set; } = new DelayProperty();
    }
}
