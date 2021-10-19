using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
///  Base Class for projectiles, contains common elements to all type of projectiles for other projectile  classes to be derived from.
/// </summary>

public class Projectile : MonoBehaviour {

    protected ProjectileInfo myInfo = new ProjectileInfo();
    protected Vector3 velocity;
    protected int hitCount = 0;
    protected List<Collider> collidersToIgnore = new List<Collider>();
    protected List<Collider> backCollidersToIgnore = new List<Collider>();
		
	  
    // This is bullet initialization, It gets called by the weapon that fired this projectile
    public virtual void SetUp(ProjectileInfo info)
    {
        myInfo = info;       
        hitCount = 0;
        velocity = myInfo.speed * transform.forward + transform.TransformDirection(Random.Range(-myInfo.maxSpread, myInfo.maxSpread) * myInfo.spread, Random.Range(-myInfo.maxSpread, myInfo.maxSpread) * myInfo.spread, 1);
		collidersToIgnore.Add (myInfo.owner.GetComponent<Collider>());
		backCollidersToIgnore.Add (myInfo.owner.GetComponent<Collider>());
        Invoke("Recycle", myInfo.projectileLifeTime); // set a life time for this projectile
    }

    protected virtual void MakeAHole(RaycastHit hit)
    {       
        foreach (Collider c in collidersToIgnore)
        {
            if (hit.collider == c)
            {
                return;
            }
        }

        BulletHoleManager.bulletHole.SpawnHole(hit);
        collidersToIgnore.Add(hit.collider);
    }

    protected virtual void MakeABackHole(RaycastHit hit)
    {
        foreach (Collider c in backCollidersToIgnore)
        {
            if (hit.collider == c)
            {
                return;
            }
        }

        BulletHoleManager.bulletHole.SpawnHole(hit);
        backCollidersToIgnore.Add(hit.collider);
    }
   
    protected virtual void Recycle()
    {
		//Clear the colliders this bullet ignores
		collidersToIgnore.Clear ();
		backCollidersToIgnore.Clear ();

        // pool or destroy the bullet when it is no longer used.
        if (myInfo.usePool)
        {
            ObjectPool.pool.PoolObject(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}



[System.Serializable]
public class ProjectileInfo
{
    public GameObject owner;
    public string name;
    public Damage damage = new Damage();
    public float force;
    public int maxPenetration;
    public float maxSpread;
    public float spread;
    public float speed;
    public bool usePool;
    public float projectileLifeTime;
}
