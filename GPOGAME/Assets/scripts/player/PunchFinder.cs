using Entity1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchFinder : StateMachineBehaviour
{
    private PlayerAttack _playerAttack;
    private bool _has_attacked;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _has_attacked = false;
       _playerAttack = animator.GetComponent<PlayerAttack>();
        _playerAttack.PunchOver = false;
       //Debug.Log("start");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        if (!_has_attacked)
        {
            int i = 0;
            while (i < _playerAttack.Collides)
            {
                var CollidedObj = _playerAttack.Hit_collides[i];
                Entity en = CollidedObj.gameObject.GetComponent<Entity>();
                en.TakeDamage(_playerAttack.Weapon.WeaponData.Damage, en.transform);
                i++;
            }
            _has_attacked = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        _playerAttack.PunchOver = true;
        if (_playerAttack.State.Contains("PUNCH"))
        {
           // animator.Stop(_playerAttack.State, false);
        }

        //Debug.Log("Over");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
