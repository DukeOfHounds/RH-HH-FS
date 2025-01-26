using UnityEngine;

namespace Thermals
{
    public abstract class BaseThermalSource : MonoBehaviour
    {
        public abstract float Process(Vector3 thermalBodyWorldPosition);
    }
}