using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour {

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

    GameObject MulitaryUI;
    Vector3 MulitaryPos;
    Quaternion MulitaryRot;

    GameObject StockUI;
    Vector3 StockPos;
    Quaternion StockRot;

    public float speed = 20f;
    public float step;
    bool fold = true;
    bool startFolding;
    bool switchingUI;
    bool controlDisable;

	int RandomEventOption = 0;
	// Use this for initialization

    enum UIChoice
    {
        MainMenu,
        TurnReport,
        Mulitary,
        Stock
    } ;

	public enum MulitaryAction
	{
		Attack,
		Explore
	};

    UIChoice lastUI = new UIChoice();
    UIChoice currentUI = new UIChoice();
	MulitaryAction mulitaryAction = new MulitaryAction ();
	int MulitaryStatus = 0;
	bool MulitaryActionAssigned = false;

	void Start () {
        Cursor.visible = false;
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

        TurnReport = GameObject.Find("TurnReport");
        ReportPosition = TurnReport.transform.position;
        ReportRotation = TurnReport.transform.rotation;
        FocusLocationPos = GameObject.Find("FocusLocation").transform.position;
        FocusLocationRot = GameObject.Find("FocusLocation").transform.rotation;
        CurrentUI = MainMenu;
        LeftPage = GameObject.Find("MainMenuLeftPage");
        RightPage = GameObject.Find("MainMenuRightPage");

		MulitaryChoice (0);
	}

	void MulitaryChoice(int value)
	{
		Debug.Log (MulitaryStatus);
		switch(MulitaryStatus)
		{
		case (0):
			switch (mulitaryAction += value) 
			{
				case(MulitaryAction.Attack):
					GameObject.Find("MilitaryAction").GetComponent<Text>().text = "Attack";
					break;
				case(MulitaryAction.Explore):
					GameObject.Find("MilitaryAction").GetComponent<Text>().text = "Explore";
					break;
				default:
					mulitaryAction -= value;
					break;
			}
			break;
		case(1): //Enable Map
			break;
		case(2):
			MulitaryStatus = 0;
			MulitaryActionAssigned = true;
			break;
		}
	}

	void turnEnd()
	{
		MulitaryActionAssigned = false;
		currentUI = UIChoice.TurnReport;
		LeftPage = GameObject.Find("TurnReportLeftPage");
		RightPage = GameObject.Find("TurnReportRightPage");
	}

	void MulitaryChoice(bool temp)
	{
		Debug.Log ("MulitaryChoice AnimationTriggers");
		if(temp == true)
		{
			MulitaryStatus++;
		}
		else
		{
			MulitaryStatus--;
		}
	}

	void TurnChoice(int value)
	{
		switch(RandomEventOption += value)
		{
		case (0):
			break;
		case (1):
			break;
		case (2):
			break;
		default:
			RandomEventOption -= value;
			break;
		}
	}


	// Update is called once per frame
	void Update () {
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
				turnEnd ();
			}

			if(Input.GetButtonDown("B"))
			{
				turnEnd ();
			}

			if(Input.GetButtonDown("X"))
			{
				if (fold == false)
				{
					switch(currentUI)
					{
						case (UIChoice.Mulitary):
							if(MulitaryActionAssigned == false)
								MulitaryChoice(true);
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
						if(MulitaryActionAssigned == false)
							MulitaryChoice(false);
						break;
					}
				}
			}


            if (Input.GetButtonDown("Up"))
            {
                if (fold == true)
                {
                    controlDisable = true;
                    fold = false;
                    startFolding = true;

                }
	
            }
            else if (Input.GetButtonDown("Down"))
            {
                if (fold == false)
                {
                    controlDisable = true;
                    fold = true;
                    startFolding = true;

                }
            }
            else if (Input.GetButtonDown("Left"))
            {
                if (fold == false)
                {
					switch(currentUI)
					{
					case (UIChoice.Mulitary):
						MulitaryChoice(1);
						break;
					case (UIChoice.TurnReport):
						TurnChoice(1);
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
            else if (Input.GetButtonDown("Right"))
            {
                if (fold == false)
                {
					switch(currentUI)
					{
					case (UIChoice.Mulitary):
						MulitaryChoice(-1);
						break;
					case (UIChoice.TurnReport):
						TurnChoice(-1);
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

            }
            if(temp == true)
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

                }
            }
        }
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
