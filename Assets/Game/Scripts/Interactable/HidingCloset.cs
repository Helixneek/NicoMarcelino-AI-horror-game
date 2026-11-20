using AgateAIHorror.Player;
using System.Collections;
using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class HidingCloset : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _name;
        [SerializeField] private Transform _hidePosition;
        [SerializeField] private Transform _unhidePosition;
        [SerializeField] private float _hideAnimationDuration = 1f;
        [SerializeField] private Door _doorObject;

        private PlayerCharacter _hidingPlayer;

        private Coroutine _hideCoroutine, _unhideCoroutine;

        public string Name => _name;

        public void Interact(PlayerCharacter player)
        {
            if (_hidePosition != null && _unhidePosition != null && _doorObject != null)
            {
                _hidingPlayer = player;

                if (_hideCoroutine != null)
                {
                    StopCoroutine(_hideCoroutine);
                }

                _hideCoroutine = StartCoroutine(Hide());
            }
        }

        public void StopHiding()
        {
            if (_hidingPlayer != null)
            {
                if (_unhideCoroutine != null)
                {
                    StopCoroutine(_unhideCoroutine);
                }
                _unhideCoroutine = StartCoroutine(Unhide());
            }
        }

        public IEnumerator Hide()
        {
            _hidingPlayer.SetHiding(true);

            _hidingPlayer.Camera.SetCameraInputEnabled(false);
            _hidingPlayer.Movement.SetEnabled(false);
            _hidingPlayer.InteractDetector.SetEnabled(false);

            //_hidingPlayer.Camera.ResetCameraRotation();

            _doorObject.Open();
            // Wait until door animation is complete
            yield return new WaitWhile(() => _doorObject.IsAnimating);

            float time = 0f;
            Vector3 startPosition = _hidingPlayer.transform.position;
            Quaternion startTransformRotation = _hidingPlayer.transform.rotation;
            float startCameraPan = _hidingPlayer.Camera.PanAxis;

            while (time < _hideAnimationDuration)
            {
                time += Time.deltaTime;
                float t = time / _hideAnimationDuration;

                // Interpolate position
                _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _hidePosition.position, t);
                // Interpolate rotation
                _hidingPlayer.transform.rotation = Quaternion.Slerp(startTransformRotation, _hidePosition.rotation, t);
                float panAxis = Mathf.LerpAngle(startCameraPan, _hidePosition.eulerAngles.y, t);
                _hidingPlayer.Camera.SetPanAxisValue(panAxis);

                yield return null;
            }

            // Set final position and rotation
            _hidingPlayer.transform.position = _hidePosition.position;
            _hidingPlayer.transform.rotation = _hidePosition.rotation;

            _doorObject.Close();

            // Wait until door animation is complete
            yield return new WaitWhile(() => _doorObject.IsAnimating);

            _hidingPlayer.Input.OnInteractInput.AddListener(StopHiding);
        }

        public IEnumerator Unhide()
        {
            _hidingPlayer.Input.OnInteractInput.RemoveListener(StopHiding);

            _doorObject.Open();
            // Wait until door animation is complete
            yield return new WaitWhile(() => _doorObject.IsAnimating);

            float time = 0f;
            Vector3 startPosition = _hidingPlayer.transform.position;
            Quaternion startTransformRotation = _hidingPlayer.transform.rotation;
            float startCameraPan = _hidingPlayer.Camera.PanAxis;

            while (time < _hideAnimationDuration)
            {
                time += Time.deltaTime;
                float t = time / _hideAnimationDuration;

                // Interpolate position
                _hidingPlayer.transform.position = Vector3.Lerp(startPosition, _unhidePosition.position, t);
                // Interpolate rotation
                _hidingPlayer.transform.rotation = Quaternion.Slerp(startTransformRotation, _unhidePosition.rotation, t);
                float panAxis = Mathf.Lerp(startCameraPan, _unhidePosition.eulerAngles.y, time / _hideAnimationDuration);
                _hidingPlayer.Camera.SetPanAxisValue(panAxis);


                yield return null;
            }

            // Set final position and rotation
            _hidingPlayer.transform.position = _unhidePosition.position;
            _hidingPlayer.transform.rotation = _unhidePosition.rotation;

            _doorObject.Close();

            _hidingPlayer.Camera.SetCameraInputEnabled(true);
            _hidingPlayer.Movement.SetEnabled(true);
            _hidingPlayer.InteractDetector.SetEnabled(true);

            _hidingPlayer.SetHiding(false);
            _hidingPlayer = null;

            // Wait until door animation is complete
            yield return new WaitWhile(() => _doorObject.IsAnimating);
        }
    }

}