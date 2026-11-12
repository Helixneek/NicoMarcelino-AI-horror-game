using TMPro;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _sprintStaminaCost = 20f;
    [SerializeField] private float _staminaRegenRate = 10f;

    [SerializeField] private PlayerCharacterMovement _characterMovement;

    [Header("Debug")]
    [SerializeField] private TMP_Text debug_staminaText;

    private float _currentStamina;

    private void Awake()
    {
        _currentStamina = _maxStamina;
    }

    public void CalculateStamina()
    {
        if (_characterMovement.IsSprinting)
        {
            if (_currentStamina > 0)
            {
                _currentStamina -= _sprintStaminaCost * Time.deltaTime;
            }
            else
            {
                _characterMovement.SetSprint(false);
            }
        }
        else
        {
            // Stamina regen
            _currentStamina += _staminaRegenRate * Time.deltaTime;
        }

        _currentStamina = Mathf.Clamp(_currentStamina, 0, _maxStamina);
    }

    private void Update()
    {
        CalculateStamina();
        Debug_UpdateStaminaText();
    }

    private void Debug_UpdateStaminaText()
    {
        if(debug_staminaText == null) return;
        debug_staminaText.SetText($"Stamina: {_currentStamina:F1}/{_maxStamina:F1}");
    }
}
