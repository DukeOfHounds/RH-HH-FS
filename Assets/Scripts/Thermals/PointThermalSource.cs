using UnityEngine;
using UnityEngine.Serialization;

namespace Thermals
{
    public class PointThermalSource : BaseThermalSource
    {
        [SerializeField] private float thermalPower;
        [SerializeField] private float halfDistance = 1f;
        public override float Process(Vector3 thermalBodyWorldPosition)
        {
            var position = transform.position;
            var distance = Vector3.Distance(position, thermalBodyWorldPosition);
            var decay = Mathf.Pow(0.5f, distance / halfDistance);
            var power = thermalPower * decay;

            return power;
        }
    }
}