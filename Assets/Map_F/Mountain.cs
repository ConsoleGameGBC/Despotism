using UnityEngine;
using System.Collections;

public class Mountain : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            Debug.Log("Mountain");
        }
    }
}
