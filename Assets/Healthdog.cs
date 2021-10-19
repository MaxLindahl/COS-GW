using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Healthdog : MonoBehaviour {

	public int HP = 100;
    GameObject slider;
    [SerializeField] private AudioClip m_takedmg;
    private AudioSource audiosource;

	// Use this for initialization
	void Start () {
        slider = GameObject.FindGameObjectWithTag("slider");
        audiosource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
       

	if (HP <= 0) {
        Application.LoadLevel("MAIN");
		}
	}
	public void takedamage(int damageamount)
	{
        audiosource.clip = m_takedmg;
        GetComponent<AudioSource>().Play();
        slider.GetComponent<Slider>().value -= damageamount;
            HP = (HP - damageamount);
           
        
	}
}
