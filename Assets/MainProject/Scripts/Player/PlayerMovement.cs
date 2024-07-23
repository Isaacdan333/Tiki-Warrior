using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    BoxCollider2D coll;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] float jump = 10f;
    [SerializeField] LayerMask jumpableGround;
    [SerializeField] Behaviour attackscript;
    [SerializeField] private AudioClip jumpsound;
    [SerializeField] private AudioClip powerup;
    [SerializeField] private AudioClip powerdown;
    private enum MovementState {idle, running, jumping, falling};
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attackscript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*speed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            SoundManager.instance.PlaySound(jumpsound);
        }
        UpdateAnimationState();
        Restart();
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            //sprite.flipX = false;
            transform.localScale = Vector3.one;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            //sprite.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else 
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    void Restart()
    {
        if (transform.position.y < -20)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameObject.GetComponent<Health>().TakeDamage(10);
        }
    }

    public bool canAttack()
    {
        return dirX == 0 && IsGrounded();
    }

    // PowerUp
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedUp"))
        {
            SoundManager.instance.PlaySound(powerup);
            Destroy(collision.gameObject);
            collision.GetComponent<SpriteRenderer>().enabled = false;
            speed *= 1.5f;
            sprite.color = new Color(0, 1, 1);
            StartCoroutine(ResetPower());
        }

        if (collision.CompareTag("Fireball"))
        {
            SoundManager.instance.PlaySound(powerup);
            Destroy(collision.gameObject);
            attackscript.enabled = true;
            //sprite.color = new Color(1, (float)0.5, 0);
            //StartCoroutine(ResetPower());
        }

        if (collision.CompareTag("JumpIncrease"))
        {
            SoundManager.instance.PlaySound(powerup);
            //Destroy(collision.gameObject);
            //collision.GetComponent<SpriteRenderer>().enabled = false;
            jump *= 1.5f;
            sprite.color = Color.green;
            StartCoroutine(ResetPower());
        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5);
        SoundManager.instance.PlaySound(powerdown);
        speed = 10f;
        jump = 15;
        sprite.color = Color.white;
        //attackscript.enabled = false;
        
    }


}
