using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsWheeling=false;
    private Transform _wheel;
    void Start()
    {
         _wheel = transform.Find("Wheel").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWheeling)
        {
            transform.Rotate(0, 0, 9);
            _wheel.Rotate(Vector3.right*(Time.deltaTime*1000f));
        }
    }
}
