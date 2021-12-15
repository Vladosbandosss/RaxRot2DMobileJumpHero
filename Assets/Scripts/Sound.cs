using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SOUNDFX
{
    JUMP=0,
    GO=1
}

public class Sound : MonoBehaviour
{
    public static Sound instance;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip[] sfxSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
        
    }

    public void PlaySound(SOUNDFX sounds)
    {
        _audioSource.clip = sfxSounds[(int) sounds];
       
        _audioSource.Play();
    }
}
