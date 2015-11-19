using UnityEngine;
using System.Collections;

public class Grass : MonoBehaviour {

	// Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unit")
        {
            Debug.Log("Grassland");
        }
    }
}
