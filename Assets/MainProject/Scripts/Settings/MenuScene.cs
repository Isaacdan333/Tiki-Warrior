using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public bool levelCompleted = false;
    [SerializeField] private AudioClip finishSource;
    [SerializeField] string scene = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene()
    {
        if (scene == "Quit")
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            levelCompleted = true;
            SoundManager.instance.PlaySound(finishSource);
            Invoke("loadScene", 1f);
            //CompleteLevel();
        }
    }
}
