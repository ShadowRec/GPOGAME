using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity1;
using System.Linq;
using UnityEditorInternal;
public class PlayerAttack : MonoBehaviour
{
    public Transform PlayerPos;
    private bool _weaponEquiped = false;
    public Vector3 boxsize;
    public LayerMask CustomLayerMask;
    private bool _isStarted;
    private Weapon _weapon=null;
    private Attack _currentAttack;
    private int _attackIndex=1;
    private PlayerMovement _playerMovement;
    bool _isAttacking = false;
    private Animator _animator;
    public RuntimeAnimatorController[] runtimeAnimatorControllers;

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
    private bool _state;

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
        _isStarted = true;
        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.IsMoving += StopAttack;
        
        
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _weapon.Attacks.Enqueue(new Attack(AttackIndex/5 , AttackIndex/10));
           
            if (!_isAttacking)
            {
                StartCoroutine(Attack(_weapon));
            }
           
           
        }

        
    }

    //void OnDrawGizmos()
    //{
    //    if (_isStarted)
    //    {
    //        Gizmos.color = Color.red;

    //        if (_weapon == null)
    //        {
    //            Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward, transform.rotation, transform.localScale);
    //            Gizmos.DrawCube(Vector3.zero, new Vector3(boxsize.x, boxsize.y, boxsize.z));
    //        }
    //        else
    //        {
    //            if (_currentAttack != null)
    //            {
    //                Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward, transform.rotation, transform.localScale);
    //                Gizmos.DrawCube(Vector3.zero, new Vector3(_currentAttack.HitBoxSize.x, _currentAttack.HitBoxSize.y, _currentAttack.HitBoxSize.z));
    //            }
    //        }
            
    //    }
    //}
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
        Attack first_attack;
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
    private IEnumerator Attack(Weapon weapon)
    {
        _isAttacking = true;
        Attack curAttack = _weapon.Attacks.Dequeue();
        _currentAttack = curAttack;
        if (weapon == null)
        {

            yield return new WaitForSeconds(0.1f);
            Collider[] hitColliders = new Collider[10];

            int collides = Physics.OverlapBoxNonAlloc(gameObject.transform.position + transform.forward, boxsize, hitColliders, PlayerPos.rotation, CustomLayerMask);
            int i = 0;
            while (i < collides)
            {
                var CollidedObj = hitColliders[i];
                Entity en = CollidedObj.gameObject.GetComponent<Entity>();
                en.TakeDamage(5);
                i++;
            }
            yield return new WaitForSeconds(0.1f);
        }

        else
        {
                    int thisAtindex=AttackIndex;
            if (thisAtindex == 1) {
                _animator.SetBool("PunchPrep", true);
                Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).length.ToString());
                yield return new WaitForSeconds(1f);
                _animator.SetBool("PunchPrep", false);
                _animator.SetBool("IsAttacking", true);
            }
                    Collider[] hitColliders = new Collider[10];
                    _animator.SetBool("Punch" + (thisAtindex).ToString(), true);
            _animator.speed = 2f;
                    yield return new WaitForSeconds(0.5f);
                    int collides = Physics.OverlapBoxNonAlloc(gameObject.transform.position + transform.forward * _currentAttack.HitBoxRange, _currentAttack.HitBoxSize, hitColliders, PlayerPos.rotation, CustomLayerMask);
                    Debug.Log("Punch" + (thisAtindex).ToString());
                    _animator.SetBool("Punch" + (thisAtindex).ToString(), false);
            _animator.speed = 1;
            int i = 0;
                    while (i < collides)
                    {
                        var CollidedObj = hitColliders[i];
                        Entity en = CollidedObj.gameObject.GetComponent<Entity>();
                        en.TakeDamage(_weapon.Damage);
                        i++;
                    }

                    yield return new WaitForSeconds(_currentAttack.DelayAfterAttack);
                    Attack first_attack;
                    AttackIndex++;
                    if ((_weapon.Attacks.TryPeek(out first_attack)))
                    {
                        StartCoroutine(Attack(_weapon));
                    }
                    else
                    {
                        AttackIndex = 1;
                _animator.SetBool("IsAttacking", false);
                _isAttacking = false;
                    }
        }
    }

   
}
