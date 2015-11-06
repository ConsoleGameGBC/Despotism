using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
    [SerializeField]
    GameObject combatUIObj;

    [SerializeField]
    GameObject combatResultUIObj;

    int soldiers;
    int enemyNum;

    float playerRange = 0.35f;
    float playerMelee = 0.35f;

    float zombieRange = 0.0f;
    float zombieMelee = 0.25f;

    float wolfRange = 0.0f;
    float wolfMelee = 0.40f;

    float rivalRange = 0.30f;
    float rivalMelee = 0.30f;

    float enemyRange;
    float enemyMelee;

    float cityRange = 0.1f;
    float cityMelee = 1.0f;

    float hillRange = 0.5f;
    float hillMelee = 1.1f;

    float forestRange = 0.35f;
    float forestMelee = 0.95f;

    float flatRange = 1.15f;
    float flatMelee = 1.0f;

    float terrainRange;
    float terrainMelee;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public string combatResult(bool isExplored, int terrainType, int soldierNum)
    {
        //GENERATE ENEMY TYPE FOR PROTOTYPE
        int enemyType = 1;
        switch (terrainType)
        {
            case 1:
                enemyType = 1;
                break;
            case 2:
                enemyType = 2;
                break;
            case 3:
                enemyType = 1;
                break;
            case 4:
                enemyType = 3;
                break;
        }

        string myString = "We have encountered " + enemyNum.ToString() + " ";
        switch (enemyType)
        {
            case 1:
                enemyRange = zombieRange;
                enemyMelee = zombieMelee;
                myString += "zombies.";
                break;
            case 2:
                enemyRange = wolfRange;
                enemyMelee = wolfMelee;
                myString += "wolves.";
                break;
            case 3:
                enemyRange = rivalRange;
                enemyMelee = rivalMelee;
                myString += "gangers.";
                break;
        }
        myString += " We fought ";
        switch (terrainType)
        {
            case 1:
                terrainRange = flatRange;
                terrainMelee = flatMelee;
                myString += "in flat wastelands.";
                break;
            case 2:
                terrainRange = forestRange;
                terrainMelee = forestMelee;
                myString += "in a forest.";
                break;
            case 3:
                terrainRange = cityRange;
                terrainMelee = cityMelee;
                myString += "in an urban area.";
                break;
            case 4:
                terrainRange = hillRange;
                terrainMelee = hillMelee;
                myString += "on hills.";
                break;
        }

        float enemyCas = soldierNum * terrainRange * playerRange * Random.Range(0.5f, 2.0f);
        float playerCas = enemyNum * terrainRange * playerRange * Random.Range(0.5f, 2.0f);

        if ((int)enemyCas > enemyNum)
            enemyCas = enemyNum;

        if ((int)playerCas > soldierNum)
            playerCas = soldierNum;

        soldierNum -= (int)playerCas;
        enemyNum -= (int)enemyCas;

        myString += " On range we killed " + ((int)enemyCas).ToString() + " and we lost "
            + ((int)playerCas).ToString() + " soldiers";

        if(soldierNum > 0 && enemyNum > 0)
        {
             enemyCas = soldierNum * terrainMelee * enemyMelee * Random.Range(0.5f, 2.0f);
             playerCas = enemyNum * terrainMelee * playerMelee * Random.Range(0.5f, 2.0f);

            if ((int)enemyCas > enemyNum)
                enemyCas = enemyNum;

            if ((int)playerCas > soldierNum)
                playerCas = soldierNum;

            soldierNum -= (int)playerCas;
            enemyNum -= (int)enemyCas;


            myString += " On melee we killed " + ((int)enemyCas).ToString() + " and we lost "
            + ((int)playerCas).ToString() + " soldiers";
        }

        if (soldierNum > 0)
        {
            myString += " We won the fight.";
        }
        else
        {
            myString += "We lost the fight.";
        }

        soldiers = soldierNum;

        combatResultUIObj.SetActive(true);
        combatUIObj.SetActive(false);


        return myString;
    }

    public string explorationResult(bool isExplored, int terrainType, int soldierNum)
    {
        string myString = combatResult(isExplored, terrainType, soldierNum);

        if(soldiers > 0)
        {
            myString += "We looted: ";
            int temp = Random.Range(0, 40);
            myString += temp.ToString() + " food, ";
            this.GetComponent<Resource>().changeFood(temp);

            temp = Random.Range(0, 40);
            myString += temp.ToString() + " water, ";
            this.GetComponent<Resource>().changeWater(temp);

            temp = Random.Range(0, 40);
            myString += temp.ToString() + " fuel, ";
            this.GetComponent<Resource>().changeFuel(temp);

            temp = Random.Range(0, 40);
            myString += temp.ToString() + " medicine, ";
            this.GetComponent<Resource>().changeMedical(temp);

            temp = Random.Range(0, 5);
            myString += "We also found " + temp.ToString() + " survivors.";
            this.GetComponent<Resource>().changeUnemployed(temp);
        }

        return myString;
    }


}
