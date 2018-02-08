using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class gui_diesel : NetworkBehaviour
{
    Vector3 screenPosition;
    public AudioSource au;
    private Animator ani;
    public Light[] l;
    [SerializeField]
    AudioSource au_zombiehorde;
    private bool alert;

    public spwanbot[] sbot;

    private float time_count;

    // Use this for initialization
    void Start()
    {
        au = GetComponent<AudioSource>();
        ani = GetComponentInParent<Animator>();
        foreach (Light a in l)
        {
            a.GetComponent<Light>().enabled = false;
        }
        foreach (spwanbot a in sbot)
        {
            a.GetComponent<spwanbot>().lo = false;
        }
        //  au_zombiehorde = GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(alert == true)
        {
            time_count += Time.deltaTime;
        }
        if(time_count > 120f)
        {
            au.Stop();
            au_zombiehorde.Stop();
            foreach (spwanbot a in sbot)
            {
                a.GetComponent<spwanbot>().lo = false;
            }
            time_count = 0;
            alert = false;
            this.enabled = false;
        }
        
        if (Camera.main != null)
        {
            screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;

        }
        if (alert == true && au.isPlaying == false)
        {
            if (au.isPlaying == false)
            {
                au.Play();
                ani.SetBool("down", true);
                au_zombiehorde.Play();
                foreach (Light a in l)
                {
                    a.GetComponent<Light>().enabled = true;
                }
                foreach (spwanbot a in sbot)
                {
                    a.GetComponent<spwanbot>().lo = true;
                }
            }
        }

    }


    void OnGUI()
    {
        if (this.GetComponent<Renderer>().isVisible)
        {
            GUI.Label(new Rect(screenPosition.x - 36, screenPosition.y - 35, Screen.width / 8, 7), "PRESS HERE !!!");
            GUI.Box(new Rect(screenPosition.x - 36, screenPosition.y - 80, 120, 30), "PRESS HERE !!!");
        }

    }
    public void audioplay()
    {
        alert = true;
    }
}

