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
				if (hit.transform.gameObject.name == "StartButton") {
					Application.LoadLevel (1);
				} else if (hit.transform.gameObject.name == "QuitButton") {
					Application.Quit();
				} else if (hit.transform.gameObject.name == "CreditButton") {
					// Give Credit where credit is due
				}
			}
		}
	}
}
