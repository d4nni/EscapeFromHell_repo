using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
	//skilgreini breytur
	public CharacterController2D controller;
	public Animator animator;
	public AudioSource footsteps;
	public AudioSource fireattack;
	public AudioSource death;
	public bool hasTakenDamage = false;
	public BoxCollider2D GroundCheck;

	// býr til breytu spriterenderer fyrir eldri sprite mynd
	SpriteRenderer old_sprite_img;
	// býr til breytu fyrir nýja sprite, nýja mynd er dregin á breytuna í editor
	public Sprite new_sprite_img;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	public GameObject AttackCollision;

	// public static int breyta sem notuð er til að geyma senu index þar sem player deyr
	public static int senaNr;

	public HealthBar healthBar;

	void Start() {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	void Update()
	{
		if(healthBar.GetHealth() <= 0){ // ef health er minna eða jafnt og núll, s.s. player dauður, þá loadar senu 6, sem er game over sena
			SceneManager.LoadScene(6);
		}
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //næ í "wasd" og örvatakka value, hvort það sé ýtt á þá og margfalda með hraðanum
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); //stilli hraðabreytuna sem animatorinn hlustar á og breytur úr idle í walk animation og til baka

		if (Input.GetButtonDown("Jump")) //ef ýtt er á spacebar
		{
			jump = true; //breyta stillt
		}
		if (Input.GetButtonDown("Fire1"))//ef ýtt er á músina (eða left ctrl)
		{
			animator.SetBool("shoot", true);//set á animation sem lætur karakterinn skjóta
			fireattack.Play();//spila sound effect
			AttackCollision.SetActive(true); //birti trigger collider fyrir eldinn sem hlustar eftir enemy
		}
		if (Input.GetButtonUp("Fire1"))//ef sleppt er músinni (eða left ctrl)
		{
			animator.SetBool("shoot", false);//disable-a animationið
			fireattack.Stop();//hætti að spila audioið
			AttackCollision.SetActive(false);//disable-a trigger collider fyrir eldinn
		}
		if (gameObject.GetComponent<SpriteRenderer>().color == new Color(50,0,0)){ //hlustar eftir ef enemy skriftan breytir litnum á playernum (sem táknar damage)
			if(hasTakenDamage==false){
				currentHealth -= 10;
				healthBar.SetHealth(currentHealth);
				hasTakenDamage = true;
			}
			StartCoroutine(turnColorOff()); //byrjar IEnumerator sem gerir litinn á karakternum aftur venjulegann
		}

	}
	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //læt player controllerinn sem við downloaduðum af netinu sjá um movement með því að gefa honum breytur til þess að vinna með, við notum ekki crouch en sendum samt breytuna með
		jump = false;//stilli boolean breytu
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "IceBlock")
		{
			// setur spriterender iceblock inn í old sprite img breytu
			old_sprite_img = collision.collider.GetComponent<SpriteRenderer>();
			// breytir gamla sprite yfir í nýja
			old_sprite_img.sprite = new_sprite_img;
			// byrjar coroutine timer, sendir gameobject iceblocksins me�
			StartCoroutine(Timer(collision.gameObject));
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		// ef rekist er á trigger sem er með tag Trigger
		if (col.gameObject.tag == "Trigger")
		{
			// nær í núverandi senu
			Scene scene = SceneManager.GetActiveScene();
			// hleður upp næstu senu
			SceneManager.LoadScene(scene.buildIndex + 1);
		}
		// ef rekist er á trigger með tag Daudur, þegar player deyr
		else if (col.gameObject.tag == "Daudur")
        {
			currentHealth = 0;
			healthBar.SetHealth(currentHealth);
			// spilar death sound players
			death.Play();
			// hleður upp gameover senu
			StartCoroutine(SkiptaSenu(6,1.4f));
        }
	}

	IEnumerator Timer(GameObject objectid)
    {
		// bíður í fimm sek
		yield return new WaitForSeconds(5);
		// fjarlægir objectið, sem er iceblockið, úr senunni
		objectid.SetActive(false);
	}
	IEnumerator SkiptaSenu(int tala, float timi)
    {
		//IEnumerator sem bíður í smá og fer síðan í næstu senu
		yield return new WaitForSeconds(timi);
		SceneManager.LoadScene(tala);
    }

	public IEnumerator turnColorOff(){ //stilli colorinn aftur í venjulegan
		yield return new WaitForSeconds(0.5f); //bíð í hálfa sek
		gameObject.GetComponent<SpriteRenderer>().color = new Color(100,100,100);//endurstillir colorinn í venjulegt horf
		hasTakenDamage = false;
		//Debug.Log(currentHealth);
	}
}
