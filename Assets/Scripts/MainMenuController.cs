using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public Camera cam;
	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if (hit.transform.gameObject.name == "Start") {
					// DO STUFF HERE
				}
			}
		}
	}
}
