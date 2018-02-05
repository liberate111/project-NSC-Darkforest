using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class gun : NetworkBehaviour {
    [SerializeField]
    private Animator ani;
    private bool Reloading;
    private bool shoot;
    private bool running;
    private bool hold;
    private AudioSource Gun_sound;
    public AudioClip[] sa;
    public GameObject bullet;
    public Transform bul_Hit;
    [SerializeField]
    public int o;
    public int allammo;
    public GameObject cartage;
    public GameObject car_spot;
    public GameObject buller_spwan;
    public Transform gaden;
    public GameObject fpsplayer;
    public int HP;
    
   // public Light sang; //lighting enable
    [SerializeField]
    private int W_locked;
    private bool ready_joy;
    [SerializeField]
    build_bullet bbuild;

    Transform guntext;

    public gunsound g;
    // Use this for initialization
    void Start () {
        // guntext = GameObject.Find("Gun_ammo").transform;
        bbuild.GetComponentInParent<build_bullet>();
        ani = GetComponent<Animator>(); 
        Gun_sound = GetComponent<AudioSource>();
        o = 8;
        allammo = 91;
        HP = 100;

        // ani.SetBool("away", true);
        g = GetComponentInParent<gunsound>();
    }
	
	// Update is called once per frame
	void Update () {
       // guntext.GetComponent<bulletui>().canuse = o;
      //  guntext.GetComponent<bulletui>().max = allammo;
        if (Input.GetAxis("Fire1") == 0)
        {
            ready_joy = true;
            
        }
  
   /*    if (W_locked == 0)  
        {
            hold = true;
            if(ani.GetBool("away")==false)
            ani.SetBool("away", true);
        }
        else if(W_locked == 1)
        {
            if (ani.GetBool("away") == true)
                StartCoroutine(gundelay());

        }*/
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
             running = true;
                   // print(fpsplayer.GetComponent<Rigidbody>().velocity);
                    ani.SetBool("running", true);
           

        }else if (Input.GetKeyUp(KeyCode.LeftShift) )
        {
            running = false;
            ani.SetBool("running", false);
        }
        if ((((Input.GetAxis("Fire1") >0&&ready_joy==true)|| Input.GetMouseButtonDown(0))&&o>0&& !this.ani.GetCurrentAnimatorStateInfo(0).IsName("reload")&&running==false))
        {
            if (!this.ani.GetCurrentAnimatorStateInfo(0).IsName("shoot")&& !this.ani.GetCurrentAnimatorStateInfo(0).IsName("zero")&&Reloading==false)
            {
                if (hold==false&&ani.GetBool("away")==false)
                {
                    
                 
                    ready_joy = false;
                    
                    ani.Play("shoot");
                    Gun_sound.clip = sa[Random.Range(0, 2)];
                    //Gun_sound.Play();
                    g.soundShoot();
                    GameObject ss = Instantiate(cartage, car_spot.transform.position, Quaternion.Euler(90, 0, 0)) as GameObject;
                    ss.GetComponent<Rigidbody>().AddForce((gaden.position - car_spot.transform.position) * Random.Range(300, 340));
                    o--;
                    bbuild.build();
                  GetComponentInParent<sang>().light_trigger = true;
                }

            }

        }
        else if( o== 0&&allammo>0)
        {
          /*  if(Reloading == false)
            {
                 if (!this.ani.GetCurrentAnimatorStateInfo(0).IsName("zero"))
                {
                    //Reloading = true;
                    if (Gun_sound.isPlaying == false)
                    {
                        Gun_sound.clip = sa[2];
                        Gun_sound.Play();
                        ani.SetBool("zero", true);
                        StartCoroutine(Wait());
                    }
              
                }
               
            }*/
           

        }
       if ((Input.GetAxis("Reload") > 0 || Input.GetKeyDown(KeyCode.R) ) &&Reloading==false&&running==false&&allammo>0)
            {
            if (!this.ani.GetCurrentAnimatorStateInfo(0).IsName("shoot"))
            {
                if(o<8)
                Reloading = true;
            }
            }
     
        if (Reloading == true&&ani.GetBool("reload")==false)
        {
            // o = 0;
            if (Gun_sound.isPlaying == false)
            {
                Gun_sound.clip = sa[2];
                Gun_sound.Play();
                
                ani.SetBool("reload", true);
                StartCoroutine(Wait());
            }
         

        }
        else if(Reloading==false)
        {
            ani.SetBool("reload", false);
        }
        if (shoot == true)
        {
            StartCoroutine(Waittoshoot());
        }

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.17f);
        // print("WaitAndPrint " + Time.time);
        int a= 8-o;
        if (ani.GetBool("away") == false)
        {
            if (a <= allammo)
            {
                allammo -= a;
                o = 8;
            }
            else if (allammo < a)
            {
                o +=allammo;
                allammo = 0;
            }

        }



        Reloading = false;
        ani.SetBool("zero", false);
    }
    private IEnumerator Waittoshoot()
    {
        yield return new WaitForSeconds(0.12f);
        // print("WaitAndPrint " + Time.time);
        shoot = false;
    }
   
    private IEnumerator gundelay()
    {
       // yield return new WaitForSeconds(0.8f);
        // print("WaitAndPrint " + Time.time);
      
        yield return new WaitForSeconds(.5f);
        ani.SetBool("away", false);
        hold = false;
    }
 
    
}
