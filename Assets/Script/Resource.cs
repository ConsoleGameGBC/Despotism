using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resource : MonoBehaviour {

    int Food;
    int Water;
    int Fuel;
    int Power;
    int Medical;

    int Population;

    int popUnemployed;
    int popSoldier;
    int popWorker;
    int popElder;
    int popYouth;

    

    [SerializeField]
    GameObject foodAmountObj;

    [SerializeField]
    GameObject waterAmountObj;

    [SerializeField]
    GameObject fuelAmountObj;

    [SerializeField]
    GameObject medicalAmountObj;

    [SerializeField]
    GameObject powerAmountObj;

    [SerializeField]
    GameObject totalPopObj;

    [SerializeField]
    GameObject unemployedPopObj;

    [SerializeField]
    GameObject soldierPopObj;

    [SerializeField]
    GameObject workerPopObj;

    [SerializeField]
    GameObject elderYouthPopObj;




    // Use this for initialization
    void Start () {
        setResources();
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

    public void setResources()
    {
        foodAmountObj.GetComponent<Text>().text = Food.ToString();
        waterAmountObj.GetComponent<Text>().text = Water.ToString();
        fuelAmountObj.GetComponent<Text>().text = Fuel.ToString();
        medicalAmountObj.GetComponent<Text>().text = Medical.ToString();
        powerAmountObj.GetComponent<Text>().text = Power.ToString();

        Population = popUnemployed + popSoldier + popWorker + popElder + popYouth;

        totalPopObj.GetComponent<Text>().text = Population.ToString();
        unemployedPopObj.GetComponent<Text>().text = popUnemployed.ToString();
        soldierPopObj.GetComponent<Text>().text = popSoldier.ToString();
        workerPopObj.GetComponent<Text>().text = popWorker.ToString();
        elderYouthPopObj.GetComponent<Text>().text = (popElder + popYouth).ToString();


    }

    void decreasePop()
    {
        while(Population < popUnemployed + popSoldier + popWorker + popElder + popYouth)
        {
            switch(Random.Range(1, 6))
            {
                case 1:
                    if (popUnemployed > 0)
                        popUnemployed--;
                    break;
                case 2:
                    if (popSoldier > 0)
                        popSoldier--;
                    break;
                case 3:
                    if (popWorker > 0)
                        popWorker--;
                    break;
                case 4:
                    if (popElder > 0)
                        popElder--;
                    break;
                case 5:
                    if (popYouth > 0)
                        popYouth--;
                    break;
            }

        }
    }
	
	public void endTurn(){
        Population = popUnemployed + popSoldier + popWorker + popElder + popYouth;

		
		if (Water > Population) {
			Water -= Population;
		} else {
            int difference = Population - Water;
			Water = 0;
            Population -= difference;
            decreasePop();
			// Some people are dehydrated. Add code.
		}

        if (Food >= Population)
        {
            Food -= Population;
        }
        else
        {

            int difference = Population - Food;
            Food = 0;
            Population -= difference;
            decreasePop();

            // Some people starve. Decide what to do?
        }

        setResources ();
		
	}



	
	public void changeFood(int amount){
		Food += amount;
	}
	
	public void changeWater(int amount){
		Water += amount;
	}
	
	public void changeFuel(int amount){
		Fuel += amount;
	}

    public void changeMedical(int amount)
    {
        Medical += amount;
    }

    public void changeUnemployed(int amount)
    {
        popUnemployed += amount;
    }
    public void changeSoldier(int amount)
    {
        popSoldier += amount;
    }
    public void changeWorker(int amount)
    {
        popWorker += amount;
    }
    public void changeElder(int amount)
    {
        popElder += amount;
    }
    public void changeYouth(int amount)
    {
        popYouth += amount;
    }
}
