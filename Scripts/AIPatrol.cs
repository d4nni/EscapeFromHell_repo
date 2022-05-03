using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    //breytur skilgreindar
    public float walkSpeed;

    public AudioSource playerDamage;
    
    public Animator animator;

    public GameObject theplayer;

    public Transform GroundCheckPos;

    public LayerMask groundLayer;

    public BoxCollider2D playerDetector;

    public BoxCollider2D footCollider;

    public Collider2D bodyCollider;

    private bool mustTurn;

    [HideInInspector]
    public bool mustPatrol;

    public Rigidbody2D rb;
    void Start()
    {
        mustPatrol = true; //frumstilli breytuna um að enemy-ið þurfi að patrola
    }

    void Update()
    {
        if (mustPatrol == true){ //ef boolean breytan er true
            Patrol(); //kalla á function
        }
    }

    private void FixedUpdate() {
        if (mustPatrol == true){ //ef boolean breytan er true
            mustTurn = !Physics2D.OverlapCircle(GroundCheckPos.position, 0.1f, groundLayer); //checka á hvort að það sé ground innan við overlapcircle á groundcheck objectinu sem er child af enemyinu
        }
    }
    void Patrol(){ //function sem færir enemyið
        if(mustTurn == true){ //ef boolean breytan er true
            Flip(); //flippa enemyinu (function sem ég kalla á)
        }
        rb.velocity = new Vector2(walkSpeed *Time.fixedDeltaTime,rb.velocity.y); //stilli velocity á rb (breyta fyrir rigidbody á þessu enemyi) sem vector2 sem færir enemyið um x ás á stöðugum hraða. Walkspeed getur verði pósitív og negatív tala sem gerir enemy kleift að fara í báðar áttir
    }
    void Flip(){ //function sem flippar enemyinu
        mustPatrol = false; //stilli boolean breytu í false svo að hann hætti að labba á meðan hann flippar
        transform.localScale = new Vector2(transform.localScale.x*-1, transform.localScale.y); //flippa enemyinu í gegnum transform.localScale með því að breyta x gildinu í - (eða plús, bara hið öfuga við það sem er nú þegar, s.s hin áttin)
        walkSpeed *= -1; //reverse walkspeed þannig að hann labbi í hina áttina líka
        mustPatrol = true; //stilli þannig að þegar hann er búin að flippa sér og allt klárt þá má hann patrol-a aftur
    }

    public void ChangePlayerColor(){ //function sem er aðeins kallað á í gegnum animation event (breytir player í rauðan til þess að tákna damage)
        theplayer = GameObject.Find("Main Character"); //finn main karakterinn í hierarchyinu
        playerDamage.Play();//spila audio source sem táknar damage hjá player
        theplayer.GetComponent<SpriteRenderer>().color = new Color(50,0,0);//breyti lit playersins í rauðan (honum er síðan breytt aftur í playerMovement skriftunni)
    }

    public void OnTriggerEnter2D(Collider2D collision) {//hlustar eftir hvort playerinn sé nálægt
        if(collision.gameObject.tag == "Player"){
            animator.SetBool("Attack", true); //ef player er nálægt, spila attack animation.
        }        
    }
    public void OnTriggerExit2D(Collider2D collision) {//hlustar eftir hvort playerinn sé ekki lengur nálægt
        if(collision.gameObject.tag == "Player"){
            animator.SetBool("Attack", false);//disable-a attack animation
        }   
    }
}


