using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BirdScripts : MonoBehaviour {

	public static BirdScripts instance;

	[SerializeField]
	private Rigidbody2D myRigidBody;

	[SerializeField]
	private Animator anim;

	private float forwardSpeed = 3f;

	private float bounceSpeed = 4f;

	private bool didFlap;

	public bool isAlive;
	
	private Button flapButton;

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private AudioClip flapClip,pointClip,diedClip;

	public int score;
	void Awake(){

		if (instance == null) {
			instance = this;
		}

		isAlive = true;
		score = 0;

		flapButton = GameObject.FindGameObjectWithTag ("FlapButton").GetComponent<Button> ();
		flapButton.onClick.AddListener (() => FlapTheBird ());

		SetCamerasX ();
	
	}

	void FixedUpdate(){
		if (isAlive) {

			Vector3 temp = transform.position;
			temp.x += forwardSpeed * Time.deltaTime;
			transform.position = temp;

			if(didFlap){
				didFlap = false;
				myRigidBody.velocity = new Vector2(0,bounceSpeed);
				audioSource.PlayOneShot(flapClip);
				anim.SetTrigger("Flap");
			}

			if(myRigidBody.velocity.y >= 0){
				transform.rotation = Quaternion.Euler(0,0,0);
			}else{
				float angel = 0;
				angel = Mathf.Lerp(0,-90, -myRigidBody.velocity.y/7);
				transform.rotation = Quaternion.Euler(0,0,angel);
			}
		}
	}

	void SetCamerasX(){
		CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f; 	
	}

	public float GetPositionX(){
		return transform.position.x;
	}

	public void FlapTheBird(){
		didFlap = true;
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe") {
			if(isAlive){
				isAlive = false;
				anim.SetTrigger("Bird Died");
				audioSource.PlayOneShot(diedClip);
				GamePlayController.instance.PlayerDiedShowScore(score);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "PipeHolder") {
			score++;
			GamePlayController.instance.SetScore(score);
			audioSource.PlayOneShot(pointClip);
		}
	}
}











