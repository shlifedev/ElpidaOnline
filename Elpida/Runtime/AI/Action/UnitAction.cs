using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Elpida.BT.Action
{ 
    [TaskCategory("Elpida Unit/액션")]  
    public class UnitAction : BehaviorDesigner.Runtime.Tasks.Action
    {
        protected Elpida.Unit unit; 
        public override void OnStart()
        { 
            unit = this.gameObject.GetComponent<Elpida.Unit>();
        } 
    }
}