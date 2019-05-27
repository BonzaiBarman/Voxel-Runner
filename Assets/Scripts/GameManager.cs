using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
	public bool canMove;
	static public bool _canMove;
	
	public float worldSpeed;
	static public float _worldSpeed;
	
	public int coinsCollected;
	
	private bool gameStarted;
	
	//speeding up
	public float timeToIncreaseSpeed;
	private float increaseSpeedCounter;
	public float speedMultiplier;
	private float targetSpeedMultiplier;
	public float acceleration;
	public float speedIncreaseAmount;
	private float worldSpeedStore;
	private float accelerationStore;
	
	public GameObject tapMessage;
	public Text CoinsText;
	
	public Text distanceText;
	private float distanceCovered;
	
	public GameObject deathScreen;
	public Text deathScreenCoins;
	public Text deathScreenDistance;
	public float deathScreenDelay;
	public GameObject notEnoughCoinsScreen;
	
	public string mainMenuName;
	
	public PlayerController player;
	
	public GameObject pauseScreen;
	
	public GameObject[] models;
	public GameObject defaultChar;
	
	public AudioManager theAM;
	
	//private bool coinHitThisFrame;
	
	// Start is called before the first frame update
    void Start()
    {
	    if(PlayerPrefs.HasKey("CoinsCollected"))
	    {
		    coinsCollected = PlayerPrefs.GetInt("CoinsCollected");	    	
	    }
	    
	    increaseSpeedCounter = timeToIncreaseSpeed;
	    targetSpeedMultiplier = speedMultiplier;
	    worldSpeedStore = worldSpeed;
	    accelerationStore = acceleration;
	    CoinsText.text = "Coins: " + coinsCollected;
	    distanceText.text = distanceCovered + "m";
	    
	    //load correct model
	    foreach(GameObject mdl in models)
	    {
	    	if(mdl.name == PlayerPrefs.GetString("SelectedChar"))
	    	{
	    		GameObject clone = Instantiate(mdl, player.modelHolder.position, player.modelHolder.rotation);
	    		clone.transform.parent = player.modelHolder;
	    		defaultChar.SetActive(false);
	    	}	
	    }
	    
    }

    // Update is called once per frame
    void Update()
    {
	    _canMove = canMove;
	    _worldSpeed = worldSpeed;
	    
	    if(!gameStarted && Input.GetMouseButtonDown(0))
	    {
	    	canMove = true;
	    	_canMove = true;
	    	gameStarted = true;
	    	
	    	tapMessage.SetActive(false);
	    }
	    
	    //increase speed over time
	    if(canMove)
	    {
	    	increaseSpeedCounter -= Time.deltaTime;
	    	if(increaseSpeedCounter <= 0)
	    	{
	    		increaseSpeedCounter = timeToIncreaseSpeed;
	    		//worldSpeed = worldSpeed * speedMultiplier;
	    		targetSpeedMultiplier = targetSpeedMultiplier * speedIncreaseAmount;
	    		
	    		timeToIncreaseSpeed = timeToIncreaseSpeed / speedMultiplier;
	    	}
	    	acceleration = accelerationStore * speedMultiplier;
	    	speedMultiplier = Mathf.MoveTowards(speedMultiplier, targetSpeedMultiplier, acceleration * Time.deltaTime);
	    	worldSpeed = worldSpeedStore * speedMultiplier;
	    	
	    	//updating distance
	    	distanceCovered += Time.deltaTime * worldSpeed;
	    	distanceText.text = Mathf.Floor(distanceCovered) + "m";
	    }
	    
	    //coinHitThisFrame = false;
    }
    
	public void HitHazard()
	{
		canMove = false;
		_canMove = false;
		
		PlayerPrefs.SetInt("CoinsCollected", coinsCollected);
		
		
		StartCoroutine("DoDeath");
		coinsCollected += 100;

	}
	
	public IEnumerator DoDeath()
	{
		theAM.StopMusic();
		yield return new WaitForSeconds(deathScreenDelay);
		theAM.gameOverMusic.Play();
		deathScreen.SetActive(true);
		deathScreenCoins.text = coinsCollected + " coins!";
		deathScreenDistance.text = Mathf.Floor(distanceCovered) + "m!";		
	}
	
	public void AddCoin()
	{
		//if(!coinHitThisFrame)
		//{
			coinsCollected++;
		//coinHitThisFrame = true;			
		//}
		CoinsText.text = "Coins: " + coinsCollected;
	}
	
	public void ContinueGame()
	{
		if(coinsCollected >= 100)
		{
			coinsCollected -= 100;
			canMove = true;
			_canMove = true;
			deathScreen.SetActive(false);
			player.ResetPlayer();
			
			theAM.StopMusic();
			theAM.gameMusic.Play();
		}
		else
		{
			notEnoughCoinsScreen.SetActive(true);
		}
	}
	
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	public void GetCoins()
	{
		
	}
	
	public void MainMenu()
	{
		SceneManager.LoadScene(mainMenuName);
		Time.timeScale = 1f;
	}
	
	public void CloseNotEnoughCoins()
	{
		notEnoughCoinsScreen.SetActive(false);
	}
	
	public void ResumeGame()
	{
		pauseScreen.SetActive(false);
		//canMove = true;
		//_canMove = true;
		Time.timeScale = 1f;
	}
	
	public void PauseGame()
	{
		
		if(Time.timeScale == 1)
		{
			pauseScreen.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			pauseScreen.SetActive(false);			
			Time.timeScale = 1f;
		}

		//canMove = false;
		//_canMove = false;

	}
	
}
