using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static GameInput;

public class InputManager : MonoBehaviour, IPlayerActions
{
    private GameInput _inputAction;
    
    public UnityEvent<Vector2> OnMoveInput;

    private void Awake() {
        _inputAction = new GameInput();
        _inputAction.Enable();
        _inputAction.Player.Enable();

        _inputAction.Player.SetCallbacks(this);
    }

    public void OnInteract(InputAction.CallbackContext context) {
        if(context.performed) {
            Debug.Log("Interact");
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }
}
