using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private AudioClip finishSource;
    // [SerializeField] TextMeshProUGUI timerText;
    public bool levelCompleted = false;
    float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // while (!levelCompleted)
        // {
        //     elapsedTime += Time.deltaTime;
        //     int minutes = Mathf.FloorToInt(elapsedTime / 60);
        //     int seconds = Mathf.FloorToInt(elapsedTime % 60);
        //     timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            SoundManager.instance.PlaySound(finishSource);
            Invoke("CompleteLevel", 1f);
            //CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
