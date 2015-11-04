using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

    int soldiers;
    int enemyNum;

    float skillFar = 0.3f;
    float skillMed = 0.4f;
    float skillMelee = 0.4f;


    float zombieFar = 0.0f;
    float zombieMed = 0.0f;
    float zombieMelee = 0.4f;


    float wolfFar = 0.0f;
    float wolfMedium = 0.0f;
    float wolfMelee = 0.7f;



    float grassFar = 1.1f;
    float grassMedium = 1.1f;
    float grassMelee = 1.0f;

    float cityFar = 0.1f;
    float cityMedium = 0.5f;
    float cityMelee = 1.2f;

    


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string combatResult(int enemyNum, int enemyType, int terrainType, int soldierNum)
    {
        return " ";
    }


}
