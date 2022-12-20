using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public AudioSource mainMenuMusic, levelMusic, bossMusic;

    public AudioSource[] sfx;

    public void PlayMainMenuMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Play();
    }

    public void PlayLevelMusic()
    {
        if(!levelMusic.isPlaying)
        {
            bossMusic.Stop();
            mainMenuMusic.Stop();
            levelMusic.Play();
        }
        
    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void StopMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenuMusic.Stop();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    public void PlaySFXAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(.8f, 1.2f);
        sfx[sfxToPlay].Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
