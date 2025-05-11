using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------Audio Clips----------")]
    public AudioClip Background;
    public AudioClip Death;
    public AudioClip Save;

    void Start()
    {
        MusicSource.clip = Background;
        PlayMusic();
    }

    public void PlayMusic()
    {
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip audioClip)
    {
        MusicSource.Stop();
        SFXSource.PlayOneShot(audioClip);
    }
}
