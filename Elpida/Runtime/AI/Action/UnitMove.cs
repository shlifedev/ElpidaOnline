using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Elpida.BT.Action
{ 
    [TaskDescription("Elpida 유닛 객체를 목표 지점까지 이동시킵니다. ")] 
    public class MoveTo : UnitAction
    { 
        public SharedVector2 position;
        public SharedFloat minDist = 0.1f; 
        public override void OnStart()
        {
            base.OnStart(); 
        }

        public override TaskStatus OnUpdate()
        {
            if (unit == null) 
                return TaskStatus.Failure;

            var unitPosition = new Vector2(unit.transform.position.x, unit.transform.position.y);
           
            var dist = Vector2.Distance(unitPosition, this.position.Value);
            if (dist < minDist.Value) 
                return TaskStatus.Success;

            var dir = position.Value - unitPosition;
                dir.Normalize();
            
            this.unit.Move(dir); 
            return TaskStatus.Running;
        }
    }
}