using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity1
{
    public class Entity : MonoBehaviour


    {
        [field: SerializeField] public double Health { get; set; }
        [field: SerializeField] public string Name { get; set; }
        public void TakeDamage(double damage)
        {
            this.Health -= damage;
        }




    }
}
