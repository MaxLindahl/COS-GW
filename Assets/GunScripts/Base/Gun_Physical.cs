using UnityEngine;
using System.Collections;

public class Gun_Physical : Gun {
    public GameObject projectile = null;        // projectile prefab... whatever this gun shoots
    

    protected override void Start()
    {
        base.Start();
        SetupBulletInfo(); // set a majority of the projectile info
    }
	
	protected override void FireOneShot () {
        if (roundsLeft > 0)
        {
            Vector3 pos = muzzlePoint.position; // position to spawn bullet is at the muzzle point of the gun       
            Quaternion rot = muzzlePoint.rotation; // spawn bullet with the muzzle's rotation

            bulletInfo.spread = spread; // set this bullet's info to the gun's current spread
            GameObject newBullet;

            if (usePooling)
            {
                newBullet = ObjectPool.pool.GetObjectForType(projectile.name, false);
                newBullet.transform.position = pos;
                newBullet.transform.rotation = rot;
            }
            else
            {
                newBullet = Instantiate(projectile, pos, rot) as GameObject; // create a bullet
				newBullet.name = projectile.name;
            }

            newBullet.GetComponent<Projectile>().SetUp(bulletInfo); // send bullet info to spawned projectile

            spread += spreadPerShot;  // we fired so increase spread

            // if the current spread is greater then the weapons max spread, set it to max
            if (spread >= maxSpread)
            {
                spread = maxSpread;
            }

            // if the spread is not currently decreasing, start it up cause we just fired
            if (!spreadDecreasing)
            {
                InvokeRepeating("DecreaseSpread", spreadDecreaseTicker, spreadDecreaseTicker);
                spreadDecreasing = true;
            }

            // if this gun doesn't have infinite ammo, subtract a round from our clip
            if (!infinteAmmo)
            {
                roundsLeft--;

                // if our clip is empty, start to reload
                if (roundsLeft <= 0)
                {
                    StartCoroutine(Reload());
                }
            }
        }
	}
    
}
