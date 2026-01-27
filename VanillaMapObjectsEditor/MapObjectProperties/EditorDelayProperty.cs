using MapsExt;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Properties;
using UnboundLib;
using UnityEngine;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.MapObjectProperties
{
    [EditorPropertySerializer(typeof(DelayProperty))]
    internal class EditorDelayPropertySerializer : DelayPropertySerializer, IPropertyReader<DelayProperty>
    {
        public override void WriteProperty(DelayProperty property, GameObject target)
        {
            //var delayScript = target.GetComponentInChildren<DelayEvent>()
            //    ?? throw new System.ArgumentException("GameObject does not have a delay script", nameof(target));

            //delayScript.time = property.Value;
            base.WriteProperty(property, target);
            target.GetOrAddComponent<Events.SmasherDelayHandler>();
        }

        public virtual DelayProperty ReadProperty(GameObject instance)
        {
            return instance.GetComponent<DelayPropertyInstance>().Delay;
        }
    }

    [InspectorElement(typeof(DelayProperty))]
    public class DelayElement : FloatElement
    {
        public DelayElement() : base("Delay", 0, 5.9f) { }

        protected override void OnChange(float delay, ChangeType changeType)
        {
            if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
            {
                this.Context.InspectorTarget.WriteProperty<DelayProperty>(new DelayProperty(delay));
            }

            if (changeType == ChangeType.ChangeEnd)
            {
                this.Context.Editor.TakeSnaphot();
            }
        }

        protected override float GetValue() => this.Context.InspectorTarget.ReadProperty<DelayProperty>().Value;
    }
}
