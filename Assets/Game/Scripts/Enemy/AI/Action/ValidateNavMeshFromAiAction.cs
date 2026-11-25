using AgateAIHorror.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Validate NavMesh from AI", story: "Validate NavMesh from [AI]", category: "Action", id: "1b12dac26e5df6936786000a90b321d6")]
public partial class ValidateNavMeshFromAiAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (AI.Value == null) return Status.Failure;

        if (AI.Value.NavMeshAgent == null) return Status.Failure;

        if (!AI.Value.NavMeshAgent.isActiveAndEnabled) return Status.Failure;

        if (!AI.Value.NavMeshAgent.isOnNavMesh) return Status.Failure;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

