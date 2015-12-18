using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class blinkScript : MonoBehaviour {
    [SerializeField]
    Text textToBlink;
    [SerializeField] float blinkSpeed;

    Color myColor;
    float myAlpha = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (textToBlink)
        {
            myAlpha = Mathf.PingPong(Time.time * blinkSpeed, 0.7f);
            //myAlpha = 1;
            //Debug.Log("alpha of the blinking is " + myAlpha.ToString());
            myColor = new Color(0.2f, 0, 0, myAlpha+0.3f);
            textToBlink.color = myColor;
        }

	}

    public void stopBlinking()
    {
        if(textToBlink)
           textToBlink.color = new Color(0, 0, 0, 1);

        textToBlink = null;
    }

    public void assignTextToBlink(Text newText)
    {
        if(textToBlink)
        textToBlink.color = new Color(0, 0, 0, 1);

        textToBlink = newText;
    }
}
