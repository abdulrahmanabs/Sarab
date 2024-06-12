using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour, IInteractable
{
    public Color outlineColor = Color.yellow;
    public float outlineWidth = 0.05f;

    private Material outlineMaterial;
    private Material originalMaterial;

    private void Start()
    {
        outlineMaterial = new Material(Shader.Find("Custom/OutlineShader"));
        originalMaterial =  GetComponent<Renderer>().material;
    }

    public void Select()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = outlineMaterial;

        outlineMaterial.SetColor("_OutlineColor", outlineColor);
        outlineMaterial.SetFloat("_OutlineWidth", outlineWidth);

        print("CALLED SELECT");
    }

    public void Deselect()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = originalMaterial;

        print("CALLED DESELECT");
    }

    public void Interact()
    {
        
    }
}
