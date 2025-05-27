using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData _weapondata;

    private int _id;
    private BoxCollider _collider;
    private Rigidbody _rb;
    private PlayerAttack _attackScript;
    private bool _isAvaiable;

    public void SetWeapon(WeaponData weaponData)
    {
        _weapondata = weaponData;
    }

    public WeaponData WeaponData
    {
        get { return _weapondata; }
    }

    public Queue<Attack> Attacks
    {
        get
        {
            return _weapondata._attacks;
        }
    }

    public WeapomType WeaponType
    {
        get 
        {
            return _weapondata.WeaponType;
        }
    }

    public float AttackSpeed
    {
        get
        {
            return _weapondata.AttackSpeed;
        }
    }

    public float Damage
    {
        get
        {
            return _weapondata.Damage;
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
            _weapondata._attacks = new Queue<Attack>();

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
