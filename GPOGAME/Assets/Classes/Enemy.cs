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
    public float LookRadius;
    // Start is called before the first frame update
    void Start()
    {
        _agent= GetComponent<NavMeshAgent>();
        _target = PlayerManager.instance.player.transform;
        _entity=GetComponent<Entity>();
          _room= transform.parent.GetChild(0).GetComponent<RoomScript>();
        _room.EnemysNumber++;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_target.position,transform.position );
        if (distance<=LookRadius)
        {
            _agent.SetDestination(_target.position);
            if (distance <= _agent.stoppingDistance)
            {
                Debug.Log("Here");
                LookAtTarget();
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
