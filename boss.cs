using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class boss : NetworkBehaviour
{
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
    public GameObject blood;
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
    public AudioSource au;
    public bool argressive;
    public Collider[] all_coll;
    private float count;
    private float ti2;
    private bool locked;
    public Transform ui_scr;
    private int random_delay;
    private bool run_triggre;
    public GameObject dummy;
    private bool cannotatk;
    private bool navzero;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(DD_attack());
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        red = GameObject.Find("Red").GetComponent<Animator>();
        ani = GetComponent<Animator>();
        aui = GetComponent<AudioSource>(); //breath
         //battle sound
        // player = FindClosestPlayer();
        hp = 500;
        //random_delay = Random.Range(4, 8);

        // player = playersync;
        if (ui_scr == null)
        {
            ui_scr = GameObject.Find("text_score").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (cannotatk == true)
        {
            atk = false;
          
        }
        if (navzero)
        {
            nav.speed = 0;
        }
        else
        {
            nav.speed = 7;
        }
        //print(ti2);
        if (run_triggre == true)
        {
            ani.SetBool("run", true);
        }
        else if(atking==true)
        {
            ani.SetBool("run", false);
        }
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
        

        }

        if (ani.GetBool("dead") == true)
        {
            au.Stop();
            aui.Stop();
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
                au.Stop();
                aui.Stop();
                atk = false;
            }

        }
        else
        {
            ti = 0;

        }

        if (hp <= 0 && player != null)
        {
            foreach (BoxCollider box in GetComponents<BoxCollider>())
            {
                box.enabled = false;
            }
            if (locked == false)
            {
                GetComponent<des_zombie>().desit = true;
                locked = true;
            }


            // Destroy(this.gameObject);
            if (check_dy == false)
            {
                nav.speed = 0;

                nav.SetDestination(transform.position);
                //  print("dead");
                au.Stop();
                aui.Stop();
                ani.SetBool("dead", true);
                Invoke("Des", 5);
                check_dy = true;

            }
            else if (ani.GetBool("dead") == true)
            {
                nav.speed = 0;
                nav.SetDestination(transform.position);
            }

        }
        else if (player != null && ani.GetBool("dead") == false)
        {
            if (player != null)
            {
                player = FindClosestPlayer();
                if (Vector3.Distance(player.transform.position, transform.position) < 60)
                {
                    Cmdsync(player);
                    if (au.isPlaying == false)
                    {
                        au.Play();
                    }
                   
                    run_triggre = true;
                    
                    

                }


                if (Vector3.Distance(transform.position, player.transform.position) < 10f)
                {
                  
                    if (Vector3.Distance(transform.position, player.transform.position) < 3f)
                    {
                        var q = Quaternion.LookRotation(player.transform.position - transform.position);
                        transform.rotation = Quaternion.Lerp(transform.rotation, q, 9 * Time.deltaTime);
                        //ani.Play("attack");
                        atk = true;
                    }
                    if (Physics.Raycast(box.position, player.transform.position - box.transform.position, out ray))
                    {

                        if (ray.transform.tag == "Player")
                        {

                            var q = Quaternion.LookRotation(player.transform.position - transform.position);
                            transform.rotation = Quaternion.Lerp(transform.rotation, q, 9 * Time.deltaTime);
                           
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
        GameObject dummy_ball = Instantiate(dummy, player.transform.position, Quaternion.identity) as GameObject;
        dummy_ball.GetComponent<Rigidbody>().AddForce(new Vector3(1,1,0) * 452);
        player.GetComponent<check_diesel>().getX(dummy_ball);
        int a = Random.Range(0, 2);
        navzero = true;

        if (a == 0)
        {
            ani.Play("attack");
        }
        else
        {
            ani.Play("attack2");
        }
        yield return new WaitForSeconds(0.08f);

       

        if (atk == true)
        {

            player.GetComponent<hp_hud>().paa = true;
     
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            //player.GetComponent<FirstPersonController>().m_RunSpeed = 2;
            //player.GetComponent<FirstPersonController>().m_WalkSpeed = 2;
            //aui.clip = clip[0];
            //aui.Play();
        }

        yield return new WaitForSeconds(0.9f);
       
        if (atk == true)
        {
         
            player.GetComponent<hp_hud>().paa = true;
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            player.GetComponent<hp_hud>().gethit();
            

            //player.GetComponent<FirstPersonController>().m_RunSpeed = 2;
            //player.GetComponent<FirstPersonController>().m_WalkSpeed = 2;
            //aui.clip = clip[1];
            //aui.Play();
        }
        yield return new WaitForSeconds(0.67f);
        Destroy(dummy_ball);
        player.GetComponent<check_diesel>().stopX();
        //player_now.GetComponent<FirstPersonController>().m_RunSpeed = 10;
        //player.GetComponent<FirstPersonController>().m_WalkSpeed = 5;
        navzero = false;
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
    private IEnumerator DD_attack()
    {
        cannotatk = true;
        //nav.speed = 0;
        navzero = true;
        yield return new WaitForSeconds(5.57f);
        //nav.speed = 7;
        navzero = false;
        cannotatk = false;
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
