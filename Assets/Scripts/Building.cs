using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : IInteractable
{
    public override void Deselect()
    {
        base.Deselect();
    }

    public override void Interact()
    {
       
    }

    public override void Select()
    {
        base.Select();
        print("Building select");

    }
   
}
