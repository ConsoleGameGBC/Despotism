using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resource : MonoBehaviour {
    int turnNum = 0;
    int turnsToWin = 60;
    int popToLose = 100;

    float productionSpeed = 1.3f;
    float scavangeSpeed = 0.8f;

    float workerMorale = 1.0f;
    float soldierMorale = 1.0f;
    float unemployedMorale = 1.0f;




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
    public int popSoldier;
    int popWorker;
    int popElder;
    int popYouth;

    [Header("Change Objects")]
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

    //[SerializeField]
    //GameObject powerChange;

    [Header("Amount Objects")]
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

    [Header("Population Objects")]

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

    [Header("Pop Percentage Objects")]
    [SerializeField]
    GameObject unemployedPercObj;
    [SerializeField]
    GameObject soldierPercObj;
    [SerializeField]
    GameObject workerPercObj;
    [SerializeField]
    GameObject elderYouthPercObj;

    [Header("Report Objects")]

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


        Population = popUnemployed + popSoldier + popWorker + popElder + popYouth;

        unemployedPercObj.GetComponent<Text>().text = ((popUnemployed * 100) / Population).ToString() + "%";
        soldierPercObj.GetComponent<Text>().text = ((popSoldier * 100) / Population).ToString() + "%";
        workerPercObj.GetComponent<Text>().text = ((popWorker * 100) / Population).ToString() + "%";
        elderYouthPercObj.GetComponent<Text>().text = (((popElder + popYouth) * 100) / Population).ToString() + "%";
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


        unemployedPercObj.GetComponent<Text>().text = ((popUnemployed * 100) / Population).ToString() + "%";
        soldierPercObj.GetComponent<Text>().text = ((popSoldier * 100) / Population).ToString() + "%";
        workerPercObj.GetComponent<Text>().text = ((popWorker * 100) / Population).ToString() + "%";
        elderYouthPercObj.GetComponent<Text>().text = (((popElder + popYouth) * 100) / Population).ToString() + "%";


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
   public void decreasePop(int amount)
    {
        Population -= amount;
        decreasePop();
    }
     
   public void decreasePop()
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
	
    public void WinGame()
    {
        Application.LoadLevel(1);
    }

    public void LoseGame()
    {
        Application.LoadLevel(2);
    }


	public void endTurn(){
        turnNum++;

        if (turnNum >= turnsToWin)
            WinGame();

        Population = popUnemployed + popSoldier + popWorker + popElder + popYouth;


        //PRODUCTION
        changeFood((int)(popWorker * productionSpeed *workerMorale));
        changeWater((int)(popWorker * productionSpeed * workerMorale));
        //SCAVANGE
        changeFood((int)(popUnemployed * scavangeSpeed *unemployedMorale));
        changeWater((int)(popUnemployed * scavangeSpeed * unemployedMorale));




        if (Water > Population) {
            //Water -= Population;
            changeWater(-Population);
		} else {
            int difference = Population - Water;
            //Water = 0;
            changeWater(-Water);
            Population -= difference;
            decreasePop();
			// Some people are dehydrated. Add code.
		}

        if (Food >= Population)
        {
            //Food -= Population;
            changeFood(-Population);
        }
        else
        {

            int difference = Population - Food;
            //Food = 0;
            changeFood(-Food);
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


        this.gameObject.GetComponent<RandomEvents>().publishEvent();
        this.gameObject.GetComponent<Combat>().changePaperBack();


        if (Population < popToLose)
            LoseGame();
		
	}



	
	public void changeFood(int amount){
		Food += amount;
        FoodChange += amount;
        setResources();
	}
	
	public void changeWater(int amount){
		Water += amount;
        WaterChange += amount;
        setResources();
	}
	
	public void changeFuel(int amount){
		Fuel += amount;
        FuelChange += amount;
        setResources();
	}

    public void changeMedical(int amount)
    {
        Medical += amount;
        MedicalChange += amount;
        setResources();
    }

    public void changeUnemployed(int amount)
    {
        popUnemployed += amount;
        setResources();
    }
    public void changeSoldier(int amount)
    {
        popSoldier += amount;
        setResources();
    }
    public void changeWorker(int amount)
    {
        popWorker += amount;
        setResources();
    }
    public void changeElder(int amount)
    {
        popElder += amount;
        setResources();
    }
    public void changeYouth(int amount)
    {
        popYouth += amount;
        setResources();
    }
    
    public int getSoldierPop()
    {
        return popSoldier;
    }
    public int getWorkerPop()
    {
        return popWorker;
    }
    public int getUnemployedPop()
    {
        return popUnemployed;
    }
    public int getYouthNElderPop() { return popYouth + popElder; }

    public void trainUnempToSoldier(int amount)
    {
        changeUnemployed(-amount);
        changeSoldier(amount);
    }
    public void trainUnempToWorker(int amount)
    {
        changeUnemployed(-amount);
        changeWorker(amount);
    }
    public void trainWorkerToSoldier(int amount)
    {
        changeWorker(-amount);
        changeSoldier(amount);
    }
    public void trainSoldierToWorker(int amount)
    {
        changeSoldier(-amount);
        changeWorker(amount);
    }



}
