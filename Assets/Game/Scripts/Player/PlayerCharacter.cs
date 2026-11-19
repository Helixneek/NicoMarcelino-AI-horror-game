using AgateAIHorror.Core;
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
        [SerializeField] private CameraManager _camera;
        [SerializeField] private InputManager _input;
        [SerializeField] private Flashlight _flashlight;

        public PlayerCharacterMovement Movement => _movement;
        public PlayerStamina Stamina => _stamina;
        public InventoryManager Inventory => _inventory;
        public InteractDetector InteractDetector => _interactDetector;
        public CameraManager Camera => _camera;
        public InputManager Input => _input;
        public Flashlight Flashlight => _flashlight;

        public bool IsHiding { get; private set; }

        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void SetHiding(bool isHiding)
        {
            IsHiding = isHiding;
        }
    }

}
