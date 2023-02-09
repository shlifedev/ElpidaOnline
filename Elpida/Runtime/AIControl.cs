using UnityEngine;

namespace Elpida
{
    public class AIControl : UnitControl
    {        
        public override float GetHorizontal() => Input.GetAxis("Horizontal"); 
        public override float GetVertical() => Input.GetAxis("Vertical");

        public AIControl(Elpida.Unit unit) : base(unit)
        {
        }
    }
}