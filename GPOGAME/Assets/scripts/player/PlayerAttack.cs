using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity1;
using System.Linq;
using UnityEditorInternal;
public class PlayerAttack : MonoBehaviour
{
    public Transform PlayerPos;
    private float _timeBtwAttack=0;
    public Color Gizmo_color;
    private bool _weaponEquiped = false;
    public Vector3 boxsize;
    public LayerMask CustomLayerMask;
    private bool _isStarted;
    private Weapon _weapon=null;
    private Attack _currentAttack;
    private int _attackIndex=1;
    private PlayerMovement _playerMovement;
    private Rigidbody _rb;
    private Transform _transform;
    bool _isAttacking = false;
    private Animator _animator;
    public RuntimeAnimatorController[] runtimeAnimatorControllers;
    public delegate void AttackEvent();
    public AttackEvent IsAttacking;
    public AttackEvent IsNotAttacking;
    public bool PunchOver;
    private string _state;
    private Collider[] _hit_collides;
    public string State
    {
        get
        {
            return _state;
        }
        set { }
    }

    public Collider[] Hit_collides
    {
        get
        {
            return _hit_collides;
        }
        set { }
    }

    public int Collides
    {
        get
        {
            int collides;
            return collides = Physics.OverlapBoxNonAlloc(gameObject.transform.position + transform.forward * _currentAttack.HitBoxRange, _currentAttack.HitBoxSize, _hit_collides, PlayerPos.rotation, CustomLayerMask) ;
        }
        set { }
    }

    public Weapon Weapon
    {
        get
        {

            return _weapon;
        }
        set { }
    }
    public int AttackIndex
    {
        set
        {
            if (value > 5)
            {
                _attackIndex = 1;
            }
            else
            {
                _attackIndex = value;
            }
        }
        get {
            return _attackIndex;


        }
    }
    //private bool _state;

    public Transform hand;
    private enum _colliderState
    {
        Closed,
        Open,
        Colliding
    }
    private void Start()
    {
        
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _isStarted = true;
       
       
        
        
        
    }
    void Update()
    {

        //if (_timeBtwAttack <= 0 & _weapon != null)
        //{

        //    _timeBtwAttack = _weapon.WeaponData.timeBtwAttack;
        //    Debug.Log("prep");
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _weapon.Attacks.Enqueue(new Attack(AttackIndex / 5, AttackIndex / 10));

                if (!_isAttacking)
                {
                    IsAttacking?.Invoke();
                    StartCoroutine(Attack(_weapon));
                }
            }
        //}
        //else
        //{
        //    _timeBtwAttack -= Time.deltaTime;
        //}



    }

    void OnDrawGizmos()
    {
        if (_isStarted)
        {
            Gizmos.color = Gizmo_color;

            if (_weapon == null)
            {
                Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward, transform.rotation, transform.localScale);
                Gizmos.DrawCube(Vector3.zero, new Vector3(boxsize.x, boxsize.y, boxsize.z));
            }
            else
            {
                if (_currentAttack != null)
                {
                    Gizmos.matrix = Matrix4x4.TRS(gameObject.transform.position + transform.forward * _currentAttack.HitBoxRange + transform.up * (float)1.5, transform.rotation, transform.localScale);
                    Gizmos.DrawCube(Vector3.zero, new Vector3(_currentAttack.HitBoxSize.x*2, _currentAttack.HitBoxSize.y*2, _currentAttack.HitBoxSize.z*2));
                }
            }
            
        }
    }
    public void TakeWeapon(Transform weapon)
    {
        _weaponEquiped = true;
        _weapon = weapon.gameObject.GetComponent<Weapon>();
        _animator.runtimeAnimatorController = runtimeAnimatorControllers[(int)_weapon.WeaponType];
        weapon.SetParent(hand);
        weapon.localPosition= Vector3.zero;
        weapon.localRotation= Quaternion.Euler(0f, 0f, 0f);

    }

    private void StopAttack()
    {
        if(_weapon != null) 
        { 
            
            _currentAttack = null;
            _weapon.Attacks.Clear();
            StopAllCoroutines();
            _isAttacking = false;
            AttackIndex = 1;
            
            _animator.SetBool("IsAttacking", false);
            for (int i= 1; i <= 5;i++)
                {
                    _animator.SetBool("Punch" + i.ToString(), false);
                }
            
            
        
        }

    }

    private IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(4f);
        AttackIndex = 1;
    }
    private IEnumerator Attack(Weapon weapon)
    {
        _isAttacking = true;
        Attack curAttack = _weapon.Attacks.Dequeue();
        _currentAttack = curAttack;
        if (weapon == null)
        {

            yield return new WaitForSeconds(0.1f);
            Collider[] hitColliders = new Collider[10];
            
           
            yield return new WaitForSeconds(0.1f);
        }

        else
        {
            
            _animator.SetBool("IsAttacking", true);
            int thisAtindex = AttackIndex;
            _hit_collides = new Collider[10];
            _animator.SetFloat("AttackSpeed",weapon.AttackSpeed);
            // _animator.SetBool("Punch" + (thisAtindex).ToString(), true);
            PunchOver = false;
            this._state = "Punch" + (thisAtindex).ToString();
            _rb.velocity = _transform.forward * 5f;
           // int collides = Physics.OverlapBoxNonAlloc(gameObject.transform.position + transform.forward * _currentAttack.HitBoxRange, _currentAttack.HitBoxSize, _hit_collides, PlayerPos.rotation, CustomLayerMask);
            // Debug.Log("Punch" + (thisAtindex).ToString());
            _animator.Play("PUNCH" + (thisAtindex).ToString());
            //int i = 0;
            //while (i < collides)
            //{
        
            //        var CollidedObj = hitColliders[i];
            //        Entity en = CollidedObj.gameObject.GetComponent<Entity>();
            //        en.TakeDamage(_weapon.WeaponData.Damage,en.transform);
            //        i++;
            //}
            //Debug.Log(_currentAttack.DelayAfterAttack.ToString());
            yield return new WaitUntil(() => PunchOver);
            //yield return new WaitForSeconds(_currentAttack.DelayAfterAttack);
            AttackIndex++;
            IsNotAttacking?.Invoke();
            _animator.SetBool("IsAttacking", false);
            _isAttacking = false;
        }
    }
   

}
