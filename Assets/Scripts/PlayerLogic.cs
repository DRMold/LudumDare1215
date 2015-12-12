using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	public GameObject cloudPlane;
	public GameObject gameMaster;
	public Camera mainCam;
	public bool autoInit = false;

	// parameters
	public float switchDelay = 0.25f;

	// Sprites
	public Sprite baseCloud1;
	public Sprite baseCloud2;

	// private vars
	private GameController gameController;
	private Sprite baseSprite;
	private short currentAxis;

	void SwitchBaseSprite () {
		if (gameController.playerState == 1) {
			if (cloudPlane.GetComponent<SpriteRenderer>().sprite == baseCloud1) {
				cloudPlane.GetComponent<SpriteRenderer>().sprite = baseCloud2;
			} else {
				cloudPlane.GetComponent<SpriteRenderer>().sprite = baseCloud1;
			}
		}
	}



	void AnimateOnStart() {
		InvokeRepeating("SwitchBaseSprite",switchDelay,switchDelay);
	}

	void AnimateOnUpdate() {
		Vector3 newScale = cloudPlane.transform.localScale;
		float pingPong = Mathf.PingPong(Time.time / 2, 0.2f) + 0.4f;
		if (currentAxis = 0) {
			newScale.x = pingPong;
		} else {
			newScale.y = pingPong;
		}
		cloudPlane.transform.localScale = newScale;
	}

	void Awake () {
		// Initialize as default camera?
		if (autoInit == true) {
			mainCam = Camera.main;
		}

	}

	// Use this for initialization
	void Start () {
		// init variables
		cloudPlane = transform.Find("CloudPlane").gameObject;
		gameController = GameObject.FindWithTag("GameMaster").GetComponent<GameController>();
		baseSprite = cloudPlane.GetComponent<SpriteRenderer>().sprite;
		currentAxis = 0;
		// Run funcs
		AnimateOnStart ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		// Rotate to camera orientation
		cloudPlane.transform.rotation = Quaternion.identity;
		cloudPlane.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
		AnimateOnUpdate();
	}
}
