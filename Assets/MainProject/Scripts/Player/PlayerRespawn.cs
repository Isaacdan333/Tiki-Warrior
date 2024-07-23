using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckRespawn()
    {
        if (currentCheckpoint == null)
        {
            // Show gameOver
            uiManager.GameOver();
            return;
        }
        transform.position = currentCheckpoint.position; // Move player to checkpoint
        playerHealth.Respawn(); // Restore Player health and reset animation
        
    }

    // Activate checkpoint
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            SoundManager.instance.PlaySound(checkpoint);
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate collider
            collision.GetComponent<Animator>().SetTrigger("appear");
            collision.GetComponent<Animator>().SetTrigger("grabbed");
        }
    }
}
