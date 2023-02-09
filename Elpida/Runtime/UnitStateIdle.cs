namespace Elpida
{
    public class UnitStateIdle : UnitStateBase
    {
        public override void OnEnter()
        {
            base.OnEnter();
            unit.animator.SetBool("idle", true);  
        }

        public override void OnLogic()
        {
            base.OnLogic();
        }

        public override void OnExit()
        {
            base.OnExit();
            unit.animator.SetBool("idle", false); 
        }

        public UnitStateIdle(Unit unit) : base(unit)
        {
        }
    }
    
    
}