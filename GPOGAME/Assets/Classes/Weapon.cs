using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int _id;
    public string Name;
    public float Damage;
    public int AttackNumber;
    protected Queue<Attack> _attacks;
    float _distance = 4;
    private BoxCollider _collider;
    private Rigidbody _rb;
    private PlayerAttack _attackScript;
    private bool _isAvaiable;
    public WeapomType WeaponType;
    public float timeBtwAttack;
    public float AttackSpeed;
    

    public Queue<Attack> Attacks
    {
        get
        {
            return _attacks;
        }
    }
            // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _attackScript = GameObject.Find("CharacterTest").GetComponent<PlayerAttack>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isAvaiable==true)
        {
            _rb.isKinematic = true;
            _collider.enabled = false;
            _attackScript.TakeWeapon(gameObject.transform);
            _attacks = new Queue<Attack>();

        }

    }
  

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isAvaiable= true;
        }
    }
   
   
}
