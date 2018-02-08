using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class hp_hud : NetworkBehaviour {

    [SyncVar]
    public float i = 100;
    [SyncVar]
    public string playerName;
    public Transform red_hud;

    public bool paa;
    [SerializeField]
    private GameObject HP_TEXT;
    [SerializeField]
    private GameObject medic_c;
    public GameObject red_bar;
    public int medd_kit;
    public LayerMask everything;
    private bool stop;
    private bool trigger_deadcheck;
    private bool _gameover;
    private float count_dead;
    private GameObject text_dead;
    public Transform body;


    public GameObject medic_icon;
    private GameObject test_player;
    public rifle r;
    public static int zombie_hit;
    public GameObject g;
    private bool lock_level;
    public GameObject tomb_stone;
    public bool build_tomb;

    private bool death_check;
    // Use this for initialization
    void Start () {
        //red_bar = GameObject.Find("red_bar");
        text_dead = GameObject.Find("dead_count");
        medic_icon.SetActive(false);
        HP_TEXT = GameObject.Find("Hp");
        medic_c = GameObject.Find("medic_count");
        red_hud = GameObject.Find("Red").transform;
        if (!isLocalPlayer)
        {
         
            this.enabled = false;
        }

      
    }
	
	// Update is called once per frame
	void Update () {

        if (g == null)
        {
            g = GameObject.Find("Level");
        }
        else
        {
            zombie_hit = g.GetComponent<level_game>().zom_hit;
        }

        /*if(zombie_hit == 1)
        {
            Cmdtrigger(6);
            lock_level = true;
        }
        if (zombie_hit == 2)
        {
            Cmdtrigger(7);
            lock_level = true;
        }
        if (zombie_hit == 5)
        {
            Cmdtrigger(8);
            lock_level = true;
        }*/

        //}
        if (transform.tag == "deadplayer")
        {
            GetComponent<weapon_switch>().enabled = false;
            GetComponent<FirstPersonController>().enabled = false;
            if (i > 0)
            {
                Cmdtrigger(1);
            }
        }


        if (count_dead < 0)
        {
            
            if(build_tomb == false)
            {
                //Instantiate(tomb_stone,transform.)
                build_tomb = true;
                tag_death();
                text_dead.GetComponent<Text>().text = "You are dead";
                count_dead = 0;
                //GetComponent<playermultiplayer>().player_model.SetActive(false);
            }
            

        }
        else
        {

            text_dead.GetComponent<Text>().text = "" + Mathf.RoundToInt(count_dead);

        }
        if (gameObject.transform.tag=="Player")
        {
            GetComponent<FirstPersonController>().enabled = true;
           // GetComponentInChildren<gun>().enabled = true;
            //  red_hud.GetComponent<Animator>().SetBool("died", false);
            
            count_dead = 10;
            text_dead.SetActive(false);
        }
        else
        {
         
            test_player = GameObject.FindGameObjectWithTag("Player");//optimize
            text_dead.SetActive(true);
            if (test_player != null)
            {
                if (Vector3.Distance(test_player.transform.position, this.transform.position) < 15)
            {
              
                count_dead -= 0;
            }
            else if (Vector3.Distance(test_player.transform.position, transform.position) > 15)
            {
                count_dead -= Time.deltaTime;
                
            }
           
            }
            else
            {
            
                GetComponent<death_check>().findplayer();
            }
               

       

        }
        if (paa)
        {
            redscreen();
            paa = false;
        }
      
        //HP_TEXT.GetComponent<Text>().text = "" + i + "/100";
        // medic_c.GetComponent<Text>().text = "x"+medd_kit;
       
        if (i <= 0 && stop == false)
        {
           
            if (transform.tag == "Player")
            {
                GetComponent<weapon_switch>().enabled = true;
                //  i = 0;
                // print("dead");
                trigger_deadcheck = true;
            }
            if (_gameover == false)
            {
                Invoke("kine", 5);
                Invoke("startgameover", 4.5f);
            }
         
           // cam.GetComponent<Rigidbody>().AddForce(transform.forward * 80);
            stop = true;
            
        }
        if (trigger_deadcheck == true)
        {
            if (transform.tag == "Player")
            {
                Cmdtrigger(0);
                GetComponent<death_check>().findplayer();
            }
            else
            {
                transform.tag = "Player";
                Cmdtrigger(1);
                GetComponent<FirstPersonController>().enabled = true;
                GetComponentInChildren<gun>().enabled = true;
            }

            trigger_deadcheck = false;
        }
      
    }
    void kine()
    {
        //cam.GetComponent<Rigidbody>().isKinematic = true;

    }
    void startgameover()
    {
       // print("Over");
    }
    public void redscreen()
    {
        red_hud.GetComponent<Animator>().Play("blood2");
    }
    public void revive()
    {
        Cmdtrigger(1);
    }
    public void gethit()
    {
        Cmdtrigger(2);
    }
    public void roi_hp()
    {
        Cmdtrigger(4);
    }
    public void gethit_byplayer()
    {
        Cmdtrigger(3);
    }
    public void tag_death()
    {
        Cmdtrigger(5);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "bullet")
        {
            if(isLocalPlayer)
            redscreen();
            GetComponent<hp_hud>().gethit_byplayer();
            
  
            if (i < 0)
            {
                i = 0;
            }
        }
    }
    [Command]
    void Cmdtrigger(int x)
    {
        SetDirtyBit(1u);
        Rpctriggerdead(x);
    }
    [ClientRpc]
    void Rpctriggerdead(int y)
    {
        if (y == 0)
        {
            transform.tag = "deadplayer";
            // body.GetComponent<Renderer>().material = mat_red;
           
            r.enabled = false;
            medic_icon.SetActive(true);
            //    GetComponent<hp_update>().enabled = false;
            // GetComponentInChildren<gun>().enabled = false;
            if (isLocalPlayer)
                red_hud.GetComponent<Animator>().SetBool("died", true);
        }
        else if (y == 1)
        {
            transform.tag = "Player";
            r.enabled = true;
            GetComponent<weapon_switch>().enabled = true;
            // body.GetComponent<Renderer>().material = mat_green;
            // GetComponent<FirstPersonController>().enabled = true;
    
            medic_icon.SetActive(false);
            i = 10;
            this.stop = false; //lock death one time
            
            if (isLocalPlayer)
                red_hud.GetComponent<Animator>().SetBool("died", false);
        }
        else if (y == 2)
        {
            i = i - zombie_hit;
        }
        else if (y == 3)
        {
            i = i - 10;
        }
        else if (y == 4)
        {
            i = 100;
        }
        else if (y == 5)
        {
            transform.tag = "death";
            medic_icon.SetActive(false);
            GetComponent<playermultiplayer>().player_model.SetActive(false);
            Instantiate(tomb_stone, transform.position, transform.rotation);
        }
        else if (y == 6)
        {
            zombie_hit = 1;
        }
        else if (y == 7)
        {
            zombie_hit = 2;
        }
        else if (y == 8)
        {
            zombie_hit = 5;
        }


    }
}
