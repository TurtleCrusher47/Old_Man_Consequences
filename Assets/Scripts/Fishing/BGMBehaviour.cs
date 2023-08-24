using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMBehaviour : MonoBehaviour
{
    private AudioSource mainBGMSource;
    private AudioSource mainSeaBGMSource;
    [SerializeField]
    private List<AudioClip> clipList;
    private int songIndex;
    // Start is called before the first frame update
    void Start()
    {
        mainBGMSource = GetComponents<AudioSource>()[0];
        mainSeaBGMSource = GetComponents<AudioSource>()[1];
        mainBGMSource.loop = true;
        mainSeaBGMSource.loop = true;
        songIndex = 0;
        mainBGMSource.clip = clipList[songIndex];
        mainBGMSource.Play();
    }

    public void PauseBGMusic()
    {
        mainBGMSource.Pause();
    }
    public void PlayBGMusic()
    {
        mainBGMSource.Play();
    }
    public void PauseSeaSounds()
    {
        mainSeaBGMSource.Pause();
    }
    public void PlaySeaSounds()
    {
        mainSeaBGMSource.Play();
    }
    public void ChangeSong(int newIndex)
    {
        PauseBGMusic();
        songIndex = newIndex;
        if (songIndex < clipList.Count)
        {
            mainBGMSource.clip = clipList[songIndex];
            PlayBGMusic();
        }
        else
            Debug.Log("Invalid!");
    }
}
