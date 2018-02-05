using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class check_diesel : NetworkBehaviour
{
    private RaycastHit r;
    public bool a;
    private Vector3 kkkk= new Vector3(35,79,44);

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = kkkk;
        }
            if (Input.GetKeyDown(KeyCode.U))
        {

            a = true;
            print("pls");
            StartCoroutine(delay_attack());
        }
        if (a == true)
        {
         // kkkk = transform.position;
                    //  GetComponent<Rigidbody>().isKinematic = false;
                    GetComponent<FirstPersonController>().enabled = false;
            if (Input.GetKeyDown(KeyCode.O))
            {


                
                GetComponent<FirstPersonController>().enabled = true;
                //    GetComponent<Rigidbody>().isKinematic = true;
            }

        }

        if (Physics.Raycast(transform.position, transform.forward, out r, 2))
        {
            //print(r.transform.name);
            if (r.transform.tag == "diesel" && Input.GetKeyDown(KeyCode.F))
            {
                //print("d");
                //r.transform.GetComponent<gui_diesel>().audioplay();
                Cmdr(r.transform.gameObject);
            }
        }
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
    private IEnumerator delay_attack()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<FirstPersonController>().enabled = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(1, 1.5f, 0) * 400);
        yield return new WaitForSeconds(2f);
        kkkk = transform.position;
        yield return new WaitForSeconds(0.1f);
        transform.position = kkkk;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<FirstPersonController>().enabled = true;
        GetComponent<FirstPersonController>().m_MoveDir = kkkk;
        a = false;
    }
}
