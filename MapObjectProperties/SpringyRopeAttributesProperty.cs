using UnityEngine;
using System;
using UnboundLib;
using MapsExt.Properties;

namespace VanillaMapObjects.MapObjectProperties
{
    public class SpringyRopeAttributesProperty : IProperty, IEquatable<SpringyRopeAttributesProperty>
    {
        [SerializeField] private float _frequency;
        [SerializeField] private float _damping;

        public float Frequency { get => this._frequency; set => this._frequency = value; }
        public float DampingRatio { get => this._damping; set => this._damping = value; }

        public SpringyRopeAttributesProperty()
        {
            this._frequency = 1.0f;
            this._damping = 0.0f;
        }

        public SpringyRopeAttributesProperty(float frequency, float damping)
        {
            this._frequency = frequency;
            this._damping = damping;
        }

        public bool Equals(SpringyRopeAttributesProperty other) =>
            this.Frequency == other.Frequency && this.DampingRatio == other.DampingRatio;
        public override bool Equals(object other) => other is SpringyRopeAttributesProperty prop && this.Equals(prop);
        public override int GetHashCode() => (this.Frequency, this.DampingRatio).GetHashCode();

        public static bool operator ==(SpringyRopeAttributesProperty a, SpringyRopeAttributesProperty b) => a.Equals(b);
        public static bool operator !=(SpringyRopeAttributesProperty a, SpringyRopeAttributesProperty b) => !a.Equals(b);
    }

    [PropertySerializer(typeof(SpringyRopeAttributesProperty))]
    public class SpringyRopeAttributesPropertySerializer : IPropertyWriter<SpringyRopeAttributesProperty>
    {
        public virtual void WriteProperty(SpringyRopeAttributesProperty property, GameObject target)
        {
            target.GetOrAddComponent<SpringyRopeAttributesPropertyInstance>().Attributes = property;
            var rope = target.GetComponent<MapObjet_Rope>();
            rope.OnJointAdded(joint =>
            {
                var springJoint = joint as SpringJoint2D;
                if (springJoint)
                {
                    rope.ExecuteAfterFrames(1, () =>
                    {
                        springJoint.frequency = property.Frequency;
                        springJoint.dampingRatio = property.DampingRatio;
                    });
                }
            });
        }
    }
    public class SpringyRopeAttributesPropertyInstance : MonoBehaviour
    {
        public SpringyRopeAttributesProperty Attributes { get; set; } = new SpringyRopeAttributesProperty();
    }
}