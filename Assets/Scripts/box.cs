using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : IInteractable
{



    private void Start()
    {

        originalMaterial = GetComponent<Renderer>().material;

        objectRenderer = GetComponent<Renderer>();



    }

    public override void Select()
    {
        base.Select();
        print("box select");



    }

    public override void Deselect()
    {
        base.Deselect();

    }

    public override void Interact()
    {
        print("Box Interaction");
    }
}
