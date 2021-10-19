using UnityEngine;
using System.Collections;

public class knifedajf : MonoBehaviour {

    Ray ray;
    public int DamagePerKnife = 65;
    public GameObject bullet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void knifeknife()
    {
        Instantiate(bullet, transform.position, transform.rotation);

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.Log("s1");
        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("s2");
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("s3");
                enemyHealth.TakeDamage(DamagePerKnife, hit.point);
                Debug.Log("enemy hit");
            }

        }
    }
}
