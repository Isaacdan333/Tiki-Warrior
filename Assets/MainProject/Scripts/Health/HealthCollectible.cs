using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip healthpickup;
    [SerializeField] private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && sr.enabled == true)
        {
            SoundManager.instance.PlaySound(healthpickup);
            collision.GetComponent<Health>().AddHealth(healthValue);
            // gameObject.SetActive(false);
            sr.enabled = false;
            StartCoroutine(RespawnItem());
        }
    }

    private IEnumerator RespawnItem()
    {
        yield return new WaitForSeconds(5);
        sr.enabled = true;
    }


}
