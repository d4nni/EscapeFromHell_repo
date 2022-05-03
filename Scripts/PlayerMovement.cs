using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	//skilgreini breytur
	public CharacterController2D controller;
	public Animator animator;
	public AudioSource footsteps;
	public AudioSource fireattack;
	public AudioSource death;
	public BoxCollider2D GroundCheck;

	// b�r til breytu spriterenderer fyrir eldri sprite mynd
	SpriteRenderer old_sprite_img;
	// b�r til breytu fyrir n�ja sprite, n�ja mynd er dregin � breytuna � editor
	public Sprite new_sprite_img;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	public GameObject AttackCollision;

	// public static int breyta sem notu� er til a� geyma senu index �ar sem player deyr
	public static int senaNr;

	void Update()
	{
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
			// setur spriterender iceblock inn � old sprite img breytu
			old_sprite_img = collision.collider.GetComponent<SpriteRenderer>();
			// breytir gamla sprite yfir � n�ja
			old_sprite_img.sprite = new_sprite_img;
			// byrjar coroutine timer, sendir gameobject iceblocksins me�
			StartCoroutine(Timer(collision.gameObject));
		}
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		// ef rekist er � trigger sem er me� tag Trigger
		if (col.gameObject.tag == "Trigger")
		{
			// n�r � n�verandi senu
			Scene scene = SceneManager.GetActiveScene();
			// hle�ur upp n�stu senu
			SceneManager.LoadScene(scene.buildIndex + 1);
		}
		// ef rekist er � trigger me� tag Daudur, �egar player deyr
		else if (col.gameObject.tag == "Daudur")
        {
			// nær í núverandi senu
			Scene scene = SceneManager.GetActiveScene();
			// setur senu indexin � senaNr breytuna og n�� er � �essa breytu � buttonbehaviour skriftunni
			senaNr = scene.buildIndex;
			// spilar death sound players
			death.Play();
			// hle�ur upp gameover senu
			StartCoroutine(SkiptaSenu(6,1.4f));
        }
	}

	IEnumerator Timer(GameObject objectid)
    {
		// b��ur � fimm sek
		yield return new WaitForSeconds(5);
		// fjarl�gir objecti�, sem er iceblocki�, �r senunni
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
	}
}
