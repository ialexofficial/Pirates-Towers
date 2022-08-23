using Components;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace Views
{
    public class PowderStorageView : Dragable
    {
        [SerializeField] private LayerMask loadingLayers = 6;

        private bool _isLoaded = false;

        public override void OnDrag(PointerEventData eventData)
        {
            if (_isLoaded)
                return;
            
            base.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _isLoaded = false;
            
            base.OnEndDrag(eventData);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (LayerChecker.IsIncludeLayer(other.gameObject.layer, loadingLayers))
            {
                GunView gunView = other.GetComponent<GunView>();
                
                if (!gunView.IsPowderLoaded)
                {
                    gunView.LoadGunPowder();
                    _isLoaded = true;
                    transform.position = _dragableStartPosition;
                }
            }
        }
    }
}