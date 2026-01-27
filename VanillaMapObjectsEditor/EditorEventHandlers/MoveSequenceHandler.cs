using MapsExt.Editor;
using MapsExt.Editor.Events;
using UnityEngine;
using UnityEngine.UI;
using VanillaMapObjects.MapObjectProperties;

namespace VanillaMapObjectsEditor.Events
{
    internal class MoveSequenceHandler : EditorEventHandler, ITransformModifyingEditorEventHandler
    {
        private bool _isChanging;
        private MoveSequenceProperty _prevSequence;

        public GameObject Content {  get; private set; }
        public event TransformChangedEventHandler OnTransformChanged;

        protected override void Awake()
        {
            base.Awake();

            this.Content = new GameObject("Move Sequence Interaction Content");
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

        public virtual void SetValue(MoveSequenceProperty sequence)
        {
            var moveProp = this.GetComponent<MoveSequenceProperty>();
            moveProp.Positions = sequence.Positions;
            moveProp.Spring = sequence.Spring;
            moveProp.TimeAtPos = sequence.TimeAtPos;
            var moveScript = this.GetComponent<MoveSequence>()
                ?? throw new System.ArgumentException("GameObject does not have a move sequence script", nameof(this.gameObject));

            moveScript.positions = sequence.Positions;
            moveScript.spring = sequence.Spring;
            moveScript.timeAtPos = sequence.TimeAtPos;
            this.OnTransformChanged?.Invoke();
        }

        public virtual MoveSequenceProperty GetValue()
        {
            return this.GetComponent<MoveSequencePropertyInstance>().Value;
        }

        private void OnChangeStart()
        {
            this._isChanging = true;
            this._prevSequence = this.GetValue();
        }

        private void OnChangeEnd()
        {
            this._isChanging = false;

            if (this.GetValue() != this._prevSequence)
            {
                this.Editor.TakeSnaphot();
            }
        }

        protected override void HandleEvent(IEditorEvent evt)
        {
            
        }
    }
}
