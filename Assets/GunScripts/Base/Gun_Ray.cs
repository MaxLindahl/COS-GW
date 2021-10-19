using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun_Ray : Gun {
    public GameObject tracer = null;        // projectile prefab... whatever this gun shoots
    public float maxRange = 500.0f;         // max effective range this weapon can fire    

    protected override void FireOneShot()
    {
         if (roundsLeft > 0)
        {
             int hitCount = 0;
             Vector3 pos = muzzlePoint.position; // position to spawn bullet is at the muzzle point of the gun 
             Quaternion rot = muzzlePoint.rotation; // spawn bullet with the muzzle's rotation
             Vector3 direction = muzzlePoint.TransformDirection(Random.Range(-maxSpread, maxSpread) * spread, Random.Range(-maxSpread, maxSpread) * spread, 1);
             RaycastHit[] hits = Physics.RaycastAll(pos, direction, maxRange);

             if (hits.Length > 0)
             {
                 for (int i = 0; i < hits.Length; i++)
                 {
                     if (hits[i].collider)
                     {
                         Health hitObjectsHP = hits[i].collider.GetComponent<Health>();

                         if (hitObjectsHP)
                         {
                             hitObjectsHP.Hit(bulletInfo);
                         }
                         else
                         {
                             BulletHoleManager.bulletHole.SpawnHole(hits[i]);
                         }
                        hitCount++; // add a hit    
                     }
                                 
                 }

                 Vector3 dir = (hits[0].point - hits[hits.Length-1].point).normalized;
                 float dist = (hits[hits.Length-1].point - hits[0].point).magnitude;

                 RaycastHit[] backHits = Physics.RaycastAll(hits[hits.Length-1].point, dir, dist);

                 for (int i = 0; i < backHits.Length; i++)
                 {
                    Health hitObjectsHP = backHits[i].collider.GetComponent<Health>();

                    if(hitObjectsHP){
                        //do nothing on back hit except add bulletholes... but not to things with health
                    }
                    else{
                        BulletHoleManager.bulletHole.SpawnHole(backHits[i]);
                    }               
                 }
            }

            if (tracer)
            {
                GameObject newBullet;

                if (usePooling)
                {
                    newBullet = ObjectPool.pool.GetObjectForType(tracer.name, true);
                    newBullet.transform.position = pos;
                    newBullet.transform.rotation = rot;
                }
                else
                {
                    newBullet = Instantiate(tracer, pos, rot) as GameObject; // create a bullet
                }               
            }

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
