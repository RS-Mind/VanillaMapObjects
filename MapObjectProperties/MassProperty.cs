using MapsExt.Properties;
using System;
using UnboundLib;
using UnityEngine;

namespace VanillaMapObjects.MapObjectProperties
{
    public class MassProperty : ValueProperty<float>, ILinearProperty<MassProperty>
    {
        [SerializeField] private readonly float _mass;

        public override float Value => this._mass;

        public MassProperty() { }

        public MassProperty(float mass)
        {
            this._mass = mass;
        }

        public override bool Equals(ValueProperty<float> other) => base.Equals(other) || this.Value == other.Value;
        public MassProperty Lerp(MassProperty end, float t) => new MassProperty(Mathf.Lerp(this.Value, end.Value, t));
        public IProperty Lerp(IProperty end, float t) => this.Lerp((MassProperty)end, t);

        public static implicit operator MassProperty(float mass) => new MassProperty(mass);
        public static implicit operator float(MassProperty prop) => prop.Value;

        public static MassProperty operator +(MassProperty a, MassProperty b) => a.Value + b.Value;
        public static MassProperty operator -(MassProperty a, MassProperty b) => a.Value - b.Value;
    }

    [PropertySerializer(typeof(MassProperty))]
    public class MassPropertySerializer : IPropertyWriter<MassProperty>
    {
        public virtual void WriteProperty(MassProperty property, GameObject target)
        {
            target.GetOrAddComponent<MassPropertyInstance>().Mass = property;
            var rigidbody = target.GetComponent<Rigidbody2D>()
                ?? throw new System.ArgumentException("GameObject does not have a rigidbody script", nameof(target));
            rigidbody.mass = property.Value * 1000;
        }
    }
    public class MassPropertyInstance : MonoBehaviour
    {
        public MassProperty Mass { get; set; } = new MassProperty();
    }
}
