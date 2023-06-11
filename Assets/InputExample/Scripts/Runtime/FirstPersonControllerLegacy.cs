// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using TestHelper.Input;
using UnityEngine;

namespace InputExample
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonControllerLegacy : MonoBehaviour
    {
        private CharacterController _controller;

        [SerializeField]
        private float moveSpeed = 7.0f;

        [SerializeField]
        private float jumpSpeed = 5.0f;

        [SerializeField]
        private float rotateSpeed = 30.0f;

        private const float Gravity = -9.81f;
        private float _yVelocity = 0f;

        // UnityEngine.Inputの代わりにInputWrapperを使用する
        internal IInput Input { private get; set; } = new InputWrapper();

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            Move();
            Look();
            Fall();
            Jump();
        }

        private void Move()
        {
            var move = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                move = Vector3.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                move = Vector3.left;
            }

            if (Input.GetKey(KeyCode.S))
            {
                move = Vector3.back;
            }

            if (Input.GetKey(KeyCode.D))
            {
                move = Vector3.right;
            }

            var direction = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            _controller.Move(direction * move * (moveSpeed * Time.deltaTime));
        }

        private void Look()
        {
            var input = Input.GetAxis("Mouse X");
            transform.Rotate(0f, input * (rotateSpeed * Time.deltaTime), 0f);
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

        private void Jump()
        {
            if (Input.GetKey(KeyCode.Space) && _yVelocity == 0f)
            {
                _yVelocity += jumpSpeed;
            }
        }
    }
}
