using UnityEngine;

namespace AgateAIHorror.Enemy
{
    public class SightPerception : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _eyePosition;
        [SerializeField] private float _viewDistance = 10f;
        [SerializeField] private float _viewAngle = 70f; // Degrees
        [SerializeField] private LayerMask _targetLayer;

        public bool CanSeePlayer { get; private set; }
        public Vector3 LastSeenPosition { get; private set; }


        private void Update()
        {
            CanSeePlayer = CheckSight();
        }

        public bool CheckSight()
        {
            if(_target == null) return false;

            // Check distance of target from user
            float distance = Vector3.Distance(_eyePosition.position, _target.position);
            if(distance > _viewDistance)
            {
                // Too far away
                return false;
            }

            // Check if target is within user's FOV
            Vector3 dirToTarget = _target.position - _eyePosition.position;
            float angle = Vector3.Angle(_eyePosition.forward, dirToTarget);

            if (angle > _viewAngle * 0.5f)
            {
                // Outside of peripherals
                return false;
            }

            // Check if user has line of sight towards the target
            bool isSeeTarget = Physics.Raycast(_eyePosition.position,
                                                dirToTarget.normalized,
                                                out RaycastHit hit,
                                                _viewDistance,
                                                _targetLayer);

            if(isSeeTarget)
            {
                if(hit.transform == _target)
                {
                    LastSeenPosition = _target.position;
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            if (_eyePosition == null) return;

            Gizmos.color = Color.red;
            bool isSeeTarget = CheckSight();
            if(isSeeTarget)
            {
                Gizmos.color = Color.green;
            }

            // Visualize distance
            Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

            // Visualize FOV
            Vector3 left = Quaternion.Euler(0, -_viewAngle / 2, 0) * _eyePosition.forward;
            Vector3 right = Quaternion.Euler(0, _viewAngle / 2, 0) * _eyePosition.forward;

            Gizmos.DrawRay(_eyePosition.position, left * _viewDistance);
            Gizmos.DrawRay(_eyePosition.position, right * _viewDistance);
        }
    }
}
