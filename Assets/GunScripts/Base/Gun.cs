using UnityEngine;
using System.Collections;



[System.Serializable]
public class Damage
{
    public int amount; // how much damage
    public DamageType type; //what type of damage
}

public enum DamageType
{
    NORMAL,
    FIRE,
    ICE,
    ACID,
    ELECTRIC,
    POISON
}

public enum WeaponType
{
    SEMIAUTO, // burst fire and shotguns fall under this category
    FULLAUTO
}


public class Gun : MonoBehaviour {
    public string weaponName;                         // Name of this weapon
    public WeaponType typeOfWeapon;             // type of weapon, used to determine how the trigger acts
    public bool usePooling = false;             // do we want to use object pooling or instantiation
    public Damage damage = new Damage();        // the damage and type of damage this gun does
    public float projectileSpeed = 100.0f;      // speed that projectile flies at
    public float projectileForce = 10.0f;       // force applied to any rigidbodies the projectile hits
    public float projectileLifeTime = 5.0f;     // how long before the projectile is considered gone and recycleable

    public Transform muzzlePoint = null;        // the muzzle point for this gun, where you want bullets to be spawned

    public int maxPenetration = 1;              // maximum amount of hits detected before the bullet is destroyed
    public float fireRate = 0.5f;               // time betwen shots

    public bool infinteAmmo = false;            // gun can have infinite ammo if thats what you wish
    public int roundsPerClip = 50;              // number of bullets in each clip
    public int ammoReserve = 200;               // number of clips you start with    
    public int roundsLeft;                      // bullets in the gun-- current clip

    public float reloadTime = 2.5f;             // how long it takes to reload in seconds
    protected bool isReloading = false;         // are we currently reloading

    public float baseSpread = 0.2f;             // how accurate the weapon starts out... smaller the number the more accurate
    public float maxSpread = 4.0f;              // maximum inaccuracy for the weapon
    public float spreadPerShot = 0.75f;         // increase the inaccuracy of bullets for every shot
    public float spread = 0.0f;                 // current spread of the gun
    public float decreaseSpreadPerTick = 0.25f;  // amount of accuracy regained per frame when the gun isn't being fired 
    public float spreadDecreaseTicker = 0.5f;   // time in seconds to decrease inaccuracy

    protected float nextFireTime = 0.0f;        // able to fire again on this frame
    protected bool spreadDecreasing = false;    // is the gun currently decrasing the spread

    protected ProjectileInfo bulletInfo = new ProjectileInfo(); // all info about gun that's sent to each projectile


    protected virtual void Start()
    {
        roundsLeft = roundsPerClip; // load gun on startup       
    }

    // all guns handle firing a bit different so give it a blank function that each gun can override
    public virtual void Fire()
    {

    }

	
    // everything fires a single round the same
	protected virtual void FireOneShot() {
       
	}

    // reload your weapon
    protected virtual IEnumerator Reload()
    {
        if (isReloading)
        {
            yield break; // if already reloading... exit and wait till reload is finished
        }

        if (ammoReserve > 0)
        {
            isReloading = true; // we are now reloading
            int roundsNeeded = roundsPerClip - roundsLeft; // how many rounds needed to fill the gun
            yield return new WaitForSeconds(reloadTime); // wait for set reload time

            if (ammoReserve < roundsNeeded)
            {
                roundsNeeded = ammoReserve;// if we have less bullets than needed to fill the gun... put all we have in
            }

            roundsLeft += roundsNeeded; // fill up the gun
        }

        isReloading = false; // done reloading
    }


    void DecreaseSpread()
    {
        // decrease the current spread per tick
        spread -= decreaseSpreadPerTick;

        // if the current spread is less then the base spread value, set it to the base
        if (spread <= baseSpread)
        {
            spread = baseSpread;

            // stop the decrease spread function until we need it again
            spreadDecreasing = false;
            CancelInvoke("DecreaseSpread");
        }
    }

    // set all bullet info from the gun's info
    // if you plan to be able to change weapon stats on the fly
    // call this function in the fire function (worst performance but always checkes gun stats before firing)
    // or Always call this just after altering a weapon's stats (best performance since its called once when it's needed)
    // default right now is it is called once in start
    protected void SetupBulletInfo()
    {
        bulletInfo.owner = transform.root.gameObject;   // the Owner of this weapon (GameObject) <- use this for scoreboard and who killed who
        bulletInfo.name = name;                         // Name of this weapon  <- for keeping track of weapon kills / whose killed by what
        bulletInfo.damage.amount = damage.amount;       // amount of damage
        bulletInfo.damage.type = damage.type;           // type of damage
        bulletInfo.force = projectileForce;             // weapon force
        bulletInfo.maxPenetration = maxPenetration;     // max hits
        bulletInfo.maxSpread = maxSpread;               // max weapon spread
        bulletInfo.spread = spread;                     // current weapon spread value
        bulletInfo.speed = projectileSpeed;             // projectile speed
        bulletInfo.owner = transform.root.gameObject;   // this projectile's owner gameobject, useful if you want to know whose killing what for kills/assists or whatever
        bulletInfo.usePool = usePooling;                // do we use object pooling
        bulletInfo.projectileLifeTime = projectileLifeTime; // how long till this bullet just goes away
    }
	
}
