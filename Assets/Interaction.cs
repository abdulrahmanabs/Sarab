using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public float interactRange = 5f;

    private Camera cam;
    private IInteractable selectedObject;
    [SerializeField] private LayerMask interactablesLayer;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        ShootInteractRaycast();
    }
    private void ShootInteractRaycast() {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactRange,interactablesLayer))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

            if (selectedObject != null && selectedObject != interactableObject)
            {
                selectedObject.Deselect();
                selectedObject = null;
            }

            if (interactableObject != null && selectedObject == null)
            {
                selectedObject = interactableObject;
                selectedObject.Select();
            }
        }
        else
        {
            if (selectedObject != null)
            {
                selectedObject.Deselect();
                selectedObject = null;
            }
        }


    }
}
