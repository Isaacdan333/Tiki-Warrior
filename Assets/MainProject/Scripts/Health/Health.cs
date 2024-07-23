using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private int numberofFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;

    [Header ("Sounds")]
    [SerializeField] private AudioClip hurtsound;
    [SerializeField] private AudioClip deathsound;
   
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
 
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
     
        if (currentHealth > 0)
        {
           //player hurt
            SoundManager.instance.PlaySound(hurtsound);
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerablity());
        }
        else
        {
            if (!dead)
            {
                // player dead
                SoundManager.instance.PlaySound(deathsound);
                anim.SetTrigger("die");
                //Player
                //if (GetComponent<PlayerMovement>() != null)
                //    GetComponent<PlayerMovement>().enabled = false;
                ////Enemy
                //if (GetComponent<EnemyPatrol>() != null)
                //    GetComponentInParent<EnemyPatrol>().enabled = false;
                //
                //if (GetComponent<MeleeEnemy>() != null)
                //    GetComponent<MeleeEnemy>().enabled = false;

                // good for when multiple enemies die/ Deactivating them
                foreach (Behaviour components in components)
                {
                    components.enabled = false;
                }
                dead = true;
            }
          
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
 
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //        TakeDamage(1);
    //}

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Player");
        
        // Activate all attached components
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }

    private IEnumerator Invulnerablity()
    {
        Physics2D.IgnoreLayerCollision(3, 7, true);
        // invul duration
        for (int i = 0; i < numberofFlashes; i++)
        {
            //spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
            //spriteRend.color = Color.white;
            yield return new WaitForSeconds(iframesDuration / (numberofFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(3, 7, false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
