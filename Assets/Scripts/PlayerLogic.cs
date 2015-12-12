using UnityEngine;
using System.Collections;

public class PlayerLogic : MonoBehaviour {

	public GameObject cloudPlane;
	public Camera mainCam;
	public bool autoInit = false;

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
