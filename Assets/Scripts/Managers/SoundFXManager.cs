using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }            
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // Spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Assign audioClip
        audioSource.clip = audioClip;

        // Assign volume
        audioSource.volume = volume;

        // Play sound
        audioSource.Play();

        // Destroy clip after it's done playing
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
}