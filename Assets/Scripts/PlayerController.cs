using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
	public GameManager gm;
	
	public Rigidbody rg;
	
	public float jumpForce;
	
	public Transform modelHolder;
	public LayerMask whatIsGround;
	public bool onGround;
	
	public Animator anim;
	
	private Vector3 startPosition;
	private Quaternion startRotation;
	
	public float invincibleTime;
	private float invincibleTimer;
	
	public AudioManager theAM;
	
	public GameObject coinFX;
	
	// Start is called before the first frame update
    void Start()
    {
	    startPosition = transform.position;
	    startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
	    //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (moveSpeed * Time.deltaTime));
	    if(gm.canMove)
	    {
		    onGround = Physics.OverlapSphere(modelHolder.position, 0.2f, whatIsGround).Length > 0;
		    
		    if(onGround)
		    {
			    if(Input.GetMouseButtonDown(0))
			    {
				    //make player jump
				    rg.velocity = new Vector3(0f, jumpForce, 0f);
				    theAM.jumpSfx.Play();
			    }		    	
		    }
	    }
	    
	    //control invincibility
	    if(invincibleTimer > 0)
	    {
	    	invincibleTimer -= Time.deltaTime;
	    }
	    
	    //control animations
	    anim.SetBool("walking", gm.canMove);
	    anim.SetBool("onGround", onGround);
    }
    
	public void OnTriggerEnter(Collider other)
	{
		if(invincibleTimer <= 0)
		{
			if(other.gameObject.tag.Equals("Hazards"))
			{
				//Debug.Log("Hit Hazard");
				gm.HitHazard();				
			
				rg.constraints = RigidbodyConstraints.None;
			
				rg.velocity = new Vector3(Random.Range(GameManager._worldSpeed / 2f, -GameManager._worldSpeed / 2f), 2.5f, (-GameManager._worldSpeed / 2f));
				
				theAM.hitSfx.Play();
			}			
		}
		
		if(other.gameObject.tag.Equals("Coin"))
		{
			gm.AddCoin();
			
			Instantiate(coinFX, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
			theAM.coinSfx.Stop();
			theAM.coinSfx.Play();
		}
	}
	
	public void ResetPlayer()
	{
		rg.constraints = RigidbodyConstraints.FreezeRotation;
		transform.rotation = startRotation;
		transform.position = startPosition;
		
		invincibleTimer = invincibleTime;
	}
	
	
}
