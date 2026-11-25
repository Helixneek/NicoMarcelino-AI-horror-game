using AgateAIHorror.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetTargetIsHiding", story: "Set [TargetIsHiding] from [AI]", category: "Action", id: "bc325ef1dd6c3cb497b2c14bdf4ece93")]
public partial class SetTargetIsHidingAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> TargetIsHiding;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null && AI.Value.TargetPlayer == null) return Status.Failure;

        TargetIsHiding.Value = AI.Value.TargetPlayer.IsHiding;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

