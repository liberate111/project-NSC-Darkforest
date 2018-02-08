using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class check_diesel : NetworkBehaviour
{
    private RaycastHit r;
    public bool a;

    private GameObject x;

    private Vector3 p11 = new Vector3(305, 100, 719.4f);
    private Vector3 p12 = new Vector3(1032.3f, 100, 793.24f);
    private Vector3 p13 = new Vector3(1198, 100, 658.6f);

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            this.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //check bug
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            transform.position = p11;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            transform.position = p12;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = p13;
        }

        if(a==true)
        {
            transform.position = x.transform.position;
        }
        if (Physics.Raycast(transform.position, transform.forward, out r, 5))
        {

            if (r.transform.tag == "diesel" && (Input.GetKeyDown(KeyCode.F) || Input.GetAxis("L1") == 1) )
            {
                //print("d");
                //r.transform.GetComponent<gui_diesel>().audioplay();
                Cmdr(r.transform.gameObject);
            }
        }
    }
    public void getX(GameObject dummy)
    {
        x = dummy;
        a = true;
    }
    public void stopX()
    {
        x = null;
        a = false;
    }
    [Command]
    void Cmdr(GameObject r)
    {
        Rpcr(r);
    }
    [ClientRpc]
    void Rpcr(GameObject r)
    {
        r.transform.GetComponent<gui_diesel>().audioplay();
    }

}
