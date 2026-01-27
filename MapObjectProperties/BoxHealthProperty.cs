using MapsExt.Properties;
using UnboundLib;
using UnityEngine;

namespace VanillaMapObjects.MapObjectProperties
{
    public class BoxHealthProperty : ValueProperty<float>, ILinearProperty<BoxHealthProperty>
    {
        [SerializeField] private readonly float _delay;

        public override float Value => this._delay;

        public BoxHealthProperty() { }

        public BoxHealthProperty(float health)
        {
            this._delay = health;
        }

        public override bool Equals(ValueProperty<float> other) => base.Equals(other) || this.Value == other.Value;
        public BoxHealthProperty Lerp(BoxHealthProperty end, float t) => new BoxHealthProperty(Mathf.Lerp(this.Value, end.Value, t));
        public IProperty Lerp(IProperty end, float t) => this.Lerp((BoxHealthProperty)end, t);

        public static implicit operator BoxHealthProperty(float delay) => new BoxHealthProperty(delay);
        public static implicit operator float(BoxHealthProperty prop) => prop.Value;

        public static BoxHealthProperty operator +(BoxHealthProperty a, BoxHealthProperty b) => a.Value + b.Value;
        public static BoxHealthProperty operator -(BoxHealthProperty a, BoxHealthProperty b) => a.Value - b.Value;
    }

    [PropertySerializer(typeof(BoxHealthProperty))]
    public class BoxHealthPropertySerializer : IPropertyWriter<BoxHealthProperty>
    {
        public virtual void WriteProperty(BoxHealthProperty property, GameObject target)
        {
            target.GetOrAddComponent<BoxHealthPropertyInstance>().Health = property;
            var damageEvent = target.GetComponent<DamagableEvent>()
                ?? throw new System.ArgumentException("GameObject does not have a damageable script", nameof(target));
            damageEvent.maxHP = property.Value;
            damageEvent.currentHP = property.Value;
        }
    }
    public class BoxHealthPropertyInstance : MonoBehaviour
    {
        public BoxHealthProperty Health { get; set; } = new BoxHealthProperty();
    }
}
