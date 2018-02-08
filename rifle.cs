using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rifle : MonoBehaviour {
    private Animator ani;
    public int ammo;
    public int all_ammo;
    public int buf_ammo;
    public bool reloading;
    public bool shooting;
    private bool sang_lock;
    private AudioSource au;
    public AudioClip[] ac;
    private bool auto_check;
    public gunsound g;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        ammo = 30;
        all_ammo = 90;
       
	}
	
	// Update is called once per frame
	void Update () {
        
        if(transform.tag == "deadplayer")
        {
            shooting = false;
        }
        if (shooting)
        {
            if (sang_lock == false)
            {
                sang_lock = true;
                StartCoroutine(Wait_sang());
            }
        }
        if ((ammo == 0 && all_ammo == 0))
        {
            shooting = false;
            ani.SetBool("shot", false);
            //au.loop = false;
            g.au.loop = false;
            //sound no ammo
        }
        if(ammo == 0 && shooting == true)
        {
            shooting = false;
            ani.SetBool("shot", false);
            //au.loop = false;
            g.au.loop = false;
        }
        if (shooting == false)
        {
            ani.SetBool("shot", false);
        }

        //ani.SetBool("shot", shooting);

        
        if (reloading == false && ammo > 0)
        {
            if ((Input.GetMouseButtonDown(0) || Input.GetAxis("R1") == 1) && ammo > 0)
            {
                shooting = true;
               

            }
            if (Input.GetMouseButton(0)!=true && Input.GetAxis("R1") == 0)
            {
                shooting = false;
                ani.SetBool("shot", false);
                //au.loop = false;
                if(g.au!=null)
                g.au.loop = false;
           
            }
            if(ani.GetBool("run")== true)
            {
                shooting = false;
            }
       

            if(ani.GetBool("run") == false && shooting)
            {
                if (auto_check == false)
                {
                    GetComponentInParent<build_bullet>().build();
                    ammo -= 1;
                    auto_check = true;
                    Invoke("auto_delay", 0.2f);
                    //au.Play();
                    g.soundShoot();
                }
                
                ani.SetBool("shot",true);
                //au.loop = true;
                g.au.loop = true;
                au.clip = ac[0];  //reload sound for me only
                
                if(g.au.isPlaying == false)
                {
                    g.soundShoot();
                }
                


            }
            if ((Input.GetMouseButtonDown(0)||Input.GetAxis("R1")==1) && ani.GetBool("run") == false && ani.GetBool("reload") == false )
            {
                ani.Play("shoot");
                //au.Play();
                //GetComponentInParent<build_bullet>().build();
                //ammo -= 1;
            }
            
        }
        if ((Input.GetKeyDown(KeyCode.R)||Input.GetAxis("O")==1) && reloading == false && ammo < 30 && all_ammo > 0 && ani.GetBool("run") == false&&shooting==false)
        {
            //play sound reload ammo !!
            //print("yyyy");
            au.clip = ac[1];
            au.Play();
            StartCoroutine(Wait_Reload());
            reloading = true;
            if(all_ammo > 30 && ammo != 30)
            {
                buf_ammo = ammo;
                ammo = 30;
                all_ammo -= (30 - buf_ammo);
            }
            else if(all_ammo <= 30 && ammo != 30)
            {
                buf_ammo = ammo;
                if(buf_ammo + all_ammo > 30)
                {
                    ammo = 30;
                    all_ammo -= (30 - buf_ammo);
                }
                else
                {
                    ammo += all_ammo;
                    all_ammo = 0;
                }
                
            }
            else if(all_ammo == 30 && ammo == 0)
            {
                ammo = 30;
                all_ammo = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetAxis("R2") > 0.5f)
        {
            ani.SetBool("run", true);
            GetComponentInParent<weapon_switch>().enabled = false;
            g.au.Stop();
        }
        else if ((Input.GetKey(KeyCode.LeftShift)!=true && Input.GetAxis("R2")  == 0))
        {
            ani.SetBool("run", false);
            if(GetComponentInParent<weapon_switch>().gun_pistol.GetComponent<gun>().enabled == false)
            {
                GetComponentInParent<weapon_switch>().enabled = true;
            }
            
        }

	}
    IEnumerator Wait_Reload()
    {
        //print(Time.time);
        ani.SetBool("reload", true);
        yield return new WaitForSeconds(2f);
        reloading = false;
        ani.SetBool("reload", false);
        //print(Time.time);
    }
    IEnumerator Wait_sang()
    {
        GetComponentInParent<sang>().light_trigger = true;
        yield return new WaitForSeconds(.1f);
        sang_lock = false;

        //print(Time.time);
    }
    void auto_delay()
    {
        auto_check = false;
    }
}
