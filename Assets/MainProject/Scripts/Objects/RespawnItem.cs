using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && sr.enabled == true)
        {
            sr.enabled = false;
            bc.enabled = false;
            StartCoroutine(Itemrespawn(collision));
        }
    }

    private IEnumerator Itemrespawn(Collider2D collision)
    {
        yield return new WaitForSeconds(5);
        sr.enabled = true;
        bc.enabled = true;
    }
}
