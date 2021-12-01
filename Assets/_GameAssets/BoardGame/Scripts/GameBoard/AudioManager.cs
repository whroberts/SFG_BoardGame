using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip BackgroundSound = null;
    [SerializeField] public AudioClip PlacementSound = null;
    [SerializeField] public AudioClip MovementSound1 = null;
    [SerializeField] public AudioClip MovementSound2 = null;
    [SerializeField] public AudioClip MovementSound3 = null;

    AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        BackgroundMusic();
    }

    void BackgroundMusic()
    {
        _audioSource.clip = BackgroundSound;
        _audioSource.Play();
        _audioSource.loop = true;
    }
}
