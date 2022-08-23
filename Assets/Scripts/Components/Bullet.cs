using UnityEngine;
using Utilities;
using Views;
using BulletScriptableObject = ScriptableObjects.Bullet;

namespace Components
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask damagableLayers;
        [SerializeField] private LayerMask skipableLayers;
        
        public BulletScriptableObject scriptableObject { get; set; }

        public void OnTriggerEnter(Collider other)
        {
            int otherLayer = other.gameObject.layer;
            
            if (LayerChecker.IsIncludeLayer(otherLayer, damagableLayers))
            {
                IDamagable damagable = other.GetComponent<IDamagable>();
                scriptableObject.Damage(damagable);
            }

            if (!LayerChecker.IsIncludeLayer(otherLayer, skipableLayers))
                Destroy(gameObject);
        }
    }
}