using MapsExt.Properties;
using UnboundLib;
using UnityEngine;

namespace VanillaMapObjects.MapObjectProperties
{
    public class MoveSequenceProperty : IProperty
    {
        [SerializeField] private Vector2[] _positions;
        [SerializeField] private float _spring;
        [SerializeField] private float _timeAtPos;

        public Vector2[] Positions { get => this._positions; set => this._positions = value; }
        public float Spring { get => this._spring; set => this._spring = value; }
        public float TimeAtPos { get => this._timeAtPos; set => this._timeAtPos = value; }

        public MoveSequenceProperty()
        {
            this._positions = new Vector2[2];
            this._spring = 500f;
            this._timeAtPos = 0f;
        }

        public MoveSequenceProperty(Vector2[] positions, float spring, float timeAtPos)
        {
            this._positions = positions;
            this._spring = spring;
            this._timeAtPos = timeAtPos;
        }
    }

    [PropertySerializer(typeof(MoveSequenceProperty))]
    public class MoveSequencePropertySerializer : IPropertyWriter<MoveSequenceProperty>
    {
        public virtual void WriteProperty(MoveSequenceProperty property, GameObject target)
        {
            target.GetOrAddComponent<MoveSequencePropertyInstance>().Value = property;
            var moveSequence = target.GetComponent<MoveSequence>()
                ?? throw new System.ArgumentException("GameObject does not have a move sequence script", nameof(target));
            moveSequence.positions = property.Positions;
            moveSequence.spring = property.Spring;
            moveSequence.timeAtPos = property.TimeAtPos;
        }
    }
    public class MoveSequencePropertyInstance : MonoBehaviour
    {
        public MoveSequenceProperty Value { get; set; } = new MoveSequenceProperty();
    }
}
