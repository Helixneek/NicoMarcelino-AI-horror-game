using System.Collections;
using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class RotatingDoor : Door
    {
        // Angle the door is rotated when opened
        [SerializeField] private float _openAngle = 90f;

        // Angle the door is rotated when closed
        [SerializeField] private float _closedAngle = 0f;

        protected Coroutine _doorRotateCoroutine;

        public override void Open()
        {
            if(_doorRotateCoroutine != null)
            {
                StopCoroutine(_doorRotateCoroutine);
            }

            _doorRotateCoroutine = StartCoroutine(RotateDoor(_openAngle));

            base.Open();
        }

        public override void Close()
        {
            if (_doorRotateCoroutine != null)
            {
                StopCoroutine(_doorRotateCoroutine);
            }

            _doorRotateCoroutine = StartCoroutine(RotateDoor(_closedAngle));

            base.Close();
        }

        private IEnumerator RotateDoor(float targetAngle)
        {
            _isAnimating = true;
            float startAngle = _doorTransform.localEulerAngles.y;
            float time = 0f;

            while(time < _animationDuration)
            {
                time += Time.deltaTime;

                float angle = Mathf.LerpAngle(startAngle, targetAngle, time / _animationDuration);
                _doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);

                yield return null;
            }

            _doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);

            _isAnimating = false;
        }
    }

}