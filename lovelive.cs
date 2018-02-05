using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;


public class lovelive : NetworkBehaviour {
    private RaycastHit ray;
    private RaycastHit ray_heal;
    public Transform gg;
    private hp_hud hp;
    private bool had_medic;
    private float ti=0;
    private float ti_healfriend = 0;
    [SerializeField]
    GameObject medic_icon;
    [SerializeField]
    private bool hold_toheal;
    [SerializeField]
    private bool hold_toheal_friend;
    [SerializeField]
    private bool x;
    private GameObject img_circle;
    private GameObject text;
    private GameObject player_temp;
	// Use this for initialization
	void Start () {
        hp = GetComponent<hp_hud>();
        
        img_circle = GameObject.Find("Loading");
       // img_circle.SetActive(false);
        medic_icon = GameObject.Find("medic");
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (hold_toheal_friend == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                x = true;

            }
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            x = false;
            hold_toheal_friend = false;
        }

            if (transform.tag == "Player")
            {
                if (hold_toheal == true && x == false)
                {
                    //GetComponentInChildren<gun>().enabled = false;
                    GetComponent<FirstPersonController>().enabled = false;
                    img_circle.SetActive(true);
                    ti = ti + Time.deltaTime;
                    img_circle.GetComponent<Image>().fillAmount = ti / 5;
                    img_circle.GetComponentInChildren<Text>().text = "" + Mathf.RoundToInt(ti);
                    if (ti > 5)
                    {
                        ti = 0;

                        Cmddestroyobject(null, 2);
                        had_medic = false;
                        hold_toheal = false;

                    }
                }
                else if (hold_toheal == false && x == false)
                {
                    // GetComponentInChildren<gun>().enabled = true;
                    GetComponent<FirstPersonController>().enabled = true;
                    img_circle.SetActive(false);
                    ti = 0;
                    ti_healfriend = 0;
                }
                else if (x == true && hold_toheal == false)
                {
                    GetComponent<FirstPersonController>().enabled = false;
                    img_circle.SetActive(true);
                    ti_healfriend = ti_healfriend + Time.deltaTime;
                    img_circle.GetComponent<Image>().fillAmount = ti_healfriend / 5;
                    img_circle.GetComponentInChildren<Text>().text = "" + Mathf.RoundToInt(ti_healfriend);
                    if (ti_healfriend > 5)
                    {
                        ti_healfriend = 0;

                       //player_temp.transform.GetComponent<hp_hud>().i = 100;
                    Cmddestroyobject(player_temp, 1);
                        had_medic = false;
                    player_temp = null;
                    hold_toheal_friend = false;
                    x = false;

                    }
                }
                if (Physics.Raycast(gg.position, gg.forward, out ray_heal, 3))
                {

                    if (ray_heal.transform.tag == "Player" && had_medic)
                    {
                        hold_toheal_friend = true;
                    player_temp = ray_heal.transform.gameObject;

                    }
                    if(ray_heal.transform.tag != "Player")
                    {
                        hold_toheal_friend = false;
                    player_temp = null;

                }
                }
                if (had_medic)
                {

                    medic_icon.SetActive(true);


                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        hold_toheal = true;
                    

                    }
                    if (Input.GetKeyUp(KeyCode.Q))
                    {

                        hold_toheal = false;
                    }
                }
                else
                {
                    medic_icon.SetActive(false);
                }
                Debug.DrawRay(gg.position, gg.forward, Color.red);
                if (Physics.Raycast(gg.position, gg.forward, out ray))
                {
                    if (ray.transform.tag == "ammobox")
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {

                            //Destroy(ray.transform.gameObject);

                            GetComponentInChildren<rifle>().all_ammo += 30;
                            ray.transform.GetComponent<network_des>().des();
                            Cmddestroyobject(ray.transform.gameObject, 0);
                    }

                    }
                    if (ray.transform.tag == "medkit")
                    {
                        if (Input.GetKeyDown(KeyCode.F) && had_medic == false)
                        {
                            //print("destroy");
                            had_medic = true;
                            ray.transform.GetComponent<network_des>().des();
                            //  Destroy(ray.transform.gameObject);
                            Cmddestroyobject(ray.transform.gameObject, 0);
                            hp.medd_kit++;
                        }

                    }
                }
            }
        
	}

    public bool getholdtoheal()
    {
        return hold_toheal;
    }
    [Command]
    void Cmddestroyobject(GameObject a,int x)
    {
        if(x==0)
        {
            NetworkServer.Destroy(a);
        }
        else if(x == 1){
            Rpcdesobject(1, a);
        }
        else
        {
            Rpcdesobject(2,null);
        }
    }
    [ClientRpc]
    void Rpcdesobject(int x,GameObject a)
       
    {
        if (x == 0) { }
            else if (x == 1)
        {
            a.GetComponent<hp_hud>().roi_hp();
        }
    else
        this.transform.GetComponent<hp_hud>().i = 100; //wrong !
    }
}
