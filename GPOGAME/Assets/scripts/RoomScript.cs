using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

   
    private BoxCollider _trigger;
    private NavMeshSurface _surface;
    private bool _isReady=false;
   public GameObject _eneymys;
    public GameObject _shop;
    public GameObject _treasure;
    public int EnemysNumber;
    public delegate void OnFinish();
    public delegate void OnEnter();
    public OnFinish RoomCleared;
    public OnEnter RoomEnter;
    public int _floorNumber;
    
     
    // Start is called before the first frame update
    void Start()
    {
        _trigger = GetComponent<BoxCollider>();
        _surface = transform.parent.GetChild(0).Find("FloorContainer").GetComponent<NavMeshSurface>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if((EnemysNumber == 0) && (_isReady==true))
        {
            RoomCleared?.Invoke();
            _trigger.enabled = false;
            _surface.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _surface.enabled = true;
            RoomEnter?.Invoke();
        }
    }

   public void EnableEnemys()
    {
        
        _shop.SetActive(!_shop.activeSelf);
        _treasure.SetActive(!_treasure.activeSelf);
        _isReady = !_isReady;

    }

    public void EnableShop()
    {

        EnemysNumber = 0;
        _treasure.SetActive(!_treasure.activeSelf);
        _eneymys.SetActive(!_eneymys.activeSelf);
        _isReady = !_isReady;

    }

    public void EnableTreasure()
    {
        _eneymys.SetActive(!_eneymys.activeSelf);
        _shop.SetActive(!_shop.activeSelf);
        EnemysNumber = 0;
        _isReady = !_isReady;

    }


}
