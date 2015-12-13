using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {
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
