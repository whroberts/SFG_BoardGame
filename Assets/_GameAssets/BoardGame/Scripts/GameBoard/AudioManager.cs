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
    [SerializeField] public AudioClip WinSound = null;
    [SerializeField] public AudioClip LoseSound = null;
    [SerializeField] public AudioClip PlayerPieceTakenSound = null;
    [SerializeField] public AudioClip EnemyPieceTakenSound = null;

    AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        BackgroundMusic();
    }

    public void BackgroundMusic()
    {
        _audioSource.clip = BackgroundSound;
        _audioSource.Play();
        _audioSource.loop = true;
    }

    public IEnumerator PlayWinSound()
    {
        _audioSource.PlayOneShot(WinSound);
        yield return new WaitForSeconds(1f);
        BackgroundMusic();
    }

    public IEnumerator PlayLoseSound()
    {
        _audioSource.PlayOneShot(LoseSound);
        yield return new WaitForSeconds(1f);
        BackgroundMusic();
    }


}
