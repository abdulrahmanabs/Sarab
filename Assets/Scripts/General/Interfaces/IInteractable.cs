using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    [SerializeField]
    protected Material outlineMaterial;
    protected Material originalMaterial;
    protected Renderer objectRenderer;

    [SerializeField] LayerMask layer;

    protected void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalMaterial = objectRenderer.material;
    }

    public abstract void Interact();

    public virtual void Select()
    {
        objectRenderer.materials = new Material[] { originalMaterial, outlineMaterial };
    }

    public virtual void Deselect()
    {
        objectRenderer.materials = new Material[] { originalMaterial };
    }
}