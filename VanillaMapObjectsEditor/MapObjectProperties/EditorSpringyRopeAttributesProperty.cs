using MapsExt;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Properties;
using System.Collections.Generic;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.MapObjectProperties
{
    [EditorPropertySerializer(typeof(SpringyRopeAttributesProperty))]
    internal class EditorSpringyRopeAttributesPropertySerializer : SpringyRopeAttributesPropertySerializer, IPropertyReader<SpringyRopeAttributesProperty>
    {
        public override void WriteProperty(SpringyRopeAttributesProperty property, GameObject target)
        {
            base.WriteProperty(property, target);
            target.GetOrAddComponent<Events.SpringyRopeAttributesHandler>();
        }

        public virtual SpringyRopeAttributesProperty ReadProperty(GameObject instance)
        {
            return instance.GetComponent<SpringyRopeAttributesPropertyInstance>().Attributes;
        }
    }

    [InspectorElement(typeof(SpringyRopeAttributesProperty))]
    public class SpringyRopeAttributesElement : InspectorElement
    {
        private TextSliderInput _frequency;
        private TextSliderInput _damping;
        private GameObject menuInstance;

        public SpringyRopeAttributesProperty Value
        {
            get => new SpringyRopeAttributesProperty(this._frequency.Value, this._damping.Value);
            set => this.HandleInputChange(value, ChangeType.ChangeEnd);
        }

        protected override GameObject GetInstance()
        {
            menuInstance = new GameObject("SpringyRopeAttributesGroup");
            var layoutGroup = menuInstance.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandWidth = true;

            var springInstance = GameObject.Instantiate(Assets.InspectorSliderInputPrefab, menuInstance.transform);
            var springInput = springInstance.GetComponent<InspectorSliderInput>();
            springInput.Label.text = "Frequency";
            springInput.Input.MinValue = 0;
            springInput.Input.Slider.minValue = 0;
            springInput.Input.MaxValue = 10;
            springInput.Input.Slider.maxValue = 10;
            springInput.Input.Value = this.Context.InspectorTarget.ReadProperty<SpringyRopeAttributesProperty>().Frequency;
            this._frequency = springInput.Input;
            this._frequency.OnChanged += (value, changeType) => this.HandleInputChange(new SpringyRopeAttributesProperty(value, this.Value.DampingRatio), changeType);

            var timeAtPosInstance = GameObject.Instantiate(Assets.InspectorSliderInputPrefab, menuInstance.transform);
            var timeAtPosInput = timeAtPosInstance.GetComponent<InspectorSliderInput>();
            timeAtPosInput.Label.text = "Damping Ratio";
            timeAtPosInput.Input.MinValue = 0;
            timeAtPosInput.Input.Slider.minValue = 0;
            timeAtPosInput.Input.MaxValue = 1;
            timeAtPosInput.Input.Slider.maxValue = 1;
            timeAtPosInput.Input.Value = this.Context.InspectorTarget.ReadProperty<SpringyRopeAttributesProperty>().DampingRatio;
            this._damping = timeAtPosInput.Input;
            this._damping.OnChanged += (value, changeType) => this.HandleInputChange(new SpringyRopeAttributesProperty(this.Value.Frequency, value), changeType);

            return menuInstance;
        }

        public override void OnUpdate()
        {
            var val = this.Value;
            this._frequency.SetWithoutEvent(val.Frequency);
            this._damping.SetWithoutEvent(val.DampingRatio);
        }

        private void HandleInputChange(SpringyRopeAttributesProperty value, ChangeType changeType)
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
