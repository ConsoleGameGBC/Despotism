using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
	#region Game Object Singleton
	
	
	public static SoundManager Instance = null;
	public AudioClip[] Clips;
	public  AudioSource dukeBox =null;
	
	
	void Awake ()
	{
		if (Instance == null)
		{
			Instance = this;
			Object.DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Object.Destroy(this.gameObject);
		}
	}
	
	
	#endregion Game Object Singleton
	

	
	public void  PlaySound(int clip)
	{

			
			dukeBox.Stop();
			dukeBox.clip=Clips[clip];
			dukeBox.Play();


	}
}