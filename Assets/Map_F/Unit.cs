using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public int tileX;
	public int tileY;



    void Update()
    {

       // Debug.Log(tileX + "," + tileY);
    }

    //void OnTriggerEnter(Collider other)
    //{
        //Destroy(other.gameObject);
        //    Debug.Log(tileX + "," + tileY);
        //  Debug.Log(other.gameObject.tag);

       // switch (other.transform.name)
       // {

          //  case "Yellow": Debug.Log(other.gameObject.tag); break;
       // }
   // }
}
