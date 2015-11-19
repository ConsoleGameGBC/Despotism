using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            Debug.Log("Forest");
        }
    }
}
