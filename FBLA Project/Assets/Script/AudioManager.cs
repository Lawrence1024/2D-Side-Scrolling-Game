using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public AudioSource currentSource;
    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<AudioSource>().clip = sounds[0];
        
    }
    void Start()
    {
        Play(4);
        //sounds[0].source.Play();
        
    }
    void Update()
    {
        
    }

    public void Play(int index)
    {
        gameObject.GetComponent<AudioSource>().clip =sounds[index];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
