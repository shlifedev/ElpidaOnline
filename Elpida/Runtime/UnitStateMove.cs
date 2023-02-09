namespace Elpida
{
    public class UnitStateMove : UnitStateBase
    {
        public UnitStateMove(Unit unit) : base(unit)
        {
        }
 
        public override void OnEnter()
        {
            base.OnEnter();
            unit.animator.SetBool("move", true); 
        }

        public override void OnLogic()
        {
            base.OnLogic();
            unit.Move(unit.control.GetAxis()); 
        }

        public override void OnExit()
        {
            base.OnExit();
            unit.animator.SetBool("move", false); 
        }
        
    }
}