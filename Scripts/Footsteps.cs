using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{

    //test skrifta sem var síðan ekki notuð
    public AudioSource footsteps;
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Player"){
            if(animator.GetFloat("Speed") < 0.01){
                footsteps.Play();
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision){
        if(collision.collider.tag == "Player"){
            footsteps.Stop();
        }
    }
}
