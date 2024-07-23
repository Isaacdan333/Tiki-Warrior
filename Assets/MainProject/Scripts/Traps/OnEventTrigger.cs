using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEventTrigger : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [Header("Custom Event")]
    public UnityEvent myEvents;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (myEvents == null)
        {
            print("myEventTriggerOnEnter was triggered but myEvents was null");
        }
        else
        {
            print("Triggered Event");
            myEvents.Invoke();
            
        }

    }
}
