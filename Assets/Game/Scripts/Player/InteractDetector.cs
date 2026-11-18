using UnityEngine;

namespace AgateAIHorror.Player
{
    public class InteractDetector : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private float _detectorDistance = 2f;
        [SerializeField] private Vector3 _detectorBoxSize = Vector3.one;
        [SerializeField] private LayerMask _interactableLayer;

        private IInteractable _currentDetectedInteractable;
        private bool _isInteracting;

        private void Update()
        {
            UpdateDetection();
        }

        private void UpdateDetection()
        {
            if(_isInteracting)
            {
                _isInteracting = false;
                return;
            }

            Transform cameraTransform = Camera.main.transform;

            bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position,
                                                            _detectorBoxSize * 0.5f,
                                                            cameraTransform.forward,
                                                            out RaycastHit hit,
                                                            Quaternion.identity,
                                                            _detectorDistance,
                                                            _interactableLayer);

            if(isDetectingInteractable)
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if(interactable != null)
                {
                    _currentDetectedInteractable = interactable;
                }
            }
            else
            {
                _currentDetectedInteractable = null;
            }
        }

        public void Interact()
        {
            if(_currentDetectedInteractable != null)
            {
                _currentDetectedInteractable.Interact(_player);
                _currentDetectedInteractable = null;
                _isInteracting = true;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Transform cameraTransform = Camera.main.transform;

            bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position,
                                                            _detectorBoxSize * 0.5f,
                                                            cameraTransform.forward,
                                                            out RaycastHit hit,
                                                            Quaternion.identity,
                                                            _detectorDistance,
                                                            _interactableLayer);

            if(isDetectingInteractable)
            {
                Gizmos.color = Color.green;
                // Draw line from camera to the hit point
                Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * hit.distance);
                // Draw cube at the hit point
                Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * hit.distance, _detectorBoxSize);
            }
            else
            {
                // Draw the gizmos for the box cast at the default parameters
                // Draw line from camera to the hit point
                Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * _detectorDistance);
                // Draw cube at the hit point
                Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);
            }
        }
    }
}
