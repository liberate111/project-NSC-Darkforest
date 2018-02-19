using UnityEngine;
using System.Collections;

public class gunny : MonoBehaviour
{

    private float MovementSpeed = 1;
    private float MovementAmount = 2;
    private GameObject Gun;
    private float MovementX;
    private float MovementY;
    private Vector3 newGunPosition;
    private Vector3 DefaultPosition;
    private Quaternion newGunRotation;
    private Quaternion DefaultRotation;

    void Start()
    {
        Gun = gameObject;
        DefaultPosition = transform.localPosition;
        DefaultRotation = transform.rotation;
    }
    void Update()
    {
        MovementX = Input.GetAxis("Mouse X") * Time.deltaTime * MovementAmount;
        MovementY = Input.GetAxis("Mouse Y") * Time.deltaTime * MovementAmount;

        newGunPosition = new Vector3(DefaultPosition.x, DefaultPosition.y, DefaultPosition.z);
        newGunPosition.x = Mathf.Lerp(DefaultPosition.x, DefaultPosition.x + MovementX, MovementSpeed / 2);
        newGunPosition.y = Mathf.Lerp(DefaultPosition.y, DefaultPosition.y + MovementY, MovementSpeed / 2);

        newGunRotation = new Quaternion(DefaultRotation.x, DefaultRotation.y, DefaultRotation.z, 1);
        newGunRotation.z = Mathf.Lerp(DefaultRotation.z, DefaultRotation.z + MovementX, MovementSpeed);

        Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, newGunPosition, MovementSpeed * Time.deltaTime);
        Gun.transform.localRotation = Quaternion.Lerp(Gun.transform.localRotation, newGunRotation, MovementSpeed * Time.deltaTime);
        
    }
}