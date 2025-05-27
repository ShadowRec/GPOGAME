using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

namespace Entity1
{
    public class Entity : MonoBehaviour


    {
        [field: SerializeField] public double Health { get; set; }
        [field: SerializeField] public string Name { get; set; }
        private Transform _entityTransform;
        private Rigidbody _rb;
        public float KnockBackForce;
        public bool IsEnemy;
      

        public void TakeDamage(double damage, Transform DamageDealerPosition)
        {
            if (IsEnemy)
            {
                StartCoroutine(EnemyDamage(damage, DamageDealerPosition));
            }
            else
            {
                this.Health -= damage;
                Debug.Log("Damage");
                Vector3 awayFromeEntity = DamageDealerPosition.position - _entityTransform.position;
                _rb.AddForce(awayFromeEntity * KnockBackForce, ForceMode.Impulse);
            }


        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _entityTransform = GetComponent<Transform>();

        }

        private IEnumerator EnemyDamage(double damage, Transform DamageDealerPosition)
        {
            this.Health -= damage;
           
            Debug.Log("DamageEnem");
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            _rb.isKinematic = false;
            _rb.AddForce(-transform.forward*KnockBackForce, ForceMode.Impulse);
            yield return new WaitForSeconds(0.5f);
            yield return new WaitUntil(()=> (_rb.velocity.magnitude <= 0.4f));
            navMeshAgent.enabled = true;
            _rb.isKinematic = true;
        }

    }
}
