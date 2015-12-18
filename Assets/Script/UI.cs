using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {


    Text HelpUILeft;
    Text HelpUIRight;
    GameObject SunCenter;
    GameObject Sun;
    GameObject LeftPage;
    GameObject RightPage;
    GameObject CurrentUI;
    GameObject MainCamera;
    GameObject MainMenu;
    GameObject TurnReport;
    Vector3 MenuPosition;
    Vector3 ReportPosition;
    Quaternion MenuRotation;
    Quaternion ReportRotation;
    Vector3 FocusLocationPos;
    Quaternion FocusLocationRot;
    Vector3 FocusPos;
    Quaternion FocusRot;
    Vector3 UnfocusPos;
    Quaternion UnfocusRot;

    GameObject Tips;
    Resource resource;
    RandomEvents randomEvents;
    GameObject MulitaryUI;
    Vector3 MulitaryPos;
    Quaternion MulitaryRot;
    int SoldierNum;

    GameObject StockUI;
    Vector3 StockPos;
    Quaternion StockRot;

	GameObject AssignUI;
	Vector3 AssignPos;
	Quaternion AssignRot;

    blinkScript BlinkScript;

    bool showTips = false;
    public float speed = 20f;
    float step;
    float TipTime;
    public bool fold = true;
    bool startFolding;
    public int TerrainType = 1;
    bool switchingUI;
    bool controlDisable;
    bool turnIsEnd;
    int TimePassing = 0;
    // Use this for initialization

   
    
    [Header("Erdem's UI Thingy")]
    float timeSinceLastXChange = 5;
    [SerializeField]
    float inputDelay = 0.3f;
    [SerializeField]
    GameObject myManager;
    blinkScript myBlinkScript;

    public bool currentActionIsAttack = true;

	public AudioClip confirm;
	public AudioClip folder;
	public AudioClip openbook;
	public AudioClip click;
	public AudioClip morning;
	public AudioClip newevent;
    enum UIChoice
    {
        
        TurnReport,
        Mulitary,
        Stock,
		Assign,
		MainMenu
    } ;

	public enum MulitaryAction
	{
		Attack,
		Explore
	};

	public enum AssignAction
	{
		Assign,
		Transfer
	};

	public enum AssignPop
	{
		Worker,
		Soldier,
		Unemployed
	};

	AssignAction assignAction = new AssignAction ();
	AssignPop assignPopTo = new AssignPop ();
	AssignPop assignPopFrom = new AssignPop ();
	bool AssignActionAssigned = false;
	int AssignStatus = 0;
	int AssignAmount = 0;

    UIChoice lastUI = new UIChoice();
    UIChoice currentUI = new UIChoice();

    Combat myCombatClass;
	MulitaryAction mulitaryAction = new MulitaryAction ();
	public int MulitaryStatus = 0;
    int RandomEventOptionStatus = 0;
	bool MulitaryActionAssigned = false;
    bool RandomEventFinished = false;

	void Start () {
        //Erdem's manager assignment
        myBlinkScript = myManager.GetComponent<blinkScript>();

        //Cursor.visible = false;

        SunCenter = GameObject.Find("Center");
        Sun = GameObject.Find("Sun");

        myCombatClass = GameObject.Find("GameManager").GetComponent<Combat>();

        resource = GameObject.Find("GameManager").GetComponent<Resource>();
        randomEvents = GameObject.Find("GameManager").GetComponent<RandomEvents>();

        BlinkScript = GameObject.Find("GameManager").GetComponent<blinkScript>();

        HelpUILeft = GameObject.Find("HelpUILeft").GetComponent<Text>();
        HelpUIRight = GameObject.Find("HelpUIRight").GetComponent<Text>();

        Tips = GameObject.Find("Tips");

        MainCamera = GameObject.Find("Main Camera");
        UnfocusPos = MainCamera.transform.position;
        UnfocusRot = MainCamera.transform.rotation;
        FocusPos = GameObject.Find("FocusPoint").transform.position;
        FocusRot = GameObject.Find("FocusPoint").transform.rotation;
        currentUI = UIChoice.MainMenu;
        MainMenu = GameObject.Find("MainMenu");
        MenuPosition = GameObject.Find("MenuPlaceHolder").transform.position;
        MenuRotation = GameObject.Find("MenuPlaceHolder").transform.rotation;
        
        MulitaryUI = GameObject.Find("MulitaryUI");
        MulitaryPos = MulitaryUI.transform.position;
        MulitaryRot = MulitaryUI.transform.rotation;

        StockUI = GameObject.Find("StockUI");
        StockPos = StockUI.transform.position;
	        StockRot = StockUI.transform.rotation;

			AssignUI = GameObject.Find("AssignUI");
			AssignPos = AssignUI.transform.position;
			AssignRot = AssignUI.transform.rotation;

	        TurnReport = GameObject.Find("TurnReport");
	        ReportPosition = TurnReport.transform.position;
	        ReportRotation = TurnReport.transform.rotation;
	        FocusLocationPos = GameObject.Find("FocusLocation").transform.position;
	        FocusLocationRot = GameObject.Find("FocusLocation").transform.rotation;
	        CurrentUI = MainMenu;
	        LeftPage = GameObject.Find("MainMenuLeftPage");
	        RightPage = GameObject.Find("MainMenuRightPage");

            SoldierNum = resource.popSoldier;
	        //MulitaryStatusChoice(0);
		}



		public void MulitaryStatusChoice(int value)
		{
	        //Debug.Log(MulitaryStatus);
			switch(MulitaryStatus)
			{
			case (0):
                Debug.Log("Military action is " + mulitaryAction);
                Debug.Log("Military action + value is " + mulitaryAction+value);

                switch (mulitaryAction += value) 
			    {
            case (MulitaryAction.Attack):
					GameObject.Find("MilitaryAction").GetComponent<Text>().text = "Attack";
                    myCombatClass.changeActionExplanation(false,TerrainType);
                        currentActionIsAttack = true;
					break;
				case(MulitaryAction.Explore):
					GameObject.Find("MilitaryAction").GetComponent<Text>().text = "Explore";
                    myCombatClass.changeActionExplanation(true, TerrainType);
                        currentActionIsAttack = false;
					break;
				default:
                    mulitaryAction -= value;
					break;
			    }
            
			break;
		case(1): //Enable Map
			break;
        case (2):
            if (SoldierNum > resource.popSoldier)
            {
                SoldierNum = resource.popSoldier;
            }
            if (SoldierNum + value <= resource.popSoldier && SoldierNum + value > 0)
                {
                    SoldierNum += value;
                }
                 
                GameObject.Find("MilitaryAssignText").GetComponent<Text>().text = SoldierNum.ToString();
            break;
		case(3):
                MulitaryActionAssigned = true;
                callCombat();
                MulitaryStatus = 0;
               
            break;
		}
	}

	void AssignStatusChoice(int value)
	{
		switch(AssignStatus)
		{
			case (0):
				switch (assignAction += value) 
				{
				case(AssignAction.Assign):
					GameObject.Find("AssignAction").GetComponent<Text>().text = "Train";
					break;
				case(AssignAction.Transfer):
					GameObject.Find("AssignAction").GetComponent<Text>().text = "Hastened Train";
					break;
				default:
					assignAction -= value;
					break;
				}
				break;
			case(1): 
                    switch (assignPopFrom += value)
					{
						case(AssignPop.Soldier):
							GameObject.Find("AssignFromType").GetComponent<Text>().text = "Soldier";
				  			break;
						case(AssignPop.Worker):
							GameObject.Find("AssignFromType").GetComponent<Text>().text = "Worker";
							break;
						case(AssignPop.Unemployed):
							GameObject.Find("AssignFromType").GetComponent<Text>().text = "Unemployed";
							break;
						default:
                            assignPopFrom -= value;
							break;
					}
				break;
			case(2): 
                   switch (assignPopTo += value)
                {
                    case (AssignPop.Soldier):
                        GameObject.Find("AssignToType").GetComponent<Text>().text = "Soldier";
                        break;
                    case (AssignPop.Worker):
                        GameObject.Find("AssignToType").GetComponent<Text>().text = "Worker";
                        break;
                    //case (AssignPop.Unemployed):
                    //    GameObject.Find("AssignToType").GetComponent<Text>().text = "Unemployed";
                    //    break;
                    default:
                        assignPopTo -= value;
                        break;
                }
                break;
            case (3):
              
                if(assignAction == AssignAction.Assign)
				{
                    switch (assignPopFrom)
                    {
                        case (AssignPop.Soldier):
                            if (AssignAmount + value <= 10 && AssignAmount + value <= resource.getSoldierPop() && AssignAmount + value > 0)
                            {
                                AssignAmount += value;
                            }
                            break;
                        case (AssignPop.Worker):
                            if (AssignAmount + value <= 10 && AssignAmount + value <= resource.getWorkerPop() && AssignAmount + value > 0 && assignPopTo != AssignPop.Soldier)
                            {
                                AssignAmount += value;
                            }
                            else if (AssignAmount + value <= 10 && AssignAmount + value <= resource.getWorkerPop() && AssignAmount + value > 0 && AssignAmount + value <= resource.getWeapons())
                            {
                                AssignAmount += value;
                            }
                            break;
                        case (AssignPop.Unemployed):
                            if (AssignAmount + value <= 10 && AssignAmount + value <= resource.getUnemployedPop() && AssignAmount + value > 0 && assignPopTo != AssignPop.Soldier)
                            {
                                AssignAmount += value;
                            }
                            else if (AssignAmount + value <= 10 && AssignAmount + value <= resource.getUnemployedPop() && AssignAmount + value > 0 && AssignAmount + value <= resource.getWeapons())
                            {
                                AssignAmount += value;
                            }
                            
                            break;
                    }
				}
				else if(assignAction == AssignAction.Transfer)
				{
                    switch (assignPopFrom)
                    {
                        case (AssignPop.Soldier):
                            if (AssignAmount + value <= resource.getSoldierPop() && AssignAmount + value > 0)
                            {
                                AssignAmount += value;
                            }
                            break;
                        case (AssignPop.Worker):
                            if (AssignAmount + value <= resource.getWorkerPop() && AssignAmount + value > 0 && assignPopTo != AssignPop.Soldier)
                            {
                                AssignAmount += value;
                            }
                            else if (AssignAmount + value <= resource.getWorkerPop() && AssignAmount + value > 0 && AssignAmount + value <= resource.getWeapons())
                            {
                                AssignAmount += value;
                            }
                            break;
                        case (AssignPop.Unemployed):
                            if (AssignAmount + value <= resource.getUnemployedPop() && AssignAmount + value > 0 && assignPopTo != AssignPop.Soldier)
                            {
                                AssignAmount += value;
                            }
                            else if (AssignAmount + value <= resource.getUnemployedPop() && AssignAmount + value > 0 && AssignAmount + value <= resource.getWeapons())
                            {
                                AssignAmount += value;
                            }
                            break;
                    }
				}
                GameObject.Find("AssignNumber").GetComponent<Text>().text = AssignAmount.ToString();
				break;
			case(4):

                if (assignPopTo == assignPopFrom)
                {
                    GameObject.Find("AssignDetailText").GetComponent<Text>().text = "False assignment will not be permitted.";
                }
                else
                {
                    trainingFunc();
                    if (AssignAmount>1)
                    GameObject.Find("AssignDetailText").GetComponent<Text>().text = AssignAmount + " " + assignPopFrom + "s has been " + assignAction + " to " + assignPopTo + ".";
                    else if(AssignAmount == 1)
                    GameObject.Find("AssignDetailText").GetComponent<Text>().text = AssignAmount + " " + assignPopFrom + " has been " + assignAction + " to " + assignPopTo + ".";
                }
				AssignStatus = 0;
				AssignActionAssigned = true;
				
				break;
		}
	}

    void trainingFunc()
    {
        switch (assignPopFrom)
        {
            case (AssignPop.Soldier):
                if(assignAction == AssignAction.Assign)
                {
                    resource.trainSoldierToWorker(AssignAmount,false);
                }
                else
                {
                    resource.trainSoldierToWorker(AssignAmount, true);
                }
                break;
            case (AssignPop.Worker):
                if (assignAction == AssignAction.Assign)
                {
                    resource.trainWorkerToSoldier(AssignAmount, false);
                }
                else
                {
                    resource.trainWorkerToSoldier(AssignAmount, true);
                }
                break;
            case (AssignPop.Unemployed):
                switch(assignPopTo)
                {
                    case (AssignPop.Soldier):
                        if (assignAction == AssignAction.Assign)
                        {
                            resource.trainUnempToSoldier(AssignAmount, false);
                        }
                        else
                        {
                            resource.trainUnempToSoldier(AssignAmount, true);
                        }
                        break;
                    case (AssignPop.Worker):
                        if (assignAction == AssignAction.Assign)
                        {
                            resource.trainUnempToWorker(AssignAmount, false);
                        }
                        else
                        {
                            resource.trainUnempToWorker(AssignAmount, true);
                        }
                        break;
                }
                break;
        }

    }

    void RandomEventChoice()
    {
        randomEvents.madeChoice(RandomEventOptionStatus);
        
        // GameObject.Find("RandEventContent").GetComponent<Text>().text = 
        
    }

	void turnEnd()
	{
        if (fold == false)
        {
            fold = true;
            startFolding = true;

        }

        controlDisable = true;
        turnIsEnd = true;
       
	}

	public void MulitaryChoice(bool temp)
	{
		if(temp)
		{
			MulitaryStatus++;
		}
		else
		{
			MulitaryStatus--;
		}


	}

	void AssignChoice(bool temp)
	{
		if (temp) {
			AssignStatus++;
		} else {
			AssignStatus--;
		}
	}

    void callCombat()
    {
        if (MulitaryStatus == 3) {
            switch (mulitaryAction)
            {
                case (MulitaryAction.Attack):
                    myCombatClass.combatResult(false, TerrainType, SoldierNum);
                    break;
                case (MulitaryAction.Explore):
                    myCombatClass.explorationResult(true, TerrainType, SoldierNum);
                    break;
                default:
                    Debug.Log("Error in combat call");
                    break;
            }
        }
    }


    void ReportChoice(int value)
    {
        if (RandomEventFinished == false)
        {
            RandomEventOptionStatus += value;
            switch (RandomEventOptionStatus)
            {
                case (0):
                    GameObject.Find("OptionContent").GetComponent<Text>().text = randomEvents.outputOption(RandomEventOptionStatus);
                    break;
                case (1):
                    GameObject.Find("OptionContent").GetComponent<Text>().text = randomEvents.outputOption(RandomEventOptionStatus);
                    break;
                case (2):
                    GameObject.Find("OptionContent").GetComponent<Text>().text = randomEvents.outputOption(RandomEventOptionStatus);
                    break;
                default:
                    RandomEventOptionStatus -= value;
                    break;
            }
        }
    }



	// Update is called once per frame
	void Update () {
        //Debug.Log(TipTime);
        if (fold == false)
        {
            switch (currentUI)
            {
                case (UIChoice.Mulitary):
                    //MulitaryStatusChoice(1);
                    if (MulitaryStatus == 0 && MulitaryActionAssigned == false)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("MilitaryAction").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to choose military action,, A to confirm.";
                    }
                    else if (MulitaryStatus == 1)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("Cordinate").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Up/Down/Left/Right to move around the map, A to confirm.";
                    }
                    else if (MulitaryStatus == 2)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("MilitaryAssignText").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to adjust number of soldier for current action, A to confirm.";
                    }
                    else if (MulitaryStatus == 3)
                    {
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "We have received the result of this action. Press down to zoom out.";
                        BlinkScript.stopBlinking();
                    }
                    break;
                case (UIChoice.TurnReport):
                    //ReportChoice(1)
                    BlinkScript.assignTextToBlink(GameObject.Find("OptionContent").GetComponent<Text>());
                    showTips = true;
                    Tips.GetComponentInChildren<Text>().text = "Left & Right to switch between solutions, A to confirm.";
                    if (RandomEventFinished == true)
                    {
                        BlinkScript.stopBlinking();
                    }
                    break;
                case (UIChoice.Assign):
                    //AssignStatusChoice(1);
                    if (AssignStatus == 0 && AssignActionAssigned == false)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("AssignAction").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to choose assign action, A to confirm.";
                    }
                    else if (AssignStatus == 1)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("AssignFromType").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to choose population type, A to confirm.";
                    }
                    else if (AssignStatus == 2)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("AssignToType").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to choose population type, A to confirm.";
                    }
                    else if (AssignStatus == 3)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("AssignNumber").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Left & Right to adjust number of people for current action, A to confirm";
                    }
                    else if (AssignStatus == 4)
                    {
                        BlinkScript.assignTextToBlink(GameObject.Find("AssignDetailText").GetComponent<Text>());
                        showTips = true;
                        Tips.GetComponentInChildren<Text>().text = "Assign action processed, press Down to zoom out";
                    }
                    if (AssignActionAssigned == true)
                    {
                        BlinkScript.stopBlinking();
                    }
                    break;
            }
        }
        

        if (showTips == true)
        {
            TipTime += Time.deltaTime;
        }
        else if(showTips == false)
        {
            if (fold == true)
            {
                showTips = true;
                Tips.GetComponentInChildren<Text>().text = "Left/Right to switch the report Up/Down to zoom";
            }
        }
        if(TipTime > 5)
        {
            showTips = false;
            TipTime = 0;
        }


        if (fold == false)
        {
            switch (currentUI)
            {
                case (UIChoice.Stock):
                    HelpUILeft.text = "Here are our resources and their expected change for tomorrow.";
                    HelpUIRight.text = "";
                    break;
                case (UIChoice.TurnReport):
                    HelpUILeft.text = "Your resources. We need water and food to survive.";
                    HelpUIRight.text = "This is the daily event. Choose carefully. You can not end the day without resolving this event first.";
                    break;
                case (UIChoice.Assign):
                    HelpUILeft.text = "The quality and morale of your soldiers and workers affect their performance. \n \nYou will need extra weapons to train new soldiers.";
                    HelpUIRight.text = "Here, you can train more workers or soldiers. If you are in a hurry you can assign them in bulk with 'hastened train' but lack of proper training will affect their performance.";
                    break;
                 case (UIChoice.Mulitary):
                    HelpUILeft.text = "Here you choose the location of your military assignment.";
                    HelpUIRight.text = "We can explore an area to learn about its danger levels and if it has loot. Attacking loots the area of those resources, if any exists. Attacking an area without proper intel is dangerous.";
                    break;
                 case (UIChoice.MainMenu):
                    HelpUILeft.text = "Welcome to the office, Chief.";
                    HelpUIRight.text = "Survive 30 days. Good luck.";
                    break;
            }

        }
        else
        {
            HelpUILeft.text = "";
            HelpUIRight.text = "";
        }

        if(TimePassing > 0)
        {        
            float SunSpeed = 50;
            Sun.transform.LookAt(SunCenter.transform.position);
            SunCenter.transform.Rotate(Vector3.forward * Time.deltaTime * SunSpeed );
            if(SunCenter.transform.rotation.eulerAngles.z >= 359.0f)
            {
                Quaternion temp = Quaternion.identity;
                temp = Quaternion.EulerAngles(0, 0, 0);
                SunCenter.transform.rotation = temp;
                TimePassing = 0;
                controlDisable = false;
            }

            if (SunCenter.transform.rotation.eulerAngles.z > 0 && SunCenter.transform.rotation.eulerAngles.z < 180)
            {
                if(Sun.GetComponent<Light>().intensity > 0)
                {
                    Sun.GetComponent<Light>().intensity -= 0.1f * SunSpeed * Time.deltaTime;
                }
            }
            else if (Sun.GetComponent<Light>().intensity < 8)
            {
                Sun.GetComponent<Light>().intensity += 0.1f * SunSpeed * Time.deltaTime;
            }
        }

        if (turnIsEnd == true && currentUI != UIChoice.TurnReport && startFolding == false)
        {
            lastUI = currentUI;
            currentUI = UIChoice.TurnReport;
            UIChanged(currentUI, false);
            GameObject.Find("AssignDetailText").GetComponent<Text>().text = "";
            MulitaryActionAssigned = false;
            AssignAmount = 0;
			AssignActionAssigned = false;
            RandomEventOptionStatus = 0;
            SoldierNum = resource.popSoldier;
            RandomEventFinished = false;
            controlDisable = true;
            TimePassing = 1;
            turnIsEnd = false;
        }
        else if (turnIsEnd == true && currentUI == UIChoice.TurnReport)
        {
            MulitaryActionAssigned = false;
			AssignActionAssigned = false;
            AssignAmount = 0;
            RandomEventOptionStatus = 0;
            SoldierNum = resource.popSoldier;
            RandomEventFinished = false;
            GameObject.Find("AssignDetailText").GetComponent<Text>().text = "";
            controlDisable = true;
            TimePassing = 1;
            turnIsEnd = false;
        }




        step = speed * Time.deltaTime;
        if (fold == true && startFolding == true)
        {
            if (MainCamera.transform.position != UnfocusPos && MainCamera.transform.rotation != UnfocusRot)
            {
                MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, UnfocusPos, step);
                MainCamera.transform.rotation = Quaternion.RotateTowards(MainCamera.transform.rotation, UnfocusRot, step*50);
            }
            if(LeftPage.transform.rotation.eulerAngles.z > 1)
            {
                LeftPage.transform.rotation = Quaternion.RotateTowards(LeftPage.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 1)), step * 50);
            }
            if (LeftPage.transform.rotation.eulerAngles.z <= 1)
            {
                controlDisable = false;
                startFolding = false;
            }
        }
        else if (fold == false && startFolding == true)
        {
            if (MainCamera.transform.position != FocusPos && MainCamera.transform.rotation != FocusRot)
            {
                MainCamera.transform.position = Vector3.MoveTowards(MainCamera.transform.position, FocusPos, step);
                MainCamera.transform.rotation = Quaternion.RotateTowards(MainCamera.transform.rotation, FocusRot, step * 50);
            }
            if (LeftPage.transform.rotation.eulerAngles.z < 180)
            {
                LeftPage.transform.rotation = Quaternion.RotateTowards(LeftPage.transform.rotation, Quaternion.Euler(new Vector3(0, 0, 180)), step * 50);
            }
            if (LeftPage.transform.rotation.eulerAngles.z >=180)
            {
                controlDisable = false;
                startFolding = false;

            }
        }
        if (controlDisable == false)
        {
			if(Input.GetButtonDown("A"))
			{
                if (RandomEventFinished == true)
                {
                    turnEnd();
                    resource.endTurn();
                    showTips = true;
                    Tips.GetComponentInChildren<Text>().text = "Another peacful day has passed.";
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = morning;
					audio.Play();

					
                }
                else
                {
                    showTips = true;
                    Tips.GetComponentInChildren<Text>().text = "Events under Turn report have to be handled before ending today.";
                }
			}

            if (Input.GetButtonDown("LeftB") && MulitaryStatus != 1)
            {
                if (fold == false)
                {

					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = click;
					audio.Play();
                    switch (currentUI)
                    {
                        case (UIChoice.Mulitary):
                            MulitaryStatusChoice(-1);
                            break;
                        case (UIChoice.TurnReport):
                            ReportChoice(-1);
                            break;
                        case (UIChoice.Assign):
                            AssignStatusChoice(-1);
                            break;
                    }
                }
                else
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = folder;
					audio.Play();
                    controlDisable = true;
                    lastUI = currentUI;
                    currentUI--;
                    UIChanged(currentUI, false);
                }
            }

            if (Input.GetButtonDown("RightB") && MulitaryStatus != 1)
            {
             if (fold == false)
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = click;
					audio.Play();
					switch(currentUI)
					{
					case (UIChoice.Mulitary):
                      	MulitaryStatusChoice(1);
						break;
                    case (UIChoice.TurnReport):
                        ReportChoice(1);
                        break;
					case (UIChoice.Assign):
						AssignStatusChoice(1);
						break;
					}

                }
                else
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = folder;
					audio.Play();
                    controlDisable = true;
                    lastUI = currentUI;
                    currentUI++;
                    UIChanged(currentUI, true);
                }
            }



			if(Input.GetButtonDown("X"))
			{
				if (fold == false)
				{
					switch(currentUI)
					{
						case (UIChoice.Mulitary):
                            if (MulitaryActionAssigned == false)
                            {
                                MulitaryChoice(true);
                                MulitaryStatusChoice(0);
							AudioSource audio = GetComponent<AudioSource>();
							audio.clip = confirm;
							audio.Play();
                                
                            }
							break;
                        case (UIChoice.TurnReport):
                            if (RandomEventFinished == false)
                            {
                                RandomEventFinished = true;
                                RandomEventChoice();
							AudioSource audio = GetComponent<AudioSource>();
							audio.clip = confirm;
							audio.Play();
                            }
                            break;
						case (UIChoice.Assign):
							if (AssignActionAssigned == false)
							{
								AssignChoice(true);
								AssignStatusChoice(0);
							AudioSource audio = GetComponent<AudioSource>();
							audio.clip = confirm;
							audio.Play();
								
							}
							break;
					}
				}
			}

			if(Input.GetButtonDown("Y"))
			{
				if (fold == false)
				{
					switch(currentUI)
					{
						case (UIChoice.Mulitary):
	                        if (MulitaryActionAssigned == false)
	                        {
	                            MulitaryChoice(false);
							AudioSource audio = GetComponent<AudioSource>();
							audio.clip = confirm;
							audio.Play();
	                        }
							break;
						case (UIChoice.Assign):
							if (AssignActionAssigned == false)
							{
								AssignChoice(false);
							AudioSource audio = GetComponent<AudioSource>();
							audio.clip = confirm;
							audio.Play();
							}
							break;
						}
				}
			}


            if (Input.GetAxis("YAxis") < -0.5 && MulitaryStatus != 1)
            {
                if (fold == true)
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = openbook;
					audio.Play();
                    controlDisable = true;
                    fold = false;
                    startFolding = true;
                }
	
            }
            else if (Input.GetAxis("YAxis") > 0.5 && MulitaryStatus != 1)
            {
                if (fold == false)
                {
					AudioSource audio = GetComponent<AudioSource>();
					audio.clip = openbook;
					audio.Play();
                    controlDisable = true;
                    fold = true;
                    startFolding = true;

                }
            }
            else if (timeSinceLastXChange > inputDelay && Input.GetAxis("XAxis") < -0.5 && MulitaryStatus != 1)
            {
                timeSinceLastXChange = 0;
                if (fold == false)
                {

					switch(currentUI)
					{
					case (UIChoice.Mulitary):
                            MulitaryStatusChoice(-1);
						break;
                    case (UIChoice.TurnReport):
                        ReportChoice(-1);
                        break;
					case (UIChoice.Assign):
						AssignStatusChoice(-1);
						break;
					}
                }
                else
                {

                    controlDisable = true;
                    lastUI = currentUI;
                    currentUI--;
                    UIChanged(currentUI, false);
                }
            }
            else if (timeSinceLastXChange > inputDelay && Input.GetAxis("XAxis") > 0.5 && MulitaryStatus != 1)
            {
                timeSinceLastXChange = 0;
                if (fold == false)
                {
					switch(currentUI)
					{
					case (UIChoice.Mulitary):
                      	MulitaryStatusChoice(1);
						break;
                    case (UIChoice.TurnReport):
                        ReportChoice(1);
                        break;
					case (UIChoice.Assign):
						AssignStatusChoice(1);
						break;
					}

                }
                else
                {
                    controlDisable = true;
                    lastUI = currentUI;
                    currentUI++;
                    UIChanged(currentUI, true);
                }
            }
        }
        if(switchingUI == true)
        {
            bool temp = false;
            switch(lastUI)
            {
                case (UIChoice.MainMenu):
                    MainMenu.transform.position = Vector3.MoveTowards(MainMenu.transform.position, MenuPosition, step);
                    MainMenu.transform.rotation = Quaternion.RotateTowards(MainMenu.transform.rotation, MenuRotation, step * 50);
                    if (MainMenu.transform.position == MenuPosition && MainMenu.transform.rotation == MenuRotation)
                    {
                        temp = true;
                    }
                    break;
                case (UIChoice.TurnReport):
                    TurnReport.transform.position = Vector3.MoveTowards(TurnReport.transform.position, ReportPosition, step);
                    TurnReport.transform.rotation = Quaternion.RotateTowards(TurnReport.transform.rotation, ReportRotation, step * 50);
                    if (TurnReport.transform.position == ReportPosition && TurnReport.transform.rotation == ReportRotation)
                    {
                        temp = true;
                    }
                    break;
                case (UIChoice.Mulitary):
                    MulitaryUI.transform.position = Vector3.MoveTowards(MulitaryUI.transform.position, MulitaryPos, step);
                    MulitaryUI.transform.rotation = Quaternion.RotateTowards(MulitaryUI.transform.rotation, MulitaryRot, step * 50);
                    if (MulitaryUI.transform.position == MulitaryPos && MulitaryUI.transform.rotation == MulitaryRot)
                    {
                        temp = true;
                    }
                    break;
                case (UIChoice.Stock):
                    StockUI.transform.position = Vector3.MoveTowards(StockUI.transform.position, StockPos, step);
                    StockUI.transform.rotation = Quaternion.RotateTowards(StockUI.transform.rotation, StockRot, step * 50);
                    if (StockUI.transform.position == StockPos && StockUI.transform.rotation == StockRot)
                    {
                        temp = true;
                    }
                    break;
				case (UIChoice.Assign):
				AssignUI.transform.position = Vector3.MoveTowards(AssignUI.transform.position, AssignPos, step);
				AssignUI.transform.rotation = Quaternion.RotateTowards(AssignUI.transform.rotation, AssignRot, step * 50);
					if (AssignUI.transform.position == AssignPos && AssignUI.transform.rotation == AssignRot)
					{
						temp = true;
					}
					break;

            }
            if(temp == true && TimePassing == 0)
            {
                switch (currentUI)
                {
                    case (UIChoice.MainMenu):
                        MainMenu.transform.position = Vector3.MoveTowards(MainMenu.transform.position, FocusLocationPos, step);
                        MainMenu.transform.rotation = Quaternion.RotateTowards(MainMenu.transform.rotation, FocusLocationRot, step * 50);
                        if (MainMenu.transform.position == FocusLocationPos && MainMenu.transform.rotation == FocusLocationRot)
                        {
                            switchingUI = false;
                            controlDisable = false;
                        }
                        break;
                    case (UIChoice.TurnReport):
                        TurnReport.transform.position = Vector3.MoveTowards(TurnReport.transform.position, FocusLocationPos, step);
                        TurnReport.transform.rotation = Quaternion.RotateTowards(TurnReport.transform.rotation, FocusLocationRot, step * 50);
                        if (TurnReport.transform.position == FocusLocationPos && TurnReport.transform.rotation == FocusLocationRot)
                        {
                            switchingUI = false;
                            controlDisable = false;
                        }
                        break;
                    case (UIChoice.Mulitary):
                        MulitaryUI.transform.position = Vector3.MoveTowards(MulitaryUI.transform.position, FocusLocationPos, step);
                        MulitaryUI.transform.rotation = Quaternion.RotateTowards(MulitaryUI.transform.rotation, FocusLocationRot, step * 50);
                        if (MulitaryUI.transform.position == FocusLocationPos && MulitaryUI.transform.rotation == FocusLocationRot)
                        {
                            switchingUI = false;
                            controlDisable = false;

                        }
                        break;
                    case (UIChoice.Stock):
                        StockUI.transform.position = Vector3.MoveTowards(StockUI.transform.position, FocusLocationPos, step);
                        StockUI.transform.rotation = Quaternion.RotateTowards(StockUI.transform.rotation, FocusLocationRot, step * 50);
                        if (StockUI.transform.position == FocusLocationPos && StockUI.transform.rotation == FocusLocationRot)
                        {
                            switchingUI = false;
                            controlDisable = false;
                        }
                        break;
				case (UIChoice.Assign):
					AssignUI.transform.position = Vector3.MoveTowards(AssignUI.transform.position, FocusLocationPos, step);
					AssignUI.transform.rotation = Quaternion.RotateTowards(AssignUI.transform.rotation, FocusLocationRot, step * 50);
					if (AssignUI.transform.position == FocusLocationPos && AssignUI.transform.rotation == FocusLocationRot)
					{
						switchingUI = false;
						controlDisable = false;
					}
					break;

                }
            }
        }


        timeSinceLastXChange += Time.deltaTime;
	}
    void UIChanged(UIChoice temp, bool temp2)
    {
        switch (temp)
        {
            case (UIChoice.MainMenu):
                CurrentUI = MainMenu;
                LeftPage = GameObject.Find("MainMenuLeftPage");
                RightPage = GameObject.Find("MainMenuRightPage");
                switchingUI = true;
                break;
            case (UIChoice.TurnReport):
                CurrentUI = TurnReport;
                LeftPage = GameObject.Find("TurnReportLeftPage");
                RightPage = GameObject.Find("TurnReportRightPage");
                switchingUI = true;
                break;
            case (UIChoice.Mulitary):
                CurrentUI = MulitaryUI;
                LeftPage = GameObject.Find("MulitaryLeftPage");
                RightPage = GameObject.Find("MulitaryRightPage");
                switchingUI = true;
                break;
            case (UIChoice.Stock):
                CurrentUI = StockUI;
                LeftPage = GameObject.Find("StockUILeftPage");
                RightPage = GameObject.Find("StockUIRightPage");
                switchingUI = true;
                break;
			case (UIChoice.Assign):
				CurrentUI = AssignUI;
                myBlinkScript.assignTextToBlink(GameObject.Find("AssignAction").GetComponent<Text>());
				LeftPage = GameObject.Find("AssignLeftPage");
				RightPage = GameObject.Find("AssignRightPage");
				switchingUI = true;
				break;
            default:
                if(temp2 == false)
                {
                    currentUI++;
                    controlDisable = false;
                }
                else
                {
                    currentUI--;
                    controlDisable = false;
                }
                break;
        }
       
    }
}
