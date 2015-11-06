using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resource : MonoBehaviour {

    int Food;
    int Water;
    int Fuel;
    int Power;
    int Medical;

    int FoodChange = 0;
    int WaterChange = 0;
    int FuelChange = 0;
    int PowerChange = 0;
    int MedicalChange = 0;


    int Population;

    int popUnemployed;
    int popSoldier;
    int popWorker;
    int popElder;
    int popYouth;


    [SerializeField]
    GameObject foodChangeObj;
    [SerializeField]
    GameObject waterChangeObj;
    [SerializeField]
    GameObject fuelChangeObj;
    [SerializeField]
    GameObject medicalChangeObj;
    [SerializeField]
    GameObject powerChangeObj;

    [SerializeField]
    GameObject powerChange;

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

	[SerializeField] GameObject reportFoodStockObj;
	[SerializeField] GameObject reportFoodWorkerSoldierObj;
	[SerializeField] GameObject reportFoodGiveawayObj;
	[SerializeField] GameObject reportFoodProductionObj;
	[SerializeField] GameObject reportFoodImportObj;
	[SerializeField] GameObject reportFoodExportObj;
    [SerializeField]
    GameObject reportFoodEstimate;
    [SerializeField]
    GameObject reportWaterEstimate;


	[SerializeField] GameObject reportWaterStockObj;
	[SerializeField] GameObject reportWaterWorkerSoldierObj;
	[SerializeField] GameObject reportWaterGiveawayObj;
	[SerializeField] GameObject reportWaterFarmObj;
	[SerializeField] GameObject reportWaterImportObj;
	[SerializeField] GameObject reportWaterExportObj;


    // Use this for initialization
    void Start () {
        setInitialResources();
        setResources();
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
    void setInitialResources()
    {
        popSoldier = Random.Range(50, 70);
        popElder = Random.Range(10, 30);
        popYouth = Random.Range(12, 20);
        popWorker = Random.Range(50, 80);
        popUnemployed = Random.Range(10, 20);

        Food = Random.Range(660, 1500);
        Water = Random.Range(660, 1500);
        Medical = Random.Range(200, 500);
        Fuel = Random.Range(200, 500);
        Power = 0;
    }
    public int getSoldierNum()
    {
        return popSoldier;
    }
    public int getFuel()
    {
        return Fuel;
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

        reportFoodStockObj.GetComponent<Text>().text = Food.ToString();
        reportFoodWorkerSoldierObj.GetComponent<Text>().text = (popWorker + popSoldier).ToString();
        reportFoodGiveawayObj.GetComponent<Text>().text = (popUnemployed + popElder + popYouth).ToString();
        reportFoodProductionObj.GetComponent<Text>().text = "0";
        reportFoodImportObj.GetComponent<Text>().text = "0";
        reportFoodExportObj.GetComponent<Text>().text = "0";

        reportFoodEstimate.GetComponent<Text>().text = (Food - Population).ToString();
        reportWaterEstimate.GetComponent<Text>().text = (Water - Population).ToString();

        reportWaterStockObj.GetComponent<Text>().text = Water.ToString();
        reportWaterWorkerSoldierObj.GetComponent<Text>().text = (popWorker + popSoldier).ToString();
        reportWaterGiveawayObj.GetComponent<Text>().text = (popUnemployed + popElder + popYouth).ToString();
        reportWaterFarmObj.GetComponent<Text>().text = "0";
        reportWaterImportObj.GetComponent<Text>().text = "0";
        reportWaterExportObj.GetComponent<Text>().text = "0";

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

        foodChangeObj.GetComponent<Text>().text = FoodChange.ToString();
        FoodChange = 0;
        waterChangeObj.GetComponent<Text>().text = WaterChange.ToString();
        WaterChange = 0;
        fuelChangeObj.GetComponent<Text>().text = FuelChange.ToString();
        FuelChange = 0;
        powerChangeObj.GetComponent<Text>().text = PowerChange.ToString();
        PowerChange = 0;
        medicalChangeObj.GetComponent<Text>().text = MedicalChange.ToString();
        MedicalChange = 0;

        setResources ();
		
	}



	
	public void changeFood(int amount){
		Food += amount;
        FoodChange += amount;
	}
	
	public void changeWater(int amount){
		Water += amount;
        WaterChange += amount;
	}
	
	public void changeFuel(int amount){
		Fuel += amount;
        FuelChange += amount;
	}

    public void changeMedical(int amount)
    {
        Medical += amount;
        MedicalChange += amount;
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
