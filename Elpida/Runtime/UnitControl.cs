using System;
using FSM;
using Mirror; 
using UnityEngine;
using Elpida;
namespace Elpida
{  
    public abstract class UnitControl : MonoBehaviour
    {
        private readonly Elpida.Unit unit;

        public UnitControl(Elpida.Unit unit)
        {
            this.unit = unit;
        }

        public abstract float GetHorizontal();
        public abstract float GetVertical(); 
        public Vector2 GetAxis() => new Vector2(GetHorizontal(), GetVertical());

        public bool IsMouseDown(int button)
        {
            return Input.GetMouseButtonDown(button);
        }
        public bool IsMouseUp(int button)
        {
            return Input.GetMouseButtonUp(button);
        }
        
    }
}