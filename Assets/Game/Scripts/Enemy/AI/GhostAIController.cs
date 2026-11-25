using AgateAIHorror.Player;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace AgateAIHorror.Enemy
{
    public class GhostAIController : MonoBehaviour
    {
        [SerializeField] private BehaviorGraphAgent _behaviorGraphAgent;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private PlayerCharacter _targetPlayer;
        [SerializeField] private SightPerception _sightPerception;

        public BehaviorGraphAgent BehaviorGraphAgent => _behaviorGraphAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public PlayerCharacter TargetPlayer => _targetPlayer;
        public SightPerception SightPerception => _sightPerception;
    }
}
