using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axe : MonoBehaviour {
    private Animator ani;
    private bool hitting;
    private AudioSource sound;
    public Transform hitboxpos;
    public GameObject hitbox;
    private bool hold;
    [SerializeField]
    private int W_locked;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
       
	}
	
	// Update is called once per frame
	void Update () {
        //W_locked = GetComponentInParent<weapon_switch>().W_lock;
        if ((Input.GetAxis("Fire1") > 0 || Input.GetMouseButtonDown(0))&& hold == false)
        {
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName("axe_swing"))
            {
                if (ani.GetBool("away") ==false )
                {
                    ani.Play("axe_swing");
                    StartCoroutine(hitboxdelay());
                }
             
            }
          
           
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)||W_locked==0)
        {
            if (ani.GetBool("away") == true)
            {
               

                StartCoroutine(axedelay());

            }



        }
        if (Input.GetKeyDown(KeyCode.Alpha2)||W_locked==1)
        {
            hold = true;
            ani.SetBool("away",true);
        }
    }
   IEnumerator hitboxdelay()
    {
        yield return new WaitForSeconds(0.11f);
        sound.Play();
        yield return new WaitForSeconds(0.32f);
        
        GameObject standard=  Instantiate(hitbox, hitboxpos.position, Quaternion.identity) as GameObject;
        standard.transform.parent = this.transform;
        hitting = false;
    }
    private IEnumerator axedelay()
    {
      
        yield return new WaitForSeconds(.7f);
        // print("WaitAndPrint " + Time.time);
        ani.SetBool("away", false);
        hold = false;
    }
}
