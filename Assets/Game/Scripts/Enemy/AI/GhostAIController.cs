using AgateAIHorror.Player;
using System.Collections;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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

        public UnityEvent OnDespawn;

        public void Despawn()
        {
            StartCoroutine(DespawnAfterEndOfFrame());
        }

        public IEnumerator DespawnAfterEndOfFrame()
        {
            if(_behaviorGraphAgent != null)
            {
                // Set the value in the blackboard
                // Reset value so enemy cant see target upon despawn
                _behaviorGraphAgent.SetVariableValue("CanSeeTarget", false);
                _behaviorGraphAgent.enabled = false;
            }

            if(_navMeshAgent != null && _navMeshAgent.isOnNavMesh)
            {
                _navMeshAgent.ResetPath();
                _navMeshAgent.enabled = false;
            }

            OnDespawn?.Invoke();
            yield return new WaitForEndOfFrame();
            gameObject.SetActive(false);
        }
    }
}
