using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_music : MonoBehaviour
{
    // b�a til breytu fyrir lagi� sem � a� spilast � bakgrunni
    AudioSource bakgrunnsLag;
    void Start()
    {
        // n�r � audiosource component � objectinu
        bakgrunnsLag = GetComponent<AudioSource>();
        // spilar audio
        bakgrunnsLag.Play();
    }
}
