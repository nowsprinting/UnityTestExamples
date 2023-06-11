// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using InputSystemExample.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystemExample
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        private CharacterController _controller;
        private PlayerInputActions _inputActions;

        [SerializeField]
        private float moveSpeed = 7.0f;

        [SerializeField]
        private float jumpSpeed = 5.0f;

        private const float Gravity = -9.81f;
        private float _yVelocity = 0f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Player.Jump.performed += Jump;
            _inputActions?.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Player.Jump.performed -= Jump;
            _inputActions?.Disable();
        }

        private void OnDestroy()
        {
            _inputActions?.Dispose();
        }

        private void Update()
        {
            Move();
            Look();
            Fall();
        }

        private void Move()
        {
            var input = _inputActions.Player.Move.ReadValue<Vector2>();
            var move = new Vector3(input.x, 0f, input.y);
            var direction = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            _controller.Move(direction * move * (moveSpeed * Time.deltaTime));
        }

        private void Look()
        {
            var input = _inputActions.Player.Look.ReadValue<Vector2>();
            transform.Rotate(0f, input.x, 0f);
        }

        private void Fall()
        {
            _yVelocity += Gravity * Time.deltaTime;
            var collisionFlags = _controller.Move(new Vector3(0f, _yVelocity * Time.deltaTime, 0f));
            if (collisionFlags == CollisionFlags.Below)
            {
                _yVelocity = 0f;
            }
        }

        private void Jump(InputAction.CallbackContext context)
        {
            if (_yVelocity != 0f)
            {
                return;
            }

            _yVelocity += jumpSpeed;
        }
    }
}
