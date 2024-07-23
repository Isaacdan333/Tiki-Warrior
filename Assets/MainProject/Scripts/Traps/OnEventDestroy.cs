using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEventDestroy : MonoBehaviour
{
    private Animator anim;
    [SerializeField] AudioClip destroyed;
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
        else if (other.CompareTag("Projectile"))
        {
                print("Triggered Event");
                SoundManager.instance.PlaySound(destroyed);
                myEvents.Invoke();
        }

    }

    private void disable()
    {
        gameObject.SetActive(false);
    }
}
