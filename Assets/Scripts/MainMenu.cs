using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MainMenu : MonoBehaviour
{
    
	public string levelToLoad;
	public GameObject mainScreen;
	public GameObject switchGameScreen;
	
	public Transform theCamera;
	public Transform charSwitchHolder;
	
	private Vector3 camTargetPos;
	public float cameraSpeed;
	
	public GameObject[] theChars;
	public int curChar;
	
	public GameObject switchPlayButton;
	public GameObject switchUnlockButton;
	public GameObject switchGetCoinsButton;
	public GameObject switchLockImg;
	public Text coinsText;
	
	public int curCoins;
	//public string gameId = "3164006";
	//public string myPlacementId = "rewardedVideo";
	//public bool testMode = true;
	
	// Start is called before the first frame update
    void Start()
    {
	    //Advertisement.AddListener(this);
	    //Advertisement.Initialize(gameId, testMode);
	    
	    if(PlayerPrefs.HasKey("CoinsCollected"))
	    {
		    curCoins = PlayerPrefs.GetInt("CoinsCollected");
	    }
	    else
	    {
	    	PlayerPrefs.SetInt("CoinsCollected", 0);
	    }
	    
	    camTargetPos = theCamera.position;
	    
	    if(!PlayerPrefs.HasKey(theChars[0].name))
	    {
	    	PlayerPrefs.SetInt(theChars[0].name, 1);
	    }
	    PlayerPrefs.SetInt(theChars[0].name, 1);
	    coinsText.text = "Coins: " + curCoins;
    }

    // Update is called once per frame
    void Update()
    {
	    theCamera.position = Vector3.Lerp(theCamera.position, camTargetPos, cameraSpeed * Time.deltaTime);
	    //theCamera.position = Vector3.MoveTowards(theCamera.position, camTargetPos, cameraSpeed * Time.deltaTime);
	    //coinsText.text = "Coins: " + curCoins;
	    #if UNITY_EDITOR
	    if(Input.GetKeyDown(KeyCode.L))
	    {
	    	foreach(GameObject chr in theChars)
	    	{
	    		PlayerPrefs.SetInt(chr.name, 0);
	    	}
	    	PlayerPrefs.SetInt(theChars[0].name, 1);
	    	UnlockedCheck();
	    }
	    #endif
    }
    
	public void PlayGame()
	{
		Advertisement.Show();
		SceneManager.LoadScene(levelToLoad);
		//SceneManager.UnloadScene(this);
	}
	
	public void ChooseChar()
	{
		mainScreen.SetActive(false);
		switchGameScreen.SetActive(true);
		
		camTargetPos = theCamera.position + new Vector3(0f, charSwitchHolder.position.y, 0f);
		
		//theCamera.position += new Vector3(0f, charSwitchHolder.position.y, 0f);
		UnlockedCheck();
	}
	
	public void MoveLeft()
	{
		if(curChar > 0)
		{
			camTargetPos += new Vector3(2f, 0f, 0f);
			curChar --;
			UnlockedCheck();
		}
		
	}
	
	public void MoveRight()
	{
		if(curChar < theChars.Length - 1)
		{
			camTargetPos -= new Vector3(2f, 0f, 0f);
			curChar ++;
			UnlockedCheck();
		}
	}
	
	public void UnlockedCheck()
	{
		if(PlayerPrefs.HasKey(theChars[curChar].name))
		{
			if(PlayerPrefs.GetInt(theChars[curChar].name) == 0)
			{
				switchLockImg.SetActive(true);
				switchPlayButton.SetActive(false);
				if(curCoins < 500)
				{
					switchGetCoinsButton.SetActive(true);
					switchUnlockButton.SetActive(false);
				}
				else
				{
					switchUnlockButton.SetActive(true);
					switchGetCoinsButton.SetActive(false);
				}
			}
			else
			{
				switchPlayButton.SetActive(true);
				switchGetCoinsButton.SetActive(false);
				switchUnlockButton.SetActive(false);
				switchLockImg.SetActive(false);
			}
		}
		else
		{
			PlayerPrefs.SetInt(theChars[curChar].name, 0);
			UnlockedCheck();
		}
	}
	
	public void UnlockCharacter()
	{
		curCoins -= 500;
		PlayerPrefs.SetInt(theChars[curChar].name, 1);
		PlayerPrefs.SetInt("CoinsCollected)", curCoins);
		coinsText.text = "Coins: " + curCoins;
		UnlockedCheck();
	}
	
	public void SelectChar()
	{
		PlayerPrefs.SetString("SelectedChar", theChars[curChar].name);
		PlayGame();
	}
	
	public void UpdateCoins()
	{
		coinsText.text = "Coins: " + curCoins;
	}

	
}
