using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnTriggerExit(Collider other){
		if (other.tag == "Ground") {
			other.transform.position = new Vector3(
				0.0f,
				0.0f,
				other.transform.position.z + 370
				);
		} else if(other.tag == "Building") {
			Destroy(other.gameObject);
		}
	}
}
