using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private RoomScript _roomScript;
    private Animator _animator;
  
    private Transform _doorPos;
    // Start is called before the first frame update
    void Start()
    {
        _roomScript= this.transform.parent.transform.parent.GetComponent<RoomScript>();
        _animator= GetComponent<Animator>();
        _roomScript.RoomCleared += OpenDoor;
        
        _roomScript.RoomEnter +=CloseDoor;
        _doorPos=GetComponent<Transform>();
    }

    private void CloseDoor()
    {
        _animator.Play("DoorClose");
        
        
    }

    private void OpenDoor()
    {
        _animator.Play("DoorOpen");
    }
}
