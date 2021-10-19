using UnityEngine;
using System.Collections;

public class RigidProjectile : Projectile
{    
    private Rigidbody myRigid;   

    public override void SetUp(ProjectileInfo info)
    {
        base.SetUp(info);
        myRigid = GetComponent<Rigidbody>();       
        myRigid.velocity = velocity; 
    }

    void FixedUpdate()
    {
       // Debug.DrawLine(transform.position, transform.position + myRigid.velocity / 60, Color.red);
       // Debug.DrawLine(transform.position, transform.position - myRigid.velocity / 30, Color.magenta);

        RaycastHit hit;  // forward hit
        RaycastHit hit2; // rear hit        
        
        if (Physics.Raycast(transform.position, myRigid.velocity, out hit, (myRigid.velocity.magnitude/Time.deltaTime), ~LayerMask.NameToLayer("Projectiles")))
        {
            // probably shouldn't do this but best way i can think of to avoid
            // multiple hits from same bullet
            //myRigid.MovePosition(hit.point); // move the bullet to the impact point
            transform.position = hit.point;
            
            if (hit.transform.CompareTag("Ground"))
            {// if we hit dirt... kill the bullet since most weapons don't pierce the earth
                CancelInvoke("Recycle");
                Recycle();
            }

            Health hitObject = hit.transform.GetComponent<Health>();

            if (hitObject)
            {                
                hitObject.Hit(myInfo); // send bullet info to hit object's health component
            }
			else
            {
                MakeAHole(hit); // make a hole anywhere except the players
            }
	
            hitCount++; // add a hit

            if (hitCount > myInfo.maxPenetration)
            {
                CancelInvoke("Recycle");
                Recycle(); // if hit count exceeds max hits.... kill the bullet
            }
        }

        // this shoots a ray out behind the bullet.
        // use this to add a bullet hole to the back side of a penetrated wall or whatever
		if (Physics.Raycast(transform.position, -myRigid.velocity, out hit2, 2+(myRigid.velocity.magnitude/Time.deltaTime) , ~LayerMask.NameToLayer("Projectiles")))
            {
                if (hit2.transform.CompareTag("Player"))
                {
                    // do nothing since we probably already penetrated the player
                }
                else
                {
                    MakeABackHole(hit2);
                }                
            }
    }   
}
