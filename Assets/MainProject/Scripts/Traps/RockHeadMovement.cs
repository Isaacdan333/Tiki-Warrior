using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHeadMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask player;
    [SerializeField] private GameObject wayPoints;
    [SerializeField] private AudioClip impact;
    private Vector3 destination;
    private bool attacking;
    private float checkTimer;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
        
    }

    private void CheckForPlayer()
    {
        CalculateDir();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, player);
            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
            else
            {
                Return();
            }
        }
    }

    private void CalculateDir()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void Return()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints.transform.position, Time.deltaTime * 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Hit");
            SoundManager.instance.PlaySound(impact);
            Stop();
        }
    }
}
