using UnityEngine;
using System.Collections;


public enum UIType
{
	MAINMENU,
	TURNREPORT
}
public class CameraTargeting : MonoBehaviour
{
	private UIType currentType = UIType.TURNREPORT;
	private float startTime;
	private float journeyLength;
	private bool zoomIn = false;
	private bool unfoldAlready = false;
	// Which layers targeting ray must hit (-1 = everything)
	public LayerMask targetingLayerMask = -1;
	[SerializeField] GameObject turnReport;
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject randEventOptionContent;
	private Animator animatorZoom;
	private Animator animatorFold;
	private HighlightableObject[] highlightObj;
	private UILabel randEventOption;
	// Targeting ray length
	
	// Camera component reference
	private Camera cam;

	void Start()
	{
		animatorZoom = GetComponent<Animator>();
		animatorFold = turnReport.GetComponentInChildren<Animator>();
		randEventOption = randEventOptionContent.GetComponent<UILabel> ();
	}

	void Awake()
	{

	}
	
	void Update()
	{
		KeyDetect();
		HighLightDetect();
	}

	public void HighLightDetect()
	{
		switch(currentType)
		{
			case UIType.MAINMENU:
				break;
			case UIType.TURNREPORT:
				highlightObj = turnReport.GetComponentsInChildren<HighlightableObject>();
				for(int i = 0; i< highlightObj.Length; i++)
				{
					highlightObj[i].On();
				}
				break;
		}
	}
	public void KeyDetect ()
	{
		if (Input.GetButtonDown("Up") && zoomIn != true)
		{
			animatorZoom.SetBool ("zoomIn MainMenu" ,zoomIn = true);
			switch(currentType)
			{
				case UIType.TURNREPORT:
					animatorFold.SetBool ("pageUnfold" ,unfoldAlready = true);
					break;
				case UIType.MAINMENU:
					break;
			}
		}
		else if(Input.GetButtonDown("Down") && zoomIn == true)
		{
			animatorZoom.SetBool ("zoomIn MainMenu" ,zoomIn = false);
			switch(currentType)
			{
				case UIType.TURNREPORT:
					animatorFold.SetBool ("pageUnfold" ,unfoldAlready = false);
					break;
				case UIType.MAINMENU:
					break;
			}
		}
		else if (Input.GetButtonDown("Left"))
		{
			if(zoomIn != true)
			{
				switch(currentType)
				{
				case UIType.TURNREPORT:
					break;
				case UIType.MAINMENU:
					break;
				}
			}
			else if(currentType == UIType.TURNREPORT)
			{
				randEventOptionContent.GetComponent<TypewriterEffect> ().Finish();
				randEventOption.text = "TESTING TESTING TESTING";
				randEventOptionContent.GetComponent<TypewriterEffect> ().ResetToBeginning();
			}

		}
		else if (Input.GetButtonDown("Right"))
		{
			if(zoomIn != true)
			{
				switch(currentType)
				{
				case UIType.TURNREPORT:
					break;
				case UIType.MAINMENU:
					break;
				}
			}
			else if(currentType == UIType.TURNREPORT)
			{
				randEventOptionContent.GetComponent<TypewriterEffect> ().Finish();
				randEventOption.text = "blablablablablabla";
				randEventOptionContent.GetComponent<TypewriterEffect> ().ResetToBeginning();
			}
		}


	}
	
	void OnGUI()
	{

	}
}
