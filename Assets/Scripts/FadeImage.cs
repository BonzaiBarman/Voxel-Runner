using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    
	public Image fadeImg;
	public float waitToFade;
	public float fadeSpeed;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if(waitToFade > 0)
	    {
	    	waitToFade -= Time.deltaTime;
	    }
	    else
	    {
	    	fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.MoveTowards(fadeImg.color.a, 0f, fadeSpeed * Time.deltaTime));
		    if(fadeImg.color.a == 0f)
		    {
		    	gameObject.SetActive(false);
		    }	    	
	    }
    }
}
