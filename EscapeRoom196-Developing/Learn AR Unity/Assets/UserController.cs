using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserController : MonoBehaviour {

	[SerializeField]
	GameObject sword;

	[SerializeField]
	Text gameText;

	[SerializeField]
	GameObject letter;

	[SerializeField]
	Text letterText;

	[SerializeField]
	Collider _collider;

	[SerializeField]
	GameObject camera;

    [SerializeField]
    GameObject VRcamera;

    [SerializeField]
    GameObject Sphere;


	private bool haveLetter = false;
	private bool getfree = false;

	private bool holdingGunpowder;
	private GameObject gunpowder;

	private float speed = 2.0f;
	private float rotation_speed = 13f;

	private float yaw;
	private float pitch;
	private float lookSpeedH = 2f;
	private float lookSpeedV = 2f;

    private SteamVR_TrackedController _left_controller;
    private SteamVR_TrackedController _right_controller;

    public GameObject _right_controller_object;
    public GameObject _left_controller_object;

    AudioSource a2;
	AudioSource a1;
	AudioSource a3;
	AudioSource[] walkSound;

	// Use this for initialization
	void Start () {

		walkSound = GetComponents<AudioSource>();
		a1 = walkSound [0];

		a1.Play();
		a3 = walkSound [2];

		a3.Play();
		holdingGunpowder = false;
		gunpowder = null;

		_collider.enabled = false;
		gameText.text = "Free yourself!";

        _left_controller = _left_controller_object.GetComponent<SteamVR_TrackedController>();
        _right_controller = _right_controller_object.GetComponent<SteamVR_TrackedController>();
    }
	
	// Update is called once per frame
	void Update () {

		//Camera direction look around:
		//Look around with Left Mouse
		if (Input.GetMouseButton(0)) {
			yaw += lookSpeedH * Input.GetAxis("Mouse X");
			pitch -= lookSpeedV * Input.GetAxis("Mouse Y");

			camera.transform.eulerAngles = new Vector3(pitch, yaw, 0f);
		}

		//Movement with wasd relative in direction that user if facing.
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 3.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		//walk Sound
		if (Input.GetKeyDown (KeyCode.W)) {
			a2 = walkSound [1];

			a2.Play();
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			a2 = walkSound [1];

			a2.Play();
		}
		if (Input.GetKeyDown (KeyCode.A)) {
			a2 = walkSound [1];

			a2.Play();
		}
		if (Input.GetKeyDown (KeyCode.D)) {
			a2 = walkSound [1];

			a2.Play();
		}


		
		
		Vector3 targetDirection = new Vector3(x, 0f, z);
		targetDirection = Camera.main.transform.TransformDirection(targetDirection);
		targetDirection.y = 0.0f;

		transform.Translate (targetDirection);



		//Check if user touches the sword. 
		Vector3 userPos = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
		Vector3 swordPos = new Vector3 (sword.transform.localPosition.x, 0, sword.transform.localPosition.z);
		if (Vector3.Distance (userPos, swordPos) < 1) {
			gameText.text = "You are free!";
			getfree = true;
			_collider.enabled = true;

		}

		checkPickupGunPowder ();

	}

	private void checkPickupGunPowder() {
		if (gunpowder != null) {
			if(_left_controller.padPressed || _right_controller.padPressed) {
				gunpowder.transform.SetParent (_left_controller_object.transform);
                gunpowder.transform.localPosition = new Vector3(0, 0, 0);
                gunpowder.transform.localScale = new Vector3(1f, 1f, 1f);
				holdingGunpowder = true;
				gameText.text = "";
                GameObject.Destroy(Sphere);
			}
		}
	}

	void OnTriggerEnter(Collider collider) {
		checkGunpowderEnter (collider);
	}

	void OnTriggerExit(Collider collider) {
		checkGunPowderExit (collider);
	}

	//Gunpowder Interaction
	private void checkGunpowderEnter(Collider collider) {
		if (!holdingGunpowder) {
			if (collider.gameObject.tag.Equals ("GunPowder")) {
                Sphere.SetActive(true);
				gameText.text = "Press Space to pick up Gunpowder";
				gameText.enabled = true;
				gunpowder = collider.gameObject;
			}
		}
	}

	private void checkGunPowderExit(Collider collider) {
		if (collider.gameObject.tag.Equals ("GunPowder")) {
            Sphere.SetActive(true);
            gameText.text = "";
			gunpowder = null;
		}
	}

	//Sword interaction
	/*private void checkSwordEnter(Collider collider) {
		if (collider.gameObject.tag.Equals ("Sword")) {
			gameText.text = "You are free.";
			_collider.enabled = true;
		}
	}*/

}
