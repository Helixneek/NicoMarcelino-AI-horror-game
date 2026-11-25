using AgateAIHorror.Enemy;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Wait Until Reach Destination", story: "[AI] will wait until they reach destination", category: "Action", id: "955f4c61e5116cef8ca8b2e6d8f58ad9")]
public partial class WaitUntilReachDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<GhostAIController> AI;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Check if the AI controller exists
        if (AI.Value == null) return Status.Failure;

        NavMeshAgent agent = AI.Value.NavMeshAgent;

        // Check if reference to NavMeshAgent exists
        if (agent == null) return Status.Failure;

        // If NavMeshAgent is still calculating the LastSeenPosition
        // That means its status is Running
        // It will not move to the next phase
        if (agent.pathPending) return Status.Running;

        // If the agent's remaining distance is greater than the stopping distance
        // The agent is still moving, thus is still Running
        // It will not move to the next phase
        if (agent.remainingDistance > agent.stoppingDistance + 0.5f) return Status.Running;

        // If there are no issues, that means its a success
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

