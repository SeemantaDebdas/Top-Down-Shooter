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
        [SerializeField] float gravityScale = 1f;


        [Header("Rotation Settings")]
        [SerializeField] LayerMask aimLayerMask;
        [SerializeField] Transform aimVisual;

        CharacterController controller;
        Animator animator;

        Vector2 moveInput, aimInput;
        Vector3 verticalVelocity = Vector3.zero;
        Vector3 lookDirection = Vector3.zero;
        Vector3 moveDirection = Vector3.zero;

        float animMultiplier = 1f;

        float moveSpeed;

        void OnEnable()
        {
            input.OnMovePerformed += Input_OnMovePerformed;
            input.OnAimPerformed += Input_OnAimPerformed;

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
            input.OnAimPerformed -= Input_OnAimPerformed;
        }

        void Input_OnMovePerformed(Vector2 moveInput)
        {
            this.moveInput = moveInput;
        }

        void Input_OnAimPerformed(Vector2 aimInput)
        {
            this.aimInput = aimInput;
        }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            moveSpeed = walkSpeed;
        }

        void Update()
        {
            HandleMovement();
            HandleGravity();
            AimAtMouse();
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

        void AimAtMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(aimInput);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, aimLayerMask))
            {
                lookDirection = hitInfo.point - transform.position;
                lookDirection.y = 0;
                lookDirection.Normalize();

                aimVisual.position = new Vector3(hitInfo.point.x, transform.position.y + 1, hitInfo.point.z);
            }

            transform.forward = lookDirection == Vector3.zero ? transform.forward : lookDirection;
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
