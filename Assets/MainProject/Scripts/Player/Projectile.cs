using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] AudioClip explode;
    private bool hit;
    private float direction;
    private float lifetime;
    private BoxCollider2D collide;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        collide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        // if projectile does not come into contact with anything in set time, destroy
        lifetime += Time.deltaTime;
        if (lifetime > 0.5f) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ground"))
        {
            hit = true;
            //collide.enabled = false;
            anim.SetTrigger("explode");
        }

        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            SoundManager.instance.PlaySound(explode);
            hit = true;
            //collide.enabled = false;
            anim.SetTrigger("explode");
        }

        if (collision.CompareTag("Boss"))
        {
            SoundManager.instance.PlaySound(explode);
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        //collide.enabled = true;

        // switch position
        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
