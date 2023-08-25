using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoPlayerSoundController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioClip> audioList;
    // Sound indexes
    // 0: Footsteps
    // 1: Inside grass
    private int soundIndex;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        soundIndex = 0;
        audioSource.clip = audioList[soundIndex];
    }

    public void PlaySound(int newSoundIndex)
    {
        soundIndex = newSoundIndex;
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioList[soundIndex];
            audioSource.Play();
            
        }
    }
    public void PauseSound()
    {
        audioSource.Pause();
    }
    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
