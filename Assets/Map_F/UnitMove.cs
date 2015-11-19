using UnityEngine;
using System.Collections;

public class UnitMove : MonoBehaviour {

	// Use this for initialization

    int countX = 19;
    int countY = 19;

	int countC = 0;
	// Update is called once per frame
	void Update () {

       // bool up = Input.GetButtonDown("g");


        if (transform.localPosition.y < -0.28)
        {
            countY = 0;

        }
        if (transform.localPosition.y > -0.28)
        {
            if (Input.GetButtonDown("b"))
            {
                
                countY--;
                
                // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
                transform.localPosition += new Vector3(0, -0.25f, 0);
                Debug.Log(countX + "," + countY);
            }

        }

        if (transform.localPosition.y > 4.46)
        {
            countY = 19;
        }
     //if(up)
    if (transform.localPosition.y < 4.46)
    {
        if (Input.GetButtonDown("g"))
        {
            countY++;
            // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
            transform.localPosition += new Vector3(0, 0.25f, 0);
            Debug.Log(countX + "," + countY);
        }
    }

    if (transform.localPosition.x < 0.58)
    {
        countX = 0;
    }





    if (transform.localPosition.x > 0.58)
    {
        if (Input.GetButtonDown("v"))
        {
            countX--;
            // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
            transform.localPosition += new Vector3(-0.25f, 0, 0);
            Debug.Log(countX + "," + countY);
        }
    }


    if (transform.localPosition.x > 5.08)
    {
        countY = 19;
    }
    if (transform.localPosition.x < 5.08)
    {
        if (Input.GetButtonDown("n"))
        {
            countX++;
            // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
            transform.localPosition += new Vector3(0.25f, 0, 0);
            Debug.Log(countX + "," + countY);
        }
    }



	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Base") {
			//countX++;
			//transform.localPosition += new Vector3 (0.25f, 0, 0);
			//gameObject.SetActive(false);

			countC++;
			if(countC == 1)
			{

				countX++;
				transform.localPosition += new Vector3 (0.25f, 0, 0);
			}
			if(countC == 2)
			{
				
				countX--;
				transform.localPosition += new Vector3 (-0.25f, 0, 0);
			}

			if(countC==3)
			{
				
				countY++;
				transform.localPosition += new Vector3 (0, 0.25f, 0);
			}

			if(countC == 4)
			{
				
				countY--;
				transform.localPosition += new Vector3 (0, -0.25f, 0);
			}
			if(countC==5)
			{
				
				countX++;
				transform.localPosition += new Vector3 (0.25f, 0, 0);
			}
			if(countC == 6)
			{
				
				countX--;
				transform.localPosition += new Vector3 (-0.25f, 0, 0);
			}
			if(countC ==7)
			{
				
				countY++;
				transform.localPosition += new Vector3 (0, 0.25f, 0);
			}
			if(countC==8)
			{
				
				countY--;
				transform.localPosition += new Vector3 (0, -0.25f, 0);
			}
			if (countC>8)
			{
				countY--;
				transform.localPosition += new Vector3 (0, -0.25f, 0);
			}


		}
   
	}

}
