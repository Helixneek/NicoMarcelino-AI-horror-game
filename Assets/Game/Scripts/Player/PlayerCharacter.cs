using UnityEngine;

namespace AgateAIHorror.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Header("Player Components")]
        [SerializeField] private PlayerCharacterMovement _movement;
        [SerializeField] private PlayerStamina _stamina;
        [SerializeField] private InventoryManager _inventory;
        [SerializeField] private InteractDetector _interactDetector;

        public PlayerCharacterMovement Movement => _movement;
        public PlayerStamina Stamina => _stamina;
        public InventoryManager Inventory => _inventory;
        public InteractDetector InteractDetector => _interactDetector;

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
