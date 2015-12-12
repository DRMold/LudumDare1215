using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	public GameObject cloudPlane;
	public GameObject gameMaster;
	public Camera mainCam;
	public bool autoInit = false;

	private GameController gameController;
	private Vector2 offset1 = new Vector2(0.0f,0.5f);
	private Vector2 offset2;


	void Animate() {
		if (gameController.playerState == 1) {
			if (Time.time % 0.5 == 0) {
				if (true) {

				}
			}
		}
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		// Rotate to camera orientation
		cloudPlane.transform.rotation = Quaternion.identity;
		cloudPlane.transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.up);
	}
}
