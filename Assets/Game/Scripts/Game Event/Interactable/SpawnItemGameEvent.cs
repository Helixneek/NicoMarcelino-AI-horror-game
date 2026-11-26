using AgateAIHorror.Player;
using UnityEngine;

namespace AgateAIHorror.Core
{
    public class SpawnItemGameEvent : GameEventBase
    {
        [SerializeField] private GameObject _itemObject;
        [SerializeField] private Vector3 _spawnOffset = new(0,0,0);
        [SerializeField] private bool _isDestroyAfterFinished;

        private GameObject _spawnedObject;

        public override void Trigger()
        {
            Transform playerPos = FindAnyObjectByType<PlayerCharacter>().transform;
            Vector3 targetPos = playerPos.TransformPoint(_spawnOffset);

            if(_itemObject != null && playerPos != null)
            {
                _spawnedObject = Instantiate(_itemObject, targetPos, Quaternion.identity);
            }
            else if(_itemObject != null)
            {
                Debug.LogError("Player character object not found!", this);
            }

            base.Trigger();
        }

        public override void Finish()
        {
            if (_itemObject != null && _spawnedObject != null && _isDestroyAfterFinished)
            {
                Destroy(_spawnedObject);
            }

            base.Finish();
        }
    }
}
