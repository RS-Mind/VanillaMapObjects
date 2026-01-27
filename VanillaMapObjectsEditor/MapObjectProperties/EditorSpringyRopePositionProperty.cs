using MapsExt;
using MapsExt.Editor.Events;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Properties;
using UnboundLib;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;
using VanillaMapObjectsEditor.MapObjects;

namespace VanillaMapObjectsEditor.MapObjectProperties
{
    [EditorPropertySerializer(typeof(SpringyRopePositionProperty))]
    public class EditorSpringyRopePositionPropertySerializer : IPropertySerializer<SpringyRopePositionProperty>
    {
        public virtual void WriteProperty(SpringyRopePositionProperty property, GameObject target)
        {
            var ropeInstance = target.GetComponent<EditorSpringyRope.RopeInstance>();

            ropeInstance.GetAnchor(0).Detach();
            ropeInstance.GetAnchor(0).transform.position = property.StartPosition;
            ropeInstance.GetAnchor(0).UpdateAttachment();
            ropeInstance.GetAnchor(0).gameObject.GetOrAddComponent<RopeAnchorPositionHandler>();

            ropeInstance.GetAnchor(1).Detach();
            ropeInstance.GetAnchor(1).transform.position = property.EndPosition;
            ropeInstance.GetAnchor(1).UpdateAttachment();
            ropeInstance.GetAnchor(1).gameObject.GetOrAddComponent<RopeAnchorPositionHandler>();
        }

        public virtual SpringyRopePositionProperty ReadProperty(GameObject instance)
        {
            var ropeInstance
                = instance.GetComponent<EditorSpringyRope.RopeInstance>()
                ?? throw new System.ArgumentException("GameObject does not have a rope instance", nameof(instance));

            return new SpringyRopePositionProperty()
            {
                StartPosition = ropeInstance.GetAnchor(0).GetAnchoredPosition(),
                EndPosition = ropeInstance.GetAnchor(1).GetAnchoredPosition()
            };
        }
    }

    [InspectorElement(typeof(SpringyRopePositionProperty))]
    public class SpringyRopePositionElement : InspectorElement
    {
        private Vector2Input _input1;
        private Vector2Input _input2;

        public SpringyRopePositionProperty Value
        {
            get => new SpringyRopePositionProperty(
                this.Context.InspectorTarget.GetComponent<EditorSpringyRope.RopeInstance>().GetAnchor(0).GetAnchoredPosition(),
                this.Context.InspectorTarget.GetComponent<EditorSpringyRope.RopeInstance>().GetAnchor(1).GetAnchoredPosition()
            );
            set => this.HandleInputChange(value, ChangeType.ChangeEnd);
        }

        protected override GameObject GetInstance()
        {
            var instance = new GameObject("RopePositionGroup");
            var layoutGroup = instance.AddComponent<VerticalLayoutGroup>();
            layoutGroup.childControlWidth = true;
            layoutGroup.childControlHeight = true;
            layoutGroup.childForceExpandWidth = true;

            var pos1Instance = GameObject.Instantiate(Assets.InspectorVector2InputPrefab, instance.transform);
            var input1 = pos1Instance.GetComponent<InspectorVector2Input>();
            input1.Label.text = "Anchor Position 1";
            this._input1 = input1.Input;
            this._input1.OnChanged += (value, changeType) => this.HandleInputChange(new SpringyRopePositionProperty(value, this.Value.EndPosition), changeType);

            var pos2Instance = GameObject.Instantiate(Assets.InspectorVector2InputPrefab, instance.transform);
            var input2 = pos2Instance.GetComponent<InspectorVector2Input>();
            input2.Label.text = "Anchor Position 2";
            this._input2 = input2.Input;
            this._input2.OnChanged += (value, changeType) => this.HandleInputChange(new SpringyRopePositionProperty(this.Value.StartPosition, value), changeType);

            return instance;
        }

        public override void OnUpdate()
        {
            var val = this.Value;
            this._input1.SetWithoutEvent(val.StartPosition);
            this._input2.SetWithoutEvent(val.EndPosition);
        }

        private void HandleInputChange(SpringyRopePositionProperty value, ChangeType changeType)
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