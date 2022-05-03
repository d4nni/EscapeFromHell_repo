using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_music : MonoBehaviour
{
    // búa til breytu fyrir lagið sem á að spilast í bakgrunni
    AudioSource bakgrunnsLag;
    void Start()
    {
        // nær í audiosource component á objectinu
        bakgrunnsLag = GetComponent<AudioSource>();
        // spilar audio
        bakgrunnsLag.Play();
    }
}
