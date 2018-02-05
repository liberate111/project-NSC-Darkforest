using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermotion : MonoBehaviour {
    private Vector3 velocity = Vector3.zero;
    private Rigidbody rb;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    public Camera cam;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    void FixedUpdate()
    {
        //print(cam.transform.rotation.x);
        PerformMovement();
        PerformRotation();
        
       
        
        /*if (cam.transform.localRotation.x<-1){
            cam.transform.localRotation = Quaternion.Euler(-1, 0, 0);
        }*/
    }
   
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
