using UnityEngine;
using System.Collections;

public class TombSFX : MonoBehaviour {
	
	bool played = false;
	public AudioClip thud;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.rigidbody.position.y <= 14 && !played){
			audio.clip=thud;
			audio.Play ();
			played = true;
		}
	}
}
