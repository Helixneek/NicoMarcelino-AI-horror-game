using UnityEngine;
using System.Collections;

namespace AgateAIHorror.Enemy
{
    public class GhostSpawner : MonoBehaviour
    {
        [SerializeField] private GhostAIController _ghostAIController;
        [SerializeField] private float _minSpawnDelay = 5f;
        [SerializeField] private float _maxSpawnDelay = 8f;
        [SerializeField] private float _minSpawnDistance = 3f;
        [SerializeField] private float _maxSpawnDistance = 5f;

        private Coroutine _spawnCoroutine;

        public void RestartSpawn()
        {
            if(_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }

            _spawnCoroutine = StartCoroutine(StartSpawn());
        }

        public IEnumerator StartSpawn()
        {
            float spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            yield return new WaitForSeconds(spawnDelay);

            if(_ghostAIController.TargetPlayer == null || _ghostAIController.TargetPlayer.IsHiding)
            {
                RestartSpawn();
                yield break;
            }

            SpawnGhost();
        }

        public void SpawnGhost()
        {
            // Spawn ghost behind player with a random distance
            float spawnDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
            Vector3 spawnPos = _ghostAIController.TargetPlayer.transform.position - _ghostAIController.TargetPlayer.transform.forward * spawnDistance;
            spawnPos.y = _ghostAIController.TargetPlayer.transform.position.y;

            _ghostAIController.NavMeshAgent.enabled = true;
            _ghostAIController.NavMeshAgent.Warp(spawnPos);
            _ghostAIController.transform.LookAt(_ghostAIController.TargetPlayer.transform);

            _ghostAIController.gameObject.SetActive(true);

            // Reset blackboard value for last seen position
            _ghostAIController.BehaviorGraphAgent.SetVariableValue("LastSeenPosition", _ghostAIController.TargetPlayer.transform.position);
            _ghostAIController.BehaviorGraphAgent.enabled = true;

        }
    }
}
