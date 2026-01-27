using UnityEngine;
using System;
using UnboundLib;
using MapsExt.Properties;

namespace VanillaMapObjects.MapObjectProperties
{
    public class SpringyRopePositionProperty : IProperty, IEquatable<SpringyRopePositionProperty>
    {
        [SerializeField] private Vector2 _pos1;
        [SerializeField] private Vector2 _pos2;

        public Vector2 StartPosition { get => this._pos1; set => this._pos1 = value; }
        public Vector2 EndPosition { get => this._pos2; set => this._pos2 = value; }

        public SpringyRopePositionProperty()
        {
            var pos = (Vector2)MainCam.instance.cam.transform.position;
            this._pos1 = pos + Vector2.up;
            this._pos2 = pos + Vector2.down;
        }

        public SpringyRopePositionProperty(Vector2 startPosition, Vector2 endPosition)
        {
            this._pos1 = startPosition;
            this._pos2 = endPosition;
        }

        public bool Equals(SpringyRopePositionProperty other) =>
            this.StartPosition.Equals(other.StartPosition) && this.EndPosition.Equals(other.EndPosition);
        public override bool Equals(object other) => other is SpringyRopePositionProperty prop && this.Equals(prop);
        public override int GetHashCode() => (this.StartPosition, this.EndPosition).GetHashCode();

        public static bool operator ==(SpringyRopePositionProperty a, SpringyRopePositionProperty b) => a.Equals(b);
        public static bool operator !=(SpringyRopePositionProperty a, SpringyRopePositionProperty b) => !a.Equals(b);
    }

    [PropertySerializer(typeof(SpringyRopePositionProperty))]
    public class RopePositionPropertySerializer : IPropertyWriter<SpringyRopePositionProperty>
    {
        public virtual void WriteProperty(SpringyRopePositionProperty property, GameObject target)
        {
            target.transform.position = property.StartPosition;
            target.transform.GetChild(0).position = property.EndPosition;

            var rope = target.GetComponent<MapObjet_Rope>();
            rope.OnJointAdded(joint =>
            {
                var springJoint = joint as SpringJoint2D;
                if (springJoint)
                {
                    rope.ExecuteAfterFrames(1, () => springJoint.autoConfigureDistance = false);
                }
            });
        }
    }
}