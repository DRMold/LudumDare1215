
using UnityEngine;
using System.Collections;

public class AnimationInputTest : MonoBehaviour {

	private Animation anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation>();
		anim["Run"].speed = 10.0f; 


	}
	
	// Update is called once per frame
	void Update () {
		if (anim.isPlaying == false) {
			if (true) {
				anim.Play("Run");
			}
		}
	}
}
