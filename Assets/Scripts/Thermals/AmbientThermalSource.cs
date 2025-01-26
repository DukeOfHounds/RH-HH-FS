using UnityEngine;

namespace Thermals
{
    public class AmbientThermalSource : BaseThermalSource
    {
        [SerializeField] private float ambientPower;
        
        public override float Process(Vector3 thermalBodyWorldPosition)
        {
            return gameObject.activeInHierarchy ? ambientPower : 0f;
        }
    }
}