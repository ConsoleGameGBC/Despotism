﻿using UnityEngine;
using System.Collections;

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
    public float speed = 10f;
    public float step;
    bool fold = true;
    bool startFolding;
    bool switchingUI;
    bool controlDisable;
	// Use this for initialization

    public enum UIChoice
    {
        MainMenu,
        TurnReport
    } ;
    UIChoice lastUI = new UIChoice();
    public UIChoice currentUI = new UIChoice();
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
        TurnReport = GameObject.Find("TurnReport");
        ReportPosition = TurnReport.transform.position;
        ReportRotation = TurnReport.transform.rotation;
        FocusLocationPos = GameObject.Find("FocusLocation").transform.position;
        FocusLocationRot = GameObject.Find("FocusLocation").transform.rotation;
        CurrentUI = MainMenu;
        LeftPage = GameObject.Find("MainMenuLeftPage");
        RightPage = GameObject.Find("MainMenuRightPage");
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
