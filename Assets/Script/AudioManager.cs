using UnityEngine;

public enum SoundType
{
    BUTTONCLICK,
    UNLOCK,
    INCORRECT,
    RESTART,
    BACKGROUNDMUSIC
}


[RequireComponent(typeof(AudioSource))]   
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static AudioManager _instance;
    private AudioSource audioSource;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public static void PlaySound(SoundType sound, float volume = 1)
    {
        //plays audio file once
        _instance.audioSource.PlayOneShot(_instance.soundList[(int)sound], volume);
    }
}
