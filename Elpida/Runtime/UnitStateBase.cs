using FSM;
using UnityEngine;

namespace Elpida
{
    public class UnitStateBase : State<Unit.UnitState>
    {
        private bool _debug = false;
        public Unit unit;

        public UnitStateBase(Unit unit)
        {
            this.unit = unit;
        }

        public override void OnEnter()
        {
            if (_debug)
                Debug.Log($"{this.GetType().Name} enter");
            base.OnEnter();
        }

        public override void OnExit()
        {
            if (_debug)
                Debug.Log($"{this.GetType().Name} exit");
            base.OnExit();
        }

        public override void OnLogic()
        {
            if (_debug)
                Debug.Log($"{this.GetType().Name} logic");
            base.OnLogic();
        }
    }

    public class UnitStateAttack : UnitStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnLogic()
        {
            base.OnLogic(); 
        }

        public override void OnExit()
        {
            base.OnExit();
        }

        public UnitStateAttack(Unit unit) : base(unit)
        {
        }
    }
}