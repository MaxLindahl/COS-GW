using UnityEngine;
using System.Collections;

public class Ray_SingleShot : Gun_Ray {

    public override void Fire()
    {
        if (nextFireTime < Time.time)
        {
            FireOneShot();

            nextFireTime = Time.time + fireRate;
        }
    }   
}
