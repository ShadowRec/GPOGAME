using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

    public int EnemysNumber;
    private BoxCollider _trigger;
    public delegate void OnFinish();
    public delegate void OnEnter();
    public OnFinish RoomCleared;
    public OnEnter RoomEnter;
    
     
    // Start is called before the first frame update
    void Start()
    {
        _trigger = GetComponent<BoxCollider>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(EnemysNumber == 0)
        {
            RoomCleared?.Invoke();
            _trigger.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            RoomEnter?.Invoke();
        }
    }



}
