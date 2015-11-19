using UnityEngine;
using System.Collections;

public class soundtes66t : MonoBehaviour {
	SoundManager sounds;
	// Use this for initialization
	void Start () {
		 sounds = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();

		sounds.PlaySound (5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
