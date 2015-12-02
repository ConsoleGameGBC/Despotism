using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitMove : MonoBehaviour {

	// Use this for initialization

    public int countX = 19;
    public int countY = 19;
    Combat myCombatClass;
    UI uiManager;
	int countC = 0;

    [Header("Erdem's UI Thingy")]
    float timeSinceLastXChange = 5;
    float inputDelay = 0.3f;

    void Start()
    {
        uiManager = GameObject.Find("PaperWork").GetComponent<UI>();
        myCombatClass = GameObject.Find("GameManager").GetComponent<Combat>();

    }

	// Update is called once per frame
	void Update () {
        
       // bool up = Input.GetButtonDown("g");

        if (uiManager.MulitaryStatus == 1)
        {
            myCombatClass.changeActionExplanation(false, uiManager.TerrainType);
            GameObject.Find("Cordinate").GetComponent<Text>().text = "X: " + countX + " Y: "+ countY + " "; 
            switch (uiManager.TerrainType)
            {
                case 1:
                    GameObject.Find("Cordinate").GetComponent<Text>().text += "Flatland";
                    break;
                case 2:
                    GameObject.Find("Cordinate").GetComponent<Text>().text += "Forest";
                    break;
                case 3:
                    GameObject.Find("Cordinate").GetComponent<Text>().text += "Urban";
                    break;
                case 4:
                    GameObject.Find("Cordinate").GetComponent<Text>().text += "Hill";
                    break;
            }
            
            if (transform.localPosition.y < -0.28)
            {
                countY = 0;

            }
            if (transform.localPosition.y > -0.28)
            {
                if (timeSinceLastXChange > inputDelay && Input.GetAxis("YAxis") > 0.5)
                {
                    timeSinceLastXChange = 0;

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
                if (timeSinceLastXChange > inputDelay && Input.GetAxis("YAxis") < -0.5)
                {
                    timeSinceLastXChange = 0;
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
                if (timeSinceLastXChange > inputDelay && Input.GetAxis("XAxis") < -0.5)
                {
                    timeSinceLastXChange = 0;
                    countX--;
                    // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
                    transform.localPosition += new Vector3(-0.25f, 0, 0);
                    Debug.Log(countX + "," + countY);
                }
            }


            if (transform.localPosition.x > 5.08)
            {
                countX = 19;
            }
            if (transform.localPosition.x < 5.08)
            {
                if (timeSinceLastXChange > inputDelay && Input.GetAxis("XAxis") > 0.5)
                {
                    timeSinceLastXChange = 0;
                    countX++;
                    // transform.localPosition = new Vector3(5.088666f, 4.213001f, -0.08f);
                    transform.localPosition += new Vector3(0.25f, 0, 0);
                    Debug.Log(countX + "," + countY);
                }
            }

            myCombatClass.getNewCoordinates(countX, countY);
        }


        timeSinceLastXChange += Time.deltaTime;
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
