using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Items/Weapon")]
public class WeaponData : ScriptableObject
{
    public int _id;
    public string Name;
    public float Damage;
    public int AttackNumber;
    public Queue<Attack> _attacks;
    public float _distance = 4;
    public WeapomType WeaponType;
    public float timeBtwAttack;
    public float AttackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
