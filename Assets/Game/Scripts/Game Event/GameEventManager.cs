using System.Collections.Generic;
using UnityEngine;

namespace AgateAIHorror.Core
{
    public class GameEventManager : MonoBehaviour
    {
        private static GameEventManager _instance;
        public static GameEventManager Instance => _instance;

        // Key: Id string of game event
        // Value: GameEventBase associated
        private Dictionary<string, GameEventBase> _gameEvents = new();

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public void Register(GameEventBase gameEvent)
        {
            if(!_gameEvents.ContainsKey(gameEvent.ID))
            {
                _gameEvents.Add(gameEvent.ID, gameEvent);
            }
        }

        public void Unregister(GameEventBase gameEvent)
        {
            if(_gameEvents.ContainsKey(gameEvent.ID))
            {
                _gameEvents.Remove(gameEvent.ID);
            }
        }

        public void Trigger(string id)
        {
            bool isGameEventFound = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
            if(isGameEventFound)
            {
                gameEvent.Trigger();
            }
        }

        public void FinishEvent(string id)
        {
            bool isGameEventFound = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
            if (isGameEventFound)
            {
                gameEvent.Finish();
            }
        }
    }
}
