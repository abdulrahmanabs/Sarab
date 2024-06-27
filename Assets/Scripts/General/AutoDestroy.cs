using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float DestroyAfter;
    private void Awake()
    {
        Destroy(gameObject,DestroyAfter);
    }
}
