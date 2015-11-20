using UnityEngine;
using System.Collections;

public class City : MonoBehaviour {
    UI UI;
    void Start()
    {
        UI = GameObject.Find("PaperWork").GetComponent<UI>();
    }
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (UI.MulitaryStatus == 1 && other.gameObject.tag == "Unit")
        {
            UI.TerrainType = 3;
        }
    }
}
