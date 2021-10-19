using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
        public int startingHealth = 100;
    public float currentHealth;
    public int scorevalue;
    [SerializeField]  private AudioClip Hitm;
    private AudioSource m_AudioSource;
    public float sinkSpeed = 2.5f;

   // public int scoreValue = 1000;
    //public AudioClip deathClip;
    Text text;

    //Animator anim;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    GameObject canv;

    void Awake ()
    {
       // anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        //hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent <CapsuleCollider> ();
        m_AudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();

        canv = GameObject.FindGameObjectWithTag("canvi");
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage (float amount, Vector3 hitPoint)
    {
        if(isDead)
            return;

        enemyAudio.Play ();

        m_AudioSource.clip = Hitm;
        m_AudioSource.Play();
        GameObject.FindGameObjectWithTag("canvi").GetComponent<Animator>().SetTrigger("hitmark");
        currentHealth -= amount;
            
       // hitParticles.transform.position = hitPoint;
       // hitParticles.Play();

        if(currentHealth <= 0)
        {
            

            Death ();
        }
    }

 void Death ()
    {
        isDead = true;
        
        capsuleCollider.isTrigger = true;

        StartSinking();
        GameObject.FindGameObjectWithTag("scorem").GetComponent<score>().givescore(scorevalue);
       // anim.SetTrigger ("Dead");

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }

    
    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;


        m_AudioSource.clip = Hitm;
        m_AudioSource.Play();
        ScoreManager.score += scorevalue;

        canv.GetComponent<Animator>().SetTrigger("zombiekilled");

        Destroy (gameObject, 2f);
    }
}
