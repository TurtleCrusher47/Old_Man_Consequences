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
    // 1:
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
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            soundIndex = newSoundIndex;
            audioSource.clip = audioList[soundIndex];
        }
    }
    public void PauseSound()
    {
        audioSource.Pause();
    }
}
