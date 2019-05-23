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
	
	// Start is called before the first frame update
    void Start()
    {

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
			    }		    	
		    }
	    }
	    //control animations
	    anim.SetBool("walking", gm.canMove);
	    anim.SetBool("onGround", onGround);
    }
    
	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag.Equals("Hazards"))
		{
			//Debug.Log("Hit Hazard");
			gm.HitHazard();
			
			rg.constraints = RigidbodyConstraints.None;
			
			rg.velocity = new Vector3(Random.Range(GameManager._worldSpeed / 2f, -GameManager._worldSpeed / 2f), 2.5f, (-GameManager._worldSpeed / 2f));
		}
		
		if(other.gameObject.tag.Equals("Coin"))
		{
			gm.AddCoin();
			Destroy(other.gameObject);
		}
	}
}
