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
    [EditorPropertySerializer(typeof(BoxHealthProperty))]
    internal class EditorBoxHealthPropertySerializer : BoxHealthPropertySerializer, IPropertyReader<BoxHealthProperty>
    {
        public override void WriteProperty(BoxHealthProperty property, GameObject target)
        {
            base.WriteProperty(property, target);
            target.GetOrAddComponent<Events.BoxHealthHandler>();
        }

        public virtual BoxHealthProperty ReadProperty(GameObject instance)
        {
            return instance.GetComponent<BoxHealthPropertyInstance>().Health;
        }
    }

    [InspectorElement(typeof(BoxHealthProperty))]
    public class BoxHealthElement : FloatElement
    {
        public BoxHealthElement() : base("Health", 1, 1000f) { }

        protected override void OnChange(float delay, ChangeType changeType)
        {
            if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
            {
                this.Context.InspectorTarget.WriteProperty<BoxHealthProperty>(new BoxHealthProperty(delay));
            }

            if (changeType == ChangeType.ChangeEnd)
            {
                this.Context.Editor.TakeSnaphot();
            }
        }

        protected override float GetValue() => this.Context.InspectorTarget.ReadProperty<BoxHealthProperty>().Value;
    }
}
