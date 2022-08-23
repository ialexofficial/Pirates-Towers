using UnityEngine;
using UnityEngine.EventSystems;

namespace Components
{
    public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] protected Vector3 mouseOffset = new Vector3(0, -1f, 0);
        
        protected Vector3 _dragableStartPosition;
        
        protected Camera _camera;
        protected Vector3 _screenPosition;
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector3 screenTouchPosition = eventData.position;
            screenTouchPosition.z = _screenPosition.z;
            Vector3 touchPosition = _camera.ScreenToWorldPoint(screenTouchPosition);

            transform.position = touchPosition + mouseOffset;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _dragableStartPosition;
        }

        protected virtual void Start()
        {
            _dragableStartPosition = transform.position;
            
            _camera = Camera.main;
            _screenPosition = _camera.WorldToScreenPoint(_dragableStartPosition);
        }
    }
}