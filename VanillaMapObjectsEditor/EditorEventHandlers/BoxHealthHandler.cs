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
    internal class BoxHealthHandler : EditorEventHandler, ITransformModifyingEditorEventHandler
    {
        private bool _isChanging;
        private BoxHealthProperty _prevHealth;

        public GameObject Content {  get; private set; }
        public event TransformChangedEventHandler OnTransformChanged;

        protected override void Awake()
        {
            base.Awake();

            this.Content = new GameObject("Box Health Interaction Content");
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

        public virtual void SetValue(BoxHealthProperty health)
        {
            this.GetComponent<BoxHealthPropertyInstance>().Health = health;
            var damageScript = this.GetComponentInChildren<DamagableEvent>()
                ?? throw new System.ArgumentException("GameObject does not have a damageable script", nameof(this.gameObject));

            damageScript.maxHP = health.Value;
            damageScript.currentHP = health.Value;
            this.OnTransformChanged?.Invoke();
        }

        public virtual BoxHealthProperty GetValue()
        {
            return this.GetComponent<BoxHealthPropertyInstance>().Health;
        }

        private void OnChangeStart()
        {
            this._isChanging = true;
            this._prevHealth = this.GetValue();
        }

        private void OnChangeEnd()
        {
            this._isChanging = false;

            if (this.GetValue() != this._prevHealth)
            {
                this.Editor.TakeSnaphot();
            }
        }

        protected override void HandleEvent(IEditorEvent evt)
        {
            
        }
    }
}
