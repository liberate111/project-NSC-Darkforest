using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class basic_zombie :NetworkBehaviour {
    public UnityEngine.AI.NavMeshAgent nav;
    
     public GameObject player;
    [SyncVar]
    GameObject playersync;
     [SyncVar]
    GameObject p;
    private Animator red;
    private int hp;
    public Animator ani;
    RaycastHit ray;
    
    public bool check_dy;
    private bool atk;
    private bool atking;
    private bool atk_count;
    private float ti;
    private bool follow;
    //private float ti2;
    //FirstPersonController fpc;
    public Transform botsound;
    public bool hit;
    public Transform slashsound;
    public Transform box;
    public AudioClip[] clip;
    private AudioSource aui;
    public bool argressive;
    public Collider[] all_coll;
    private float count;
    private float ti2;
    private bool locked;
    public Transform ui_scr;
    private int random_delay;
    
    // Use this for initialization
    void Start () {

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        red = GameObject.Find("Red").GetComponent<Animator>();
        ani = GetComponent<Animator>();
        aui = GetComponent<AudioSource>();
       // player = FindClosestPlayer();
        hp = Random.Range(2,5);
        random_delay = Random.Range(4, 8);

        // player = playersync;
        if (ui_scr == null)
        {
            ui_scr = GameObject.Find("text_score").transform;
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        //print(ti2);
        if (hp <= 0)
        {
            ani.SetBool("dead", true);
            GetComponent<hpbot>().dead();
        }
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
        
        }
        else
        {
            //player = FindClosestPlayer();
            if (player == null)
            {
                player = FindClosestPlayer();
            }
            if (argressive)
            {
               
                    Cmdsync(player);
                
               
            }
            
        }

           if (ani.GetBool("dead") == true)
        {
            botsound.GetComponent<AudioSource>().Stop();
            atk = false;
            nav.speed = 0;
            nav.SetDestination(transform.position);
        }
        


        if (atk == true)
        {   
            ti = ti + Time.deltaTime;
            if (atking == false)
            {   
                StartCoroutine(attack(player));
                atking = true;
            }
           
         else if (ani.GetBool("dead") == true)
            {
                botsound.GetComponent<AudioSource>().Stop();
                atk = false;
            }
         
        }
        else
        {
            ti = 0;
          
        }

        if (hp <= 0&&player!=null)
        {
            if (locked == false)
            {
                GetComponent<des_zombie>().desit = true;
                locked = true;
            }
            
            foreach(BoxCollider box in GetComponents<BoxCollider>())
            {
                box.enabled = false;
            }
            // Destroy(this.gameObject);
            if (check_dy == false)
            {
                nav.speed = 0;
                    
                nav.SetDestination(transform.position);
              //  print("dead");
               botsound.GetComponent<AudioSource>().Stop() ; 
                ani.SetBool("dead", true);
                Invoke("Des", 5);
                check_dy = true;

            }else if (ani.GetBool("dead") == true)
            {
                nav.speed = 0;
                nav.SetDestination(transform.position);
            }

        }
        else if(player!=null && ani.GetBool("dead") == false)
        {
            if (player != null)
            {
                player = FindClosestPlayer();
                if (Vector3.Distance(player.transform.position, transform.position) < 30 && player!=null)
                {
                    Cmdsync(player);

                }
                

                if (Vector3.Distance(transform.position, player.transform.position) < 6.5f)
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < 2f)
                    {
                        var q = Quaternion.LookRotation(player.transform.position - transform.position);
                        transform.rotation = Quaternion.Lerp(transform.rotation, q, 9 * Time.deltaTime);
                        ani.Play("attack1");
                        atk = true;
                    }
                        if (Physics.Raycast(box.position, player.transform.position - box.transform.position, out ray))
                    {

                        if (ray.transform.tag == "Player")
                        {

                            var q = Quaternion.LookRotation(player.transform.position - transform.position);
                            transform.rotation = Quaternion.Lerp(transform.rotation, q, 9 * Time.deltaTime);
                            ani.Play("attack1");
                            atk = true;
                        }
                        else
                        {
                            atk = false;
                        }
                    }
                    
                    else
                    {

                        atk = false;
                    }


                }
                else
                {
                    atk = false;
                }
            }
        }
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "bullet")
        {
            if (col.transform.parent != null)
            {


                if (col.transform.parent.GetComponent<axe>() == true)
                {

                    slashsound.GetComponent<AudioSource>().Play();
                    hp--;
                    hp--;
                }
            
        }
            hp--;
            Destroy(col.gameObject);

            GetComponent<bloodclient>().buildblood(col.transform.position);
           
        }
    }
    private IEnumerator attack(GameObject player_now)
    {
        
            yield return new WaitForSeconds(0.08f);



            if (atk == true)
            {

            player.GetComponent<hp_hud>().paa = true;

            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<FirstPersonController>().m_RunSpeed = 2;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 2;
            aui.clip = clip[0];
            aui.Play();
            }

            yield return new WaitForSeconds(0.9f);
            if (atk == true)
            {

            player.GetComponent<hp_hud>().paa = true;
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<FirstPersonController>().m_RunSpeed = 2;
            player.GetComponent<FirstPersonController>().m_WalkSpeed = 2;
            aui.clip = clip[1];
            aui.Play();
            }
            yield return new WaitForSeconds(0.67f);
            player_now.GetComponent<FirstPersonController>().m_RunSpeed = 10;
        player.GetComponent<FirstPersonController>().m_WalkSpeed = 5;
        atking = false;

        
    }
    void Des()
    {
       ui_scr.GetComponent<score>().plusscore(10);
       NetworkServer.Destroy(this.gameObject);
    }
    void find_p()
    {
        player = FindClosestPlayer();
       // print("find");
        
    }
    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    [Command]
    void Cmdsync(GameObject p)
    {
        //print("cmd");

        playersync = player;
        Rpcstart(playersync);
    }
    [ClientRpc]
    void Rpcstart(GameObject player)
    {
      
        Invoke("find_p", 5);

     
        nav.SetDestination(player.transform.position);
    }
}
