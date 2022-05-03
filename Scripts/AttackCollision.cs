using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
  //þessi skrifta er sett á eldinn sem kemur út úr playernum (s.s. attackið hans)
   public void OnTriggerEnter2D(Collider2D collision){ //hlustar eftir collision eldsins á hlutum með enemy tag
      if(collision.gameObject.tag == "Enemy"){
        collision.gameObject.GetComponent<Animator>().SetBool("Dead", true);//spila death animation hjá enemyinu
        collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;//disable-a rigidbodyið
        collision.gameObject.GetComponent<AIPatrol>().enabled = false;//disable-a skriftuna sem lætur enemyið hreyfast
        collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;//disable-a colliderinn
      }
    }
}
