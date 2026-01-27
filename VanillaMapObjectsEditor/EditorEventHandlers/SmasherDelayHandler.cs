using MapsExt.Editor;
using MapsExt.Editor.Events;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.Events
{
    internal class SmasherDelayHandler : EditorEventHandler, ITransformModifyingEditorEventHandler
    {
        private bool _isChanging;
        private DelayProperty _prevDelay;

        public GameObject Content {  get; private set; }
        public event TransformChangedEventHandler OnTransformChanged;

        protected override void Awake()
        {
            base.Awake();

            this.Content = new GameObject("Delay Interaction Content");
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

        public virtual void SetValue(DelayProperty delay)
        {
            this.GetComponent<DelayPropertyInstance>().Delay = delay;
            var delayScript = this.GetComponentInChildren<DelayEvent>()
                ?? throw new System.ArgumentException("GameObject does not have a delay script", nameof(this.gameObject));

            delayScript.time = delay.Value;
            this.OnTransformChanged?.Invoke();
        }

        public virtual DelayProperty GetValue()
        {
            return this.GetComponent<DelayPropertyInstance>().Delay;
        }

        private void OnChangeStart()
        {
            this._isChanging = true;
            this._prevDelay = this.GetValue();
        }

        private void OnChangeEnd()
        {
            this._isChanging = false;

            if (this.GetValue() != this._prevDelay)
            {
                this.Editor.TakeSnaphot();
            }
        }

        protected override void HandleEvent(IEditorEvent evt)
        {
            
        }
    }
}
