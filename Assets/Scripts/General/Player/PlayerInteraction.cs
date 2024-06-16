using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 5f;
    private PlayerInput input;

    private Camera cam;
    private IInteractable selectedObject;
    [SerializeField] private LayerMask interactablesLayer;


    private void Start()
    {
        cam = Camera.main;
        input = GetComponent<PlayerInput>();
        input.actions["Interact"].performed += Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (selectedObject != null)
        {
            selectedObject.Interact();
        }
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, interactRange, interactablesLayer))
        {
            IInteractable interactableObject = hit.collider.GetComponent<IInteractable>();

            if (selectedObject != interactableObject)
            {
                DeselectSelectedObject();

                if (interactableObject != null)
                {
                    print("JOINED");
                    selectedObject = interactableObject;
                    selectedObject.Select();
                }
            }
        }
        else
        {
            DeselectSelectedObject();
        }
    }

    private void DeselectSelectedObject()
    {
        if (selectedObject != null)
        {
            selectedObject.Deselect();
            selectedObject = null;

        }
    }
}