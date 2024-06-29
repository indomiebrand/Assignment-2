using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CameraLock : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private const float _threshold = 0.01f;
    private float _cinemachineTargetPitch;
    private PlayerInput _playerInput;
    private float _rotationVelocity;


    [Tooltip("Rotation speed of the character")]
    public float RotationSpeed = 1.0f;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;

    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;


    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }
    private void CameraRotation()
    {
        // if there is an input
        if (_input.look.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetPitch -= _input.look.y * RotationSpeed * deltaTimeMultiplier;
            _rotationVelocity += _input.look.x * RotationSpeed * deltaTimeMultiplier;

            // clamp our pitch rotation
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);
            _rotationVelocity = ClampAngle(_rotationVelocity, BottomClamp, TopClamp);
            // Update Cinemachine camera target pitch
            CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

            // rotate the player left and right
            transform.localRotation = Quaternion.Euler(0.0f, _rotationVelocity, 0.0f);
        }
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void Update()
    {
        CameraRotation();
    }

    private void Start()
    {
        
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
    }


}
