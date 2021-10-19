using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    public Gun myGun;

	// Use this for initialization
	void Start () {
        myGun = GetComponentInChildren<Gun>(); // grab a gun script from player on start
	}
	
	// Update is called once per frame
	void Update () {
        if (myGun)
        {
            if (myGun.typeOfWeapon == WeaponType.FULLAUTO)
            {
                if (Input.GetButton("Fire1"))
                {
                    myGun.Fire();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    myGun.Fire();
                }
            }
        }
	}
}
