using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotation : MonoBehaviour
{
    [SerializeField]
    protected float sensivity = 1.5f;

    [SerializeField]
    protected float smooth = 10;
    [SerializeField]
    protected Transform character;

    protected float xRotation;
    protected float yRotation;

    void Start()
    {
        
    }

    
    void Update()
    {
        yRotation += Input.GetAxis("Mouse X") * sensivity;   
        xRotation -= Input.GetAxis("Mouse Y") * sensivity; 
        xRotation= Mathf.Clamp(xRotation, -90f, 90f );
    }
    protected void Rotate()
    {
        transform.rotation= Quaternion.Lerp(transform.rotation, Quaternion.Euler(xRotation,yRotation,0), Time.deltaTime * smooth);
        character.rotation= Quaternion.Lerp(character.rotation, Quaternion.Euler(0, yRotation, 0), Time.deltaTime * smooth); 
    }
}
