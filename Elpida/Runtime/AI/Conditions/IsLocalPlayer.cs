using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Mirror;

namespace Game.Elpida.BT.Conditions
{
    [TaskCategory("Elpida Network/조건")]
    [TaskDescription("로컬 플레이어인지 확인합니다.")] 
    public class IsLocalPlayer : Conditional
    {
        
        private SharedGameObject target;
        private NetworkBehaviour networkBehaviour;

        public override void OnStart()
        { 
            if (target != null) 
                target = this.gameObject;
            if (networkBehaviour == null)
                networkBehaviour = target.Value.GetComponent<NetworkBehaviour>(); 
        }

        public override TaskStatus OnUpdate()
        {
            if (networkBehaviour != null && networkBehaviour.isLocalPlayer)
                return TaskStatus.Success;

            return TaskStatus.Failure;
        }

        public override void OnReset()
        {
            base.OnReset();
            this.target = null;
        }
    }
}