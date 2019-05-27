using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
	public AudioSource menuMusic;
	public AudioSource gameMusic;
	public AudioSource gameOverMusic;
	public AudioSource coinSfx;
	public AudioSource jumpSfx;
	public AudioSource hitSfx;
	
	public bool soundMuted;
	
	public GameObject musicImage;
	
	// Start is called before the first frame update
    void Start()
    {
	    if(PlayerPrefs.HasKey("SoundMuted"))
		{
	    	if(PlayerPrefs.GetInt("SoundMuted") == 1)
	    	{
	    		MuteAll();
	    		soundMuted = true;	
	    		musicImage.SetActive(true);
	    	}
	    	else
	    	{
	    		UnmuteAll();
	    		soundMuted = false;
	    		musicImage.SetActive(false);
	    	}
		}
	    else
	    {
	    	PlayerPrefs.SetInt("SoundMuted", 0);
	    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	public void SoundOnOff()
	{
		if(soundMuted)
		{
			musicImage.SetActive(false);
			soundMuted = false;
			UnmuteAll();
		}
		else
		{
			musicImage.SetActive(true);
			soundMuted = true;
			MuteAll();
		}
	}

	public void MuteAll()
	{
		menuMusic.gameObject.SetActive(false);
		gameMusic.gameObject.SetActive(false);
		gameOverMusic.gameObject.SetActive(false);
		coinSfx.gameObject.SetActive(false);
		jumpSfx.gameObject.SetActive(false);
		hitSfx.gameObject.SetActive(false);
		PlayerPrefs.SetInt("SoundMuted", 1);
	}

	public void UnmuteAll()
	{
		menuMusic.gameObject.SetActive(true);
		gameMusic.gameObject.SetActive(true);
		gameOverMusic.gameObject.SetActive(true);
		coinSfx.gameObject.SetActive(true);
		jumpSfx.gameObject.SetActive(true);
		hitSfx.gameObject.SetActive(true);
		PlayerPrefs.SetInt("SoundMuted", 0);
	}
	
	public void StopMusic()
	{
		menuMusic.Stop();
		gameMusic.Stop();
		gameOverMusic.Stop();
	}
	
}
