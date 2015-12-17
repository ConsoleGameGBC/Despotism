using UnityEngine;
using System.Collections;

public class selfDestruct : MonoBehaviour {
    [SerializeField] float lifeSpan = 3.0f;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
