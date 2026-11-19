using TMPro;
using UnityEngine;

namespace AgateAIHorror.Player
{
    public class Flashlight : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Light _light;
        [SerializeField] private PlayerCharacter _player;
        [SerializeField] private string _flashlightID;

        [Header("Battery")]
        [SerializeField] private float _initialBatteryLevel = 100f;
        [SerializeField] private float _batteryDrainRate = 1f;

        [Header("Debug")]
        [SerializeField] private TMP_Text _debug_flashlightText;

        private float _batteryLevel;
        public bool HasBattery => _batteryLevel > 0f;

        public bool HasFlashlight => _player.Inventory.CheckItem(_flashlightID);

        private void Awake()
        {
            _batteryLevel = _initialBatteryLevel;
        }

        public void ToggleFlashlight()
        {
            if (HasFlashlight && _light != null)
            {
                if (HasBattery)
                {
                    _light.enabled = !_light.enabled;
                }
                else
                {
                    // Turn off lights if we dont have battery
                    _light.enabled = false;
                }
            }
        }

        private void Update()
        {
            UpdateRotation();
            UpdateBatteryLevel();

            Debug_UpdateFlashlightText();
        }

        private void UpdateRotation()
        {
            _light.transform.rotation = Camera.main.transform.rotation;
        }

        private void UpdateBatteryLevel()
        {
            if (_light != null && _light.enabled)
            {
                if (HasBattery)
                {
                    _batteryLevel -= _batteryDrainRate * Time.deltaTime;
                }
                else
                {
                    _batteryLevel = 0f;
                    _light.enabled = false;
                }
            }
        }

        public void RefillBattery()
        {
            _batteryLevel = _initialBatteryLevel;
        }

        #region DEBUG
        private void Debug_UpdateFlashlightText()
        {
            if (_debug_flashlightText == null) return;
            _debug_flashlightText.SetText(($"Flashlight: {_batteryLevel:F1}/{_initialBatteryLevel:F1}"));
        }
        #endregion
    }

}