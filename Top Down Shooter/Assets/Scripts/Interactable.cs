using UnityEngine;

namespace TDS
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] protected MeshRenderer meshRenderer;
        [SerializeField] protected Material highlightedMaterial;
        protected Material defaultMaterial;

        void Awake()
        {
            defaultMaterial = meshRenderer.material;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteraction playerInteraction))
                playerInteraction.AddToInteractableList(this);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerInteraction playerInteraction))
            {
                playerInteraction.RemoveFromInteractableList(this);
                Unhighlight();
            }

        }

        public virtual void Interact(PlayerInteraction playerInteraction)
        {
            print("Interacted with: " + gameObject.name);
        }

        public void Highlight()
        {
            meshRenderer.material = highlightedMaterial;
        }

        public void Unhighlight()
        {
            meshRenderer.material = defaultMaterial;
        }
    }
}
