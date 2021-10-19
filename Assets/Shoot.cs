using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject bullethole;
	public GameObject bullet;
    public GameObject Grenade;
    GameObject player;
	public float delaytime = 0.15f;
	public float delaytime2 = 0.1f;
    public int DamagePerShot = 15;
    public static int Cammo = 30;
    GameObject ammotext;
    GameObject gammotext;
    public float counter69 = 0;
    private int yes = 0;
    public static int GrenadeAmmo = 2;

    public int canshoot = 0;


    private float counter3 = 0f;
	private float counter2 = 0f;
	private float  counter = 0f; 
	private int setting = 0;
    private int setting2 = 0;
    private int cd = 0;
    private int cd2 = 0;
    private float count = 0f;
	private int yolo = 0;
	private int lightyesno = 0;
    private int wot = 0;
	Light lite;
    Ray ray;
	// Use this for initialization
	void Start () {
		lite = GetComponent<Light> ();
        player = GameObject.FindGameObjectWithTag("Player");
        ammotext = GameObject.FindGameObjectWithTag("Ammo");
        gammotext = GameObject.FindGameObjectWithTag("gammo");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gammotext.GetComponent<Text>().text = GrenadeAmmo + "/2";
        ammotext.GetComponent<Text>().text = Cammo + "/30";
        if ((Cammo < 30) && (Input.GetKey(KeyCode.R)) && (cd2 == 0) && (setting2 == 0) && (cd == 0) && (canshoot == 0))
        {
            player.GetComponent<Animator>().SetTrigger("reload");
            cd2 = 1;
        }
        if ((Input.GetKey(KeyCode.G)) && ( yes == 0) && (GrenadeAmmo >0))
        {
            Instantiate(Grenade, transform.position, transform.rotation);
            yes = 1;
            counter69 = 0;
            GrenadeAmmo = GrenadeAmmo - 1;
        }
        counter69 += Time.deltaTime;
        if (counter69 > 2f)
        {
            yes = 0;
        }
        if (Cammo > 0)
        {
            if ((cd2 == 0)&&(canshoot == 0))
            {
                if (setting == 0)
                {
                    delaytime = 0.15f;


                    if (Input.GetKey(KeyCode.Mouse0) && counter > delaytime)
                    {
                        pangpang();
                    }


                }

                if (setting == 1)
                {
                    if (Input.GetKey(KeyCode.Mouse0) && counter > (delaytime * 4))
                    {
                        StartCoroutine(Burst());
                        lightyesno = 1;
                        lite.enabled = true;
                    }
                }
                counter += Time.deltaTime;

                if (lightyesno == 1)
                {
                    counter2 += Time.deltaTime;
                    if (counter2 > delaytime2)
                    {
                        lightyesno = 0;
                        lite.enabled = false;
                        counter2 = 0;
                    }
                }
            }
        }
        else
        {
            if (wot == 0)
            {
                player.GetComponent<Animator>().SetTrigger("reload");
                wot = 1;
            }
            cd2 = 1;
        }
            if ((Input.GetKey(KeyCode.F)) && (setting == 0) && (yolo == 0))
            {
                setting = 1;
                yolo = 1;
            }
            else if ((Input.GetKey(KeyCode.F)) && (setting == 1) && (yolo == 0))
            {
                setting = 0;
                yolo = 1;
            }
            if (Input.GetKey(KeyCode.I))
            {
                player.GetComponent<Animator>().SetTrigger("aimin");
            }
            if (Input.GetKey(KeyCode.O))
            {
                player.GetComponent<Animator>().SetTrigger("aimout");
            }


            if ((Input.GetMouseButtonDown(1)) && (setting2 == 0) && (cd == 0) && (canshoot == 0))
            {
                setting2 = 1;
                cd = 1;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GUIcrosshair>().enabled = false;
                player.GetComponent<Animator>().SetTrigger("aimin");
            }
            else if ((Input.GetMouseButtonDown(1)) && (setting2 == 1) && (cd == 0) && (canshoot == 0))
            {
                setting2 = 0;
                cd = 1;
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GUIcrosshair>().enabled = true;
                player.GetComponent<Animator>().SetTrigger("aimout");
            }



            if (cd == 1)
            {
                count += Time.deltaTime;

                if (count > 0.5f)
                {
                    cd = 0;
                    count = 0;
                }
            }
            if (cd2 == 1)
            {
                counter3 += Time.deltaTime;
                if (counter3 > 2f)
                {
                    Cammo = 30;
                    cd2 = 0;
                    counter3 = 0;
                    wot = 0;
                    
                }
            }
        
	}
    private void pangpang()
    {
        Instantiate(bullet, transform.position, transform.rotation);
        GetComponent<AudioSource>().Play();
        counter = 0;
        yolo = 0;
        
        Cammo = Cammo - 1;
        ammotext.GetComponent<Text>().text = Cammo + "/30";
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(DamagePerShot, hit.point);
                //Debug.Log("enemy hit");
            }
            else
            {
                // Debug.Log("wall hit");
                Instantiate(bullethole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            }
        }
    }
	IEnumerator Burst()
	{
		


        pangpang();
		yield return new WaitForSeconds(0.1f);
		
		
		

        pangpang();
		yield return new WaitForSeconds(0.1f);
		

        pangpang();
		
	}
}
