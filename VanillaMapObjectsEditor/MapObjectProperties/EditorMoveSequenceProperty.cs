using MapsExt;
using MapsExt.Editor;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Editor.Utils;
using MapsExt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using UnboundLib;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;
using VanillaMapObjects.MapObjects;
using VanillaMapObjectsEditor.MapObjects;

namespace VanillaMapObjectsEditor.MapObjectProperties
{
    [EditorPropertySerializer(typeof(MoveSequenceProperty))]
    internal class EditorMoveSequencePropertySerializer : MoveSequencePropertySerializer, IPropertyReader<MoveSequenceProperty>
    {
        public virtual MoveSequenceProperty ReadProperty(GameObject instance)
        {
            var positions = instance.GetComponent<MoveSequence>().positions;
            var spring = instance.GetComponent<MoveSequence>().spring;
            var timeAtPos = instance.GetComponent<MoveSequence>().timeAtPos;
            return new MoveSequenceProperty(positions, spring, timeAtPos);
        }

        public override void WriteProperty(MoveSequenceProperty property, GameObject target)
        {
            base.WriteProperty(property, target);
            target.GetOrAddComponent<Events.MoveSequenceHandler>();
        }
    }

    [InspectorElement(typeof(MoveSequenceProperty))]
    public class MoveSequenceElement : InspectorElement
    {
        private TextSliderInput _spring;
        private TextSliderInput _timeAtPos;
        private TextSliderInput _size;
        private List<Vector2Input> _input;
        private List<GameObject> _inputObjects;
        private GameObject menuInstance;
        private MoveSequenceProperty _property;

        public MoveSequenceProperty Value
        {
            get => new MoveSequenceProperty(this.Context.InspectorTarget.GetComponent<MoveSequence>().positions,
                this.Context.InspectorTarget.GetComponent<MoveSequence>().spring,
                this.Context.InspectorTarget.GetComponent<MoveSequence>().timeAtPos);
            set => this.HandleInputChange(value, ChangeType.ChangeEnd);
        }

        protected override GameObject GetInstance()
        {
            menuInstance = new GameObject("MoveSequenceGroup");
            var layoutGroup = menuInstance.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandWidth = true;

            var springInstance = GameObject.Instantiate(Assets.InspectorSliderInputPrefab, menuInstance.transform);
            var springInput = springInstance.GetComponent<InspectorSliderInput>();
            springInput.Label.text = "Spring";
            springInput.Input.MinValue = 0;
            springInput.Input.Slider.minValue = 100;
            springInput.Input.MaxValue = 1000;
            springInput.Input.Slider.maxValue = 1000;
            this._spring = springInput.Input;
            this._spring.OnChanged += (value, changeType) => this.HandleInputChange(new MoveSequenceProperty(this.Value.Positions, value, this.Value.TimeAtPos), changeType);

            var timeAtPosInstance = GameObject.Instantiate(Assets.InspectorSliderInputPrefab, menuInstance.transform);
            var timeAtPosInput = timeAtPosInstance.GetComponent<InspectorSliderInput>();
            timeAtPosInput.Label.text = "Time At Position";
            timeAtPosInput.Input.MinValue = 0;
            timeAtPosInput.Input.Slider.minValue = 0;
            timeAtPosInput.Input.MaxValue = 60;
            timeAtPosInput.Input.Slider.maxValue = 60;
            this._timeAtPos = timeAtPosInput.Input;
            this._timeAtPos.OnChanged += (value, changeType) => this.HandleInputChange(new MoveSequenceProperty(this.Value.Positions, this.Value.Spring, value), changeType);

            var sizeInstance = GameObject.Instantiate(Assets.InspectorSliderInputPrefab, menuInstance.transform);
            var sizeInput = sizeInstance.GetComponent<InspectorSliderInput>();
            sizeInput.Label.text = "Position Count";
            sizeInput.Input.MinValue = 2;
            sizeInput.Input.Slider.minValue = 2;
            sizeInput.Input.MaxValue = 16;
            sizeInput.Input.Slider.maxValue = 16;
            this._size = sizeInput.Input;
            this._size.OnChanged += (value, changeType) =>
            {
                Vector2[] vector = new Vector2[(int)Math.Round(value)];
                this.Value.Positions.Take((int)Math.Round(value)).ToArray().CopyTo(vector, 0);
                this.HandleInputChange(new MoveSequenceProperty(vector, this.Value.Spring, this.Value.TimeAtPos), changeType);
            };

            this._input = new List<Vector2Input>();
            this._inputObjects = new List<GameObject>();
            SetInputs();
            return menuInstance;
        }

        private void SetInputs()
        {
            for (int i = _inputObjects.Count - 1; i >= Value.Positions.Length; i--)
            {
                GameObject.Destroy(_inputObjects[i]);
                _inputObjects.RemoveAt(i);
                _input.RemoveAt(i);
            }
            for (int i = _inputObjects.Count; i < Value.Positions.Length; i++)
            {
                var posInstance = GameObject.Instantiate(Assets.InspectorVector2InputPrefab, menuInstance.transform);
                var input = posInstance.GetComponent<InspectorVector2Input>();
                input.Label.text = "Position " + (i + 1).ToString();
                this._input.Add(input.Input);
                int j = i;
                this._input[i].OnChanged += (value, changeType) =>
                {
                    Vector2[] vector = this.Value.Positions;
                    vector[j] = value;
                    this.HandleInputChange(new MoveSequenceProperty(vector, this.Value.Spring, this.Value.TimeAtPos), changeType);
                    this.SetInputs();
                };
                _inputObjects.Add(posInstance);
            }
        }

        public override void OnUpdate()
        {
            this.SetInputs();
            var val = this.Value;
            for (int i = 0; i < val.Positions.Length; i++)
            {
                this._input[i].SetWithoutEvent(val.Positions[i]);
            }
            this._size.SetWithoutEvent(val.Positions.Length);
            this._spring.SetWithoutEvent(val.Spring);
            this._timeAtPos.SetWithoutEvent(val.TimeAtPos);
        }

        private void HandleInputChange(MoveSequenceProperty value, ChangeType changeType)
        {
            if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
            {
                this.Context.InspectorTarget.WriteProperty(value);
            }

            if (changeType == ChangeType.ChangeEnd)
            {
                this.Context.Editor.TakeSnaphot();
            }
        }
    }
}
