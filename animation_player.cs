using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_player : MonoBehaviour {

    public Animator ani;
    [SerializeField]
    //rifle r;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
        float translation = Input.GetAxis("Vertical");
        
        float rotation = Input.GetAxis("Horizontal");
        
        float sum = Mathf.Abs(translation) + Mathf.Abs(rotation);

        ani.SetFloat("InputMag", sum);

       
            //ani.SetBool("shot",r.shooting);
        
       
        
        /*if(r.shooting == true)
        {
            ani.SetBool("shot", true);
        }*/
        
    }
}
