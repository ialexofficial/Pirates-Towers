using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Components
{
    public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public UnityEvent<Vector2> OnUpdate = new UnityEvent<Vector2>();
        [SerializeField] private RectTransform joystick;
        [SerializeField] private RectTransform movablePart;
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] private float radius = 10f;

        private Vector2 _startPosition;
        private Vector2 _movement = Vector2.zero;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPosition = eventData.position;
            joystick.transform.position = _startPosition;
            joystick.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = (eventData.position - _startPosition) * sensitivity;
            float distance = delta.magnitude / radius;
            _movement = delta.normalized * (distance > radius ? radius : distance);
            movablePart.anchoredPosition = _movement;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _movement = Vector2.zero;
            joystick.gameObject.SetActive(false);
        }

        private void Update()
        {
            OnUpdate.Invoke(_movement * Time.deltaTime);
        }
    }
}