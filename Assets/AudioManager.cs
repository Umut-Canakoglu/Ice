using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake() 
    {
        foreach (Sound s in sounds) 
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }   
    }
    void Start()
    {
        PlaySound("MainTheme");    
    }
    public void PlaySound(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name) 
            {
                s.source.Play();
            }
        }
    }
    public void StopSound(string name)
    {
        foreach (Sound s in sounds)
        {
            if(s.name == name) 
            {
                s.source.Stop();
            }
        }
    }
}
