using System;
using TDS.Input;
using UnityEngine;

namespace TDS.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] InputSO input;

        [Header("Movement Settings")]
        [SerializeField] float walkSpeed;
        [SerializeField] float runSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] float gravityScale = 1f;

        CharacterController controller;
        Animator animator;
        PlayerAim playerAim;

        Vector2 moveInput;
        Vector3 verticalVelocity = Vector3.zero;
        Vector3 lookDirection = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;

        float animMultiplier = 1f;

        float moveSpeed;

        void OnEnable()
        {
            input.OnMovePerformed += Input_OnMovePerformed;

            input.OnSprintPerformed += () =>
            {
                moveSpeed = runSpeed;
                animMultiplier = 2;
            };

            input.OnSprintCancelled += () =>
            {
                moveSpeed = walkSpeed;
                animMultiplier = 1;
            };
        }

        void OnDisable()
        {
            input.OnMovePerformed -= Input_OnMovePerformed;
        }

        void Input_OnMovePerformed(Vector2 moveInput)
        {
            this.moveInput = moveInput;
        }


        void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            playerAim = GetComponent<PlayerAim>();
            moveSpeed = walkSpeed;
        }

        void Update()
        {
            HandleMovement();
            HandleGravity();
            RotateTowardsMousePosition();
            HandleAnimation();
        }

        private void HandleGravity()
        {
            if (controller.isGrounded && verticalVelocity.y < 0)
            {
                verticalVelocity.y = -2f;
            }
            else
            {
                verticalVelocity += gravityScale * Time.deltaTime * Physics.gravity;
            }

            controller.Move(verticalVelocity * Time.deltaTime);
        }

        private void HandleMovement()
        {
            moveDirection = new Vector3(moveInput.x, 0, moveInput.y);

            if (moveDirection.magnitude < 0.1f)
                return;

            controller.Move(moveSpeed * Time.deltaTime * moveDirection);
        }

        void RotateTowardsMousePosition()
        {
            Vector3 mousePosition = playerAim.GetRaycastHitInfo().point;

            lookDirection = mousePosition - transform.position;
            lookDirection.y = 0;
            lookDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection == Vector3.zero ? transform.forward : lookDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        void HandleAnimation()
        {
            float moveX = Vector3.Dot(transform.right, moveDirection);
            float moveZ = Vector3.Dot(transform.forward, moveDirection);

            animator.SetFloat("moveX", moveX * animMultiplier, 0.1f, Time.deltaTime);
            animator.SetFloat("moveZ", moveZ * animMultiplier, 0.1f, Time.deltaTime);
        }

    }
}
