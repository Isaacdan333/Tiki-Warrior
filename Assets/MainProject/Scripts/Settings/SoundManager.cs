using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}
    private AudioSource soundSource;
    private AudioSource musicsource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        instance = this;
        musicsource = transform.GetChild(0).GetComponent<AudioSource>();
        // Keep this object even when we go to new scene
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else if (instance != null && instance != this)
        //{
        //    Destroy(gameObject);
        //}
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicsource);
        
    }

    private void ChangeSourceVolume(float baseVolume, string volumename, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumename, 1);
        currentVolume += change;

        // Check if we reached the maximum or minimum value
        if (currentVolume > 1) 
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }

        // Assign final value
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        // Save final value to player pref
        PlayerPrefs.SetFloat(volumename, currentVolume);
    }

    
}
