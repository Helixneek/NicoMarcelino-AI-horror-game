using AgateAIHorror.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set CanSeeTarget", story: "Set [CanSeeTarget] from [AI]", category: "Action", id: "4b0afc6e18ff686ac6537137ba1ac03c")]
public partial class SetCanSeeTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> CanSeeTarget;
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null && AI.Value.SightPerception == null) return Status.Failure;

        CanSeeTarget.Value = AI.Value.SightPerception.CanSeePlayer;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

