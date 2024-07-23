using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject disableobject;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        PauseGame(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            /// If pause screen already active unpuase and viceversa
            if(pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
                disableobject.GetComponent<PlayerMovement>().enabled = true;
            }
            else
            {
                PauseGame(true);
                disableobject.GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void PauseGame(bool status)
    {
        // if status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        // when pause status  is true, time stop game
        if (status)
        {
            disableobject.GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            disableobject.GetComponent<PlayerMovement>().enabled = true;
            Time.timeScale = 1;
        }
        
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}
