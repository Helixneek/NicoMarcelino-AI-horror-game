using AgateAIHorror.Player;
using UnityEngine;

namespace AgateAIHorror.Core
{
    public class GameOverEvent : GameEventBase
    {
        [SerializeField] private GameObject _gameOverUI;

        public override void Trigger()
        {
            PlayerCharacter player = FindAnyObjectByType<PlayerCharacter>();

            if(_gameOverUI != null && player != null)
            {
                player.OnDeath();
                _gameOverUI.SetActive(true);
            }

            base.Trigger();
        }

        public override void Finish()
        {
            base.Finish();
        }
    }
}
