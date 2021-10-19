using UnityEngine;
using System.Collections;

public class Changewep : MonoBehaviour {
    private int cd = 0;
    private float counter = 0;
    public int whatwep = 0;
    public int hammertime = 0;
    
    GameObject player;
    GameObject bspawn;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        bspawn = GameObject.FindGameObjectWithTag("yala");
        
	}


    
	// Update is called once per frame
	void Update () {
        if (cd == 1)
        {
            counter += Time.deltaTime;
            if (counter > 1.2)
            {
                cd = 0;
                counter = 0;
            }

        }

        if ((hammertime == 1)&& (cd == 0))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                player.GetComponent<Animator>().SetTrigger("slash");
                GameObject.FindGameObjectWithTag("kspawn").GetComponent<knifedajf>().knifeknife();
                cd = 1;
                Debug.Log("Slashdash");
            }
        }
        
        if (((Input.GetKey(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.Alpha2)) || (Input.GetKey(KeyCode.Alpha3))) && (whatwep == 1) && (cd==0))
        {

            player.GetComponent<Animator>().SetTrigger("tom4");
            whatwep = 0;
            cd = 1;
            Debug.Log("switched to m4");
            Debug.Log("You can shoot");
            bspawn.GetComponent<Shoot>().canshoot = 0;
            Debug.Log("You cant slash");
            hammertime = 0;
                
        }
        else if (((Input.GetKey(KeyCode.Alpha1)) || (Input.GetKey(KeyCode.Alpha2)) || (Input.GetKey(KeyCode.Alpha3))) && (whatwep == 0)&& (cd == 0))
        {
            player.GetComponent<Animator>().SetTrigger("toknife");
            whatwep = 1;
            cd = 1;
            Debug.Log("switched to knife");
            bspawn.GetComponent<Shoot>().canshoot = 1;
            Debug.Log("You cant shoot");
            hammertime = 1;
            Debug.Log("You can slash");
        }

        
       
	}
}
