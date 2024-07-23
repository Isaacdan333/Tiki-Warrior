using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // have 2 boxcolliders, top one w/ trigger and main body w/ no trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when it collides with the Player object (exact name of player name in heirarchy)
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // when it collides with the Player object (exact name of player name in heirarchy)
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }

}
