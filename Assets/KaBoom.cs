using UnityEngine;
using System.Collections;

public class KaBoom : MonoBehaviour {
    public GameObject explosion;
    public Rigidbody rb;
    public float counter = 0f;
    public float timetoblow = 3f;
    private int boom = 0;
    Collider[] collidersInRange;
    Vector3 grenadeOrigin;
    private float exploDamage = 100f;
    private float explosiveRadius = 10.0f;
    EnemyHealth damageByGrenadeExplosion;
    float distance;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 750);
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;

        if (counter > timetoblow)
        {
            if (boom == 1)
            {
                Destroy(gameObject, 2f);
            }
            if (boom == 0)
            {
                grenadeOrigin = transform.position;
                GetComponent<AudioSource>().Play();
                collidersInRange = Physics.OverlapSphere(grenadeOrigin, explosiveRadius);


                foreach (Collider col in collidersInRange)
                {
                    distance = Vector3.Distance(transform.position, col.transform.position);
                    
                    distance = distance / 2;
                    exploDamage = 400 / distance;
                    Debug.Log(exploDamage);
                    Debug.Log(distance);
                    damageByGrenadeExplosion = col.GetComponent<EnemyHealth>();
                    if (damageByGrenadeExplosion != null)
                        damageByGrenadeExplosion.TakeDamage(exploDamage,grenadeOrigin);
                }



                Instantiate(explosion, transform.position, transform.rotation);
                

                boom = 1;
            }
        }
	}
    
    
}
