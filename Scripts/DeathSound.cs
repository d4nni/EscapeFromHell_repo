using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSound : MonoBehaviour
{
    //skilgreini breytur
    public AudioSource deathSound;

    public bool hasDied = false;

    void Update()
    {
        if(gameObject.GetComponent<Animator>().GetBool("Dead")==true){ //hlustar eftir hvort enemyið sé dautt og sé að spila death animation
            if(hasDied == false){ //ef svo er og þetta er í fyrsta sinn sem if statementið er true
                deathSound.Play(); //spila death sound
                hasDied = true; //stilli breytu svo það spilist ekki tvisvar vegna þess að þetta checkast á hverju framei
            }
        }
    }
}
