using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public GameController GameMaster;

	void Start(){ 
		GetComponent<Rigidbody>().velocity = new Vector3
			(
				0.0f, 
				0.0f,
				-this.transform.position.z
			) * GameMaster.worldRot; 
	}
}
