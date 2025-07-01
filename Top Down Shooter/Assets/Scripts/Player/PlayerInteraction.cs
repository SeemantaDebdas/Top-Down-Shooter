using System.Collections.Generic;
using TDS.Input;
using UnityEngine;

namespace TDS
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] InputSO playerInput;
        [SerializeField] List<Interactable> interactableList;

        Interactable closestInteractable = null;

        void OnEnable()
        {
            playerInput.OnInteractPerformed += () =>
            {
                if (closestInteractable != null)
                {
                    closestInteractable.Interact(this);
                    interactableList.Remove(closestInteractable);
                    UpdateClosestInteractable();
                }
            };
        }

        public void AddToInteractableList(Interactable interactable)
        {
            interactableList.Add(interactable);
            UpdateClosestInteractable();
        }

        public void RemoveFromInteractableList(Interactable interactable)
        {
            interactableList.Remove(interactable);
            UpdateClosestInteractable();
        }

        void UpdateClosestInteractable()
        {
            if (closestInteractable)
                closestInteractable.Unhighlight();

            float closestDistance = Mathf.Infinity;

            foreach (Interactable interactable in interactableList)
            {
                float distance = Vector3.Distance(interactable.transform.position, transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }

            if (closestInteractable)
                closestInteractable.Highlight();
        }
    }
}
