using MapsExt.Editor;
using MapsExt.Editor.Events;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.Events
{
    internal class SpringyRopeAttributesHandler : EditorEventHandler, ITransformModifyingEditorEventHandler
    {
        private bool _isChanging;
        private SpringyRopeAttributesProperty _prevAttributes;

        public GameObject Content {  get; private set; }
        public event TransformChangedEventHandler OnTransformChanged;

        protected override void Awake()
        {
            base.Awake();

            this.Content = new GameObject("Spring Attributes Interaction Content");
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

        public virtual void SetValue(SpringyRopeAttributesProperty attributes)
        {
            this.GetComponent<SpringyRopeAttributesPropertyInstance>().Attributes = attributes;
            this.OnTransformChanged?.Invoke();
        }

        public virtual SpringyRopeAttributesProperty GetValue()
        {
            return this.GetComponent<SpringyRopeAttributesPropertyInstance>().Attributes;
        }

        private void OnChangeStart()
        {
            this._isChanging = true;
            this._prevAttributes = this.GetValue();
        }

        private void OnChangeEnd()
        {
            this._isChanging = false;

            if (this.GetValue() != this._prevAttributes)
            {
                this.Editor.TakeSnaphot();
            }
        }

        protected override void HandleEvent(IEditorEvent evt)
        {
            
        }
    }
}
