using UnityEngine;
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
        foodAmountObj.GetComponent<GUIText>().text = Food.ToString();
        waterAmountObj.GetComponent<GUIText>().text = Water.ToString();
        fuelAmountObj.GetComponent<GUIText>().text = Fuel.ToString();
        medicalAmountObj.GetComponent<GUIText>().text = Medical.ToString();
        powerAmountObj.GetComponent<GUIText>().text = Power.ToString();

        Population = popUnemployed + popSoldier + popWorker + popElder + popYouth;

        totalPopObj.GetComponent<GUIText>().text = Population.ToString();
        unemployedPopObj.GetComponent<GUIText>().text = popUnemployed.ToString();
        soldierPopObj.GetComponent<GUIText>().text = popSoldier.ToString();
        workerPopObj.GetComponent<GUIText>().text = popWorker.ToString();
        elderYouthPopObj.GetComponent<GUIText>().text = (popElder + popYouth).ToString();


    }
	
	public void endTurn(){
		if (Food >= Population) {
			Food -= Population;
		} else {
			Food = 0;
			// Some people starve. Decide what to do?
		}
		
		if (Water > Population) {
			Water -= Population;
		} else {
			Water = 0;
			// Some people are dehydrated. Add code.
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
