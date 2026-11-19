using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt _panTilt;
    [SerializeField] private CinemachineInputAxisController _cameraInput;

    public float PanAxis => _panTilt.PanAxis.Value;

    public void SetCameraInputEnabled(bool isEnabled)
    {
        _cameraInput.enabled = isEnabled;
    }

    public void ResetCameraRotation()
    {
        _panTilt.PanAxis.Value = 0f;
        _panTilt.TiltAxis.Value = 0f;
    }

    public void SetPanAxisValue(float newValue)
    {
        _panTilt.PanAxis.Value = newValue;
    }

    public void SetTiltAxisValue(float newValue)
    {
        _panTilt.TiltAxis.Value = newValue;
    }
}
