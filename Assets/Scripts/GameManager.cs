﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	}
	
	public IEnumerator DoDeath()
	{
		yield return new WaitForSeconds(deathScreenDelay);
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
		
	}
	
	public void Restart()
	{
		
	}
	
	public void GetCoins()
	{
		
	}
	
	public void MainMenu()
	{
		
	}
	
}
