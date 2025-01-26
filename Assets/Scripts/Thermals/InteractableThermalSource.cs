using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Thermals
{
    public class InteractableThermalSource : BaseThermalSource
    {
        [SerializeField]
        private XRBaseInteractable interactable;
        [SerializeField] private float thermalPower;

        public override float Process(Vector3 thermalBodyWorldPosition)
        {
            return interactable.isSelected ? thermalPower : 0f;
        }
    }
}