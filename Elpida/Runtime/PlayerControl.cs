using UnityEngine;

namespace Elpida
{
    public class PlayerControl : UnitControl
    {
        public override float GetHorizontal() => Input.GetAxis("Horizontal");
        public override float GetVertical() => Input.GetAxis("Vertical");

        public PlayerControl(Elpida.Unit unit) : base(unit)
        {
        }
    }
}