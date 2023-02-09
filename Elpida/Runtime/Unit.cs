using System;
using FSM;
using Game;
using Mirror;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elpida
{
    public class Unit : EntityBase
        , IMoveableEntity
    {
        public enum UnitState
        {
            Idle,
            Move,
            Skill
        }
        
        

        public StateMachine<UnitState> fsm;
        public Animator animator;
        public RuntimeAnimatorController AnimatorController => animator.runtimeAnimatorController;
        public Rigidbody2D rigid2D;
        public UnitControl control;

        private void Awake()
        {
            control ??= this.gameObject.AddComponent<PlayerControl>();
            fsm ??= new StateMachine<UnitState>();  
            
            fsm.AddState(UnitState.Idle, new UnitStateIdle(this));
            fsm.AddState(UnitState.Move, new UnitStateMove(this));  
            fsm.AddState(UnitState.Skill, new UnitStateAttack(this));  
            fsm.AddTransition(new Transition<UnitState>(UnitState.Idle, UnitState.Move, tr =>
            {
                if (control.GetAxis() != Vector2.zero)
                    return true;
                else 
                    return false;
            }));
            fsm.AddTransition(new Transition<UnitState>(UnitState.Move, UnitState.Idle, tr =>
            {
                if (control.GetAxis() == Vector2.zero)
                    return true;
                else
                    return false;
            }));
            fsm.SetStartState(UnitState.Idle);  
            fsm.Init();  
        }
 

        public void TeleportTo(Vector2 point)
        { 
            rigid2D.position = point;
        }

     
        /// <summary>
        /// 플레이어 이동, 방향기준 
        /// </summary>
        /// <param name="dir"></param>
        public void Move(Vector2 dir)
        { 
            rigid2D.position += dir * 3 * Time.deltaTime;
        }

        protected override void Update()
        {
            base.Update();
            fsm.OnLogic();
            
            
        }

        public void Attack()
        {
            animator.SetBool("isAttack", true);
        }
    }
}