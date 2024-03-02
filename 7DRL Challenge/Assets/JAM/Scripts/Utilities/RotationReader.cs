#if FROM_OTHER_JAM
using BossRushJam2024.Entities;
using JAM.Player.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BossRushJam2024
{
    public class RotationReader : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private PlayerMovementController playerMovement;
        private DeviceType _currentDeviceType;
        private Camera _cam;

        private void Start()
        {
            _cam = Camera.main;
            
            player.PlayerInputs.Player.LookMouse.performed += SetMouseRotate;
            // player.PlayerInputs.Player.LookGamepad.performed += SetJoystickRotate;
        }

        private void Update()
        {
            if (_currentDeviceType is DeviceType.Joystick)
            {
                RotateWithJoystick();
            }
            else
            {
                RotateWithMouse();
            }
        }

        private void SetJoystickRotate(InputAction.CallbackContext cb)
        {
            _currentDeviceType = DeviceType.Joystick;
        }

        private void SetMouseRotate(InputAction.CallbackContext cb)
        {
            _currentDeviceType = DeviceType.Mouse;
        }

        private void RotateWithMouse()
        {
            var ray = _cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            var mousePosition3D = Vector3.zero;
            if (Physics.Raycast(ray, out var hit, 100, 1 << 7))
            {
                var pos = new Vector3(hit.point.x, 0, hit.point.z);
                mousePosition3D = pos;
            }
            
            // Calculate the rotation needed using Quaternion.LookRotation
            var targetRotation = Quaternion.LookRotation(mousePosition3D - transform.position, Vector3.up);

            // Determine the angle between current forward and target direction
            var rotation = transform.rotation;
            var angle = Quaternion.Angle(rotation, targetRotation);

            // Use Quaternion.RotateTowards to smoothly rotate towards the target rotation
            rotation = Quaternion.RotateTowards(rotation, targetRotation,
                Mathf.Min(playerMovement.RotationSpeed * Time.deltaTime, angle));
            transform.rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y, 0));
        }

        private void RotateWithJoystick()
        {
            transform.forward = playerMovement.Movement;
        }
    }

    public enum DeviceType
    {
        Mouse,
        Joystick
    }
}
#endif