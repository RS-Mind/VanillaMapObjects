using MapsExt;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Editor.Utils;
using MapsExt.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using VanillaMapObjects.MapObjectProperties;
using VanillaMapObjects.MapObjects;
using VanillaMapObjectsEditor.MapObjects;

namespace VanillaMapObjectsEditor.MapObjectProperties
{
    [EditorPropertySerializer(typeof(MassProperty))]
    internal class EditorMassPropertySerializer : MassPropertySerializer, IPropertyReader<MassProperty>
    {
        public override void WriteProperty(MassProperty property, GameObject target)
        {
            //var delayScript = target.GetComponentInChildren<DelayEvent>()
            //    ?? throw new System.ArgumentException("GameObject does not have a delay script", nameof(target));

            //delayScript.time = property.Value;
            base.WriteProperty(property, target);
            target.GetOrAddComponent<Events.MassHandler>();
        }

        public virtual MassProperty ReadProperty(GameObject instance)
        {
            return instance.GetComponent<MassPropertyInstance>().Mass;
        }
    }

    [InspectorElement(typeof(MassProperty))]
    public class MassElement : FloatElement
    {
        public MassElement() : base("Mass", 0, 1000f) { }

        protected override void OnChange(float mass, ChangeType changeType)
        {
            if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
            {
                this.Context.InspectorTarget.WriteProperty<MassProperty>(new MassProperty(mass));
            }

            if (changeType == ChangeType.ChangeEnd)
            {
                this.Context.Editor.TakeSnaphot();
            }
        }

        protected override float GetValue() => this.Context.InspectorTarget.ReadProperty<MassProperty>().Value;
    }
}
