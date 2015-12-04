using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Combat : MonoBehaviour {
    [SerializeField]
    GameObject combatUIObj;

    [SerializeField]
    GameObject combatResultUIObj;

    [SerializeField]
    GameObject combatResultTextObj;

    [SerializeField]
    GameObject actionExplanationObj;

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

    int currentX = 0;
    int currentY = 0;

    bool[,] exploredMap = new bool[20, 20];
    bool[,] dangerMap = new bool[20, 20];
    bool[,] lootMap = new bool[20, 20];

    // Use this for initialization
    void Start() {
        //bool[,] exploredMap = new bool[20, 20];
        //bool[,] dangerMap = new bool[20, 20];
        //bool[,] lootMap = new bool[20, 20];

        for (int i = 0; i < 20; i++)
        {
            for(int j = 0; j<20; j++)
            {
                exploredMap[i,j] = false;

                int r = UnityEngine.Random.Range(0, 100);
                dangerMap[i, j] = r < 50 ? true : false;
                r = UnityEngine.Random.Range(0, 100);
                lootMap[i, j] = r < 50 ? true : false;

            }
        }

        

	}
	



	// Update is called once per frame
	void Update () {
	
	}

    public void getNewCoordinates(int X, int Y)
    {
        currentX = X;
        currentY = Y;

        Debug.Log("x in combat " + X+ " y in combat " + Y);
    }

    public void changePaperBack()
    {
        combatResultUIObj.SetActive(false);
        combatUIObj.SetActive(true);
    }

    public void changeActionExplanation(bool isExplore, int terraintType)
    {
        string temp = "";

        if(exploredMap[currentX,currentY]==false && isExplore == false)
        {
            temp += "WARNING! Attacking a location without scouting it first is very dangerous.\n\n";
        } else if(exploredMap[currentX,currentY] && isExplore)
        {
            temp += "WARNING! We have already scouted this region. We will not learn more by sending more scouts.\n\n";
        }

        if (exploredMap[currentX, currentY])
        {
            if (dangerMap[currentX, currentY]) {
                temp += "This is a very dangerous region ";
            }
            else
            {
                temp += "This is a mildly dangerous region ";
            }

            if (lootMap[currentX, currentY])
            {
                temp += "with RESOURCES we can LOOT.\n";
            }
            else
            {
                temp += "that is already looted.\n";
            }

        }

        temp += "We can send a force to ";

        if (isExplore)
            temp += "explore this ";
        else
            temp += "clean up and loot this ";

        switch (terraintType)
        {
            case 1:
                temp += "flatland.";
                break;
            case 2:
                temp += "forest.";
                break;
            case 3:
                temp += "urban area. ";
                break;
            case 4:
                temp += "hilly area.";
                break;
            default:
                Debug.Log("Error in changeActionExplaination. Default terraintype.");
                break;
        }

        actionExplanationObj.GetComponent<Text>().text = temp;

    }

    public string combatResult(bool isExploration, int terrainType, int soldierNum)
    {
        string myString = explorationResult(isExploration, terrainType, soldierNum);

        if (soldiers > 0 && lootMap[currentX,currentY] == true)
        {
            myString += "\n";

            myString += "We looted: ";
            int temp = Random.Range(0, 100);
            myString += temp.ToString() + " food, ";
            this.GetComponent<Resource>().changeFood(temp);

            temp = Random.Range(0, 100);
            myString += temp.ToString() + " water, ";
            this.GetComponent<Resource>().changeWater(temp);

            temp = Random.Range(0, 100);
            myString += temp.ToString() + " fuel, ";
            this.GetComponent<Resource>().changeFuel(temp);

            temp = Random.Range(0, 100);
            myString += temp.ToString() + " medicine, ";
            this.GetComponent<Resource>().changeMedical(temp);

            temp = Random.Range(0, 10);
            if (temp > 5)
            {
                temp -= 4;
                myString += "We also found " + temp.ToString() + " survivors.";
                this.GetComponent<Resource>().changeUnemployed(temp);
            }
        }

        lootMap[currentX, currentY] = false;
        combatResultTextObj.GetComponent<Text>().text = myString;
        return myString;
    }

    public string explorationResult(bool isExploration, int terrainType, int soldierNum)
    {
        int totalplayerLoss = 0;
        int playerCasMelee = 0;

        if(dangerMap[currentX,currentY] && exploredMap[currentX,currentY] == false && isExploration == false)
        {
            enemyNum = Random.Range(30, 60);
        }else if(dangerMap[currentX, currentY] || exploredMap[currentX, currentY] == false)
        {
            enemyNum = Random.Range(15, 25);
        }
        else
        {
            enemyNum = Random.Range(5, 15);
        }


        //GENERATE ENEMY TYPE FOR PROTOTYPE and NUMBER
        //enemyNum = Random.Range(5, 15);
        //terrainType = Random.Range(1, 5);
        //soldierNum = 20;
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
        Debug.Log("Terrain type num is" + terrainType);

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

        Debug.Log("enemyCas" + soldierNum + " " + terrainRange + " " + playerRange);
        Debug.Log("playerCas" + enemyNum + " " + terrainRange + " " + enemyRange);
        float enemyCas = soldierNum * terrainRange * playerRange * Random.Range(1.8f, 3.0f);
        float playerCas = enemyNum * terrainRange * enemyRange * Random.Range(1.8f, 3.0f);
        Debug.Log("enemycas" + enemyCas);
        Debug.Log("playercas" + playerCas);

        if ((int)enemyCas > enemyNum)
            enemyCas = enemyNum;

        if ((int)playerCas > soldierNum)
            playerCas = soldierNum;

        soldierNum -= (int)playerCas;
        enemyNum -= (int)enemyCas;

        myString += " On range we killed " + ((int)enemyCas).ToString() + " and we lost "
            + ((int)playerCas).ToString() + " soldiers";

        if (soldierNum > 0 && enemyNum > 0)
        {

            totalplayerLoss = (int)playerCas;
            enemyCas = 0;
            playerCas = 0;

            int enemyCasMelee = 0;
            //int playerCasMelee = 0;
            while ((int)enemyCas != enemyNum && (int)playerCas != soldierNum)
            {
                enemyCas = soldierNum * terrainMelee * playerMelee * Random.Range(0.8f, 2.5f);
                playerCas = enemyNum * terrainMelee * enemyMelee * Random.Range(0.8f, 2.5f);


                if ((int)enemyCas > enemyNum)
                    enemyCas = enemyNum;

                if ((int)playerCas > soldierNum)
                    playerCas = soldierNum;

                enemyCasMelee += (int)enemyCas;
                playerCasMelee += (int)playerCas;

                soldierNum -= (int)playerCas;
                enemyNum -= (int)enemyCas;
            }


            myString += " On melee we killed " + (enemyCasMelee).ToString() + " and we lost "
            + (playerCasMelee).ToString() + " soldiers";
        }

        if (soldierNum > 0)
        {
            myString += " We won the fight.";
        }
        else
        {
            myString += "We lost the fight.";
        }
        //this is for the exploration/loot thing
        soldiers = soldierNum;

        totalplayerLoss += playerCasMelee;
        //change this later
        this.gameObject.GetComponent<Resource>().changeSoldier(-totalplayerLoss);

        combatResultUIObj.SetActive(true);
        combatUIObj.SetActive(false);


        combatResultTextObj.GetComponent<Text>().text = myString;

        exploredMap[currentX, currentY] = true;

        return myString;


    }


}
