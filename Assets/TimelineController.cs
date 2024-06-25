using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Windows;
public class TimelineController : MonoBehaviour
{
    public PlayableDirector timeLine;
    [SerializeField] PlayerInput abo_hamdi;


    // Start is called before the first frame update
    void Start()
    {
        abo_hamdi.actions["Interact"].performed += onInteract;
        timeLine = GetComponent<PlayableDirector>();
        timeLine.Pause();
    }

    private void onInteract(InputAction.CallbackContext context)
    {
        timeLine.Resume();
    }

}
