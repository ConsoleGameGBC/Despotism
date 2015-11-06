using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{


    public AudioClip Sound;

    public AudioSource Source;

    public void PlaySound()
    {
        Source.clip = Sound;
        Source.Play();





    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
