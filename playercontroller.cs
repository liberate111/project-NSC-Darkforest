using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    
    public float speed = 8f;
    private playermotion motion;
    [SerializeField]
    private float lookSensitivity = 0.1f;
	// Use this for initialization
	void Start () {
        motion = GetComponent<playermotion>();
        transform.localPosition = (new Vector3(transform.position.x, -0.5f, transform.position.z));
    }
	
	// Update is called once per frame
	void Update () {
        //movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        motion.Move(_velocity);

        //rotation
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motion.Rotate(_rotation);

        
        float _xRot = Input.GetAxisRaw("Mouse Y");
        //print(_xRot);

        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

        motion.RotateCamera(_cameraRotation);

 
    }
}
