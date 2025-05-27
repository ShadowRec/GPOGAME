using Entity1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform _target;
    NavMeshAgent _agent;
    Entity _entity;
    RoomScript _room;
    private Animator _animator;
    public float LookRadius;
    private BarrelScript barrelScript;
    // Start is called before the first frame update
    void Start()
    {
        _agent= GetComponent<NavMeshAgent>();
        _target = PlayerManager.instance.player.transform;
        _entity=GetComponent<Entity>();
        _room= transform.parent.transform.parent.transform.parent.GetChild(0).GetComponent<RoomScript>();
        _room.EnemysNumber++;
        _animator = GetComponent<Animator>();
        barrelScript=GetComponent<BarrelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_target.position,transform.position );
        if (distance<=LookRadius && _agent.enabled==true)
        {
            if (barrelScript != null)
            {
                barrelScript.IsWheeling = true;
            }
            if (_animator != null)
            {
                _animator.SetBool("IsMoving", true);
            }

            _agent.SetDestination(_target.position);

            if (distance <= _agent.stoppingDistance)
            {
                Debug.Log("Here");
                LookAtTarget();
            }
        }

        else
        {
            if (_animator != null)
            {
                _animator.SetBool("IsMoving", false);
            }
            if (barrelScript != null)
            {
                barrelScript.IsWheeling = false;
            }
        }

        if (_entity.Health <= 0)
        {
            Destroy(gameObject);
            _room.EnemysNumber--;

        }
    }
    private  void LookAtTarget()
    {
        Vector3 direction= (_target.position - transform.position).normalized;
        Quaternion LookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,LookRotation,Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,LookRadius);
    }
}
