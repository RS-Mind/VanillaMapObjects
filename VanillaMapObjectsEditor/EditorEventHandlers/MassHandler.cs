using MapsExt.Editor;
using MapsExt.Editor.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.Events
{
    internal class MassHandler : EditorEventHandler, ITransformModifyingEditorEventHandler
    {
        private bool _isChanging;
        private MassProperty _prevMass;

        public GameObject Content {  get; private set; }
        public event TransformChangedEventHandler OnTransformChanged;

        protected override void Awake()
        {
            base.Awake();

            this.Content = new GameObject("Mass Interaction Content");
            this.Content.transform.SetParent(this.transform);
            this.Content.transform.localScale = Vector3.one;
            this.Content.layer = MapsExtendedEditor.MapObjectUILayer;
            this.Content.AddComponent<GraphicRaycaster>();
            var canvas = this.Content.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = MainCam.instance.cam;
        }

        protected virtual void Update()
        {
            
        }

        public virtual void SetValue(MassProperty mass)
        {
            this.GetComponent<MassPropertyInstance>().Mass = mass;
            var rigidbody = this.GetComponent<Rigidbody2D>()
                ?? throw new System.ArgumentException("GameObject does not have a rigidbody script", nameof(this.gameObject));

            rigidbody.mass = mass.Value * 1000;
            this.OnTransformChanged?.Invoke();
        }

        public virtual MassProperty GetValue()
        {
            return this.GetComponent<MassPropertyInstance>().Mass / 1000f;
        }

        private void OnChangeStart()
        {
            this._isChanging = true;
            this._prevMass = this.GetValue();
        }

        private void OnChangeEnd()
        {
            this._isChanging = false;

            if (this.GetValue() != this._prevMass)
            {
                this.Editor.TakeSnaphot();
            }
        }

        protected override void HandleEvent(IEditorEvent evt)
        {
            
        }
    }
}
