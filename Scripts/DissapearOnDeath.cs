using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearOnDeath : MonoBehaviour
{
    //breytur skilgreindar
    public Animator animator;
    public bool hasDied = false;

    public GameObject enemyParticles;

    void Update()
    {
        if (hasDied == false){//boolean breyta sem passar upp á að þetta gerist ekki tvisvar, bara einu sinni
            if(animator.GetBool("Dead")==true){ //hlustar eftir ef þetta enemy er dautt og er að spila death animation
                StartCoroutine(Disappear());//byrja IEnumeratorinn
                hasDied = true;//stilli breytu, þannig að þetta gerist bara einu sinni
            }
        }
        
    }

    public IEnumerator Disappear(){ //IEnumerator
        yield return new WaitForSeconds(0.5f); //bíð í hálfa sekúndu
        Instantiate(enemyParticles,transform.position,Quaternion.identity);//læt particle system birtast á sama stað og enemyið dó
        gameObject.SetActive(false);//læt enemyið hverfa
    }
}
