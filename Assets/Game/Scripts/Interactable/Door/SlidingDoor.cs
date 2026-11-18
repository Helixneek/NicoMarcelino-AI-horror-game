using System.Collections;
using UnityEngine;

namespace AgateAIHorror.Interactable
{
    public class SlidingDoor : Door
    {
        // Angle the door is rotated when opened
        [SerializeField] private Vector3 _openPosition;

        // Angle the door is rotated when closed
        [SerializeField] private Vector3 _closedPosition;

        protected Coroutine _doorRotateCoroutine;

        public override void Open()
        {
            if (_doorRotateCoroutine != null)
            {
                StopCoroutine(_doorRotateCoroutine);
            }

            _doorRotateCoroutine = StartCoroutine(SlideDoor(_openPosition));

            base.Open();
        }

        public override void Close()
        {
            if (_doorRotateCoroutine != null)
            {
                StopCoroutine(_doorRotateCoroutine);
            }

            _doorRotateCoroutine = StartCoroutine(SlideDoor(_closedPosition));

            base.Close();
        }

        private IEnumerator SlideDoor(Vector3 targetPosition)
        {
            _isAnimating = true;
            Vector3 startPosition = _doorTransform.localPosition;
            float time = 0f;

            while (time < _animationDuration)
            {
                time += Time.deltaTime;

                Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _animationDuration);
                _doorTransform.localPosition = position;

                yield return null;
            }

            _doorTransform.localPosition = targetPosition;

            _isAnimating = false;
        }
    }

}