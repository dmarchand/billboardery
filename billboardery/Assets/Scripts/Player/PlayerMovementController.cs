using Dan.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dan.Player {
    public class PlayerMovementController : MonoBehaviour {
        public float Speed;

        private Animator animator;
        private Vector3 movementVector = new Vector3(0, 0, 0);
        private MoveDirection lastMovedDirection = MoveDirection.Up;
        private GameService gameService;
        private Camera mainCamera;

        private List<MoveDirection> moveDirectionStack = new List<MoveDirection>();

        private bool holdingLeft, holdingRight, holdingUp, holdingDown;

        // Start is called before the first frame update
        void Start() {
            animator = GetComponent<Animator>();

            animator.SetFloat("moveX", movementVector.x);
            animator.SetFloat("moveY", 1);

            gameService = GameService.Instance;
            mainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update() {

        }

        public void MoveUp(InputAction.CallbackContext context) {
            if (context.started) {
            } else if (context.canceled) {
                moveDirectionStack.Remove(MoveDirection.Up);
            } else {
                moveDirectionStack.Insert(0, MoveDirection.Up);
            }
        }

        public void MoveDown(InputAction.CallbackContext context) {
            if (context.started) {
            } else if (context.canceled) {
                moveDirectionStack.Remove(MoveDirection.Down);
            } else {
                moveDirectionStack.Insert(0, MoveDirection.Down);
            }
        }

        public void MoveLeft(InputAction.CallbackContext context) {
            if (context.started) {
            } else if (context.canceled) {
                moveDirectionStack.Remove(MoveDirection.Left);
            } else {
                moveDirectionStack.Insert(0, MoveDirection.Left);
            }
        }

        public void MoveRight(InputAction.CallbackContext context) {
            if (context.started) {
            } else if (context.canceled) {
                moveDirectionStack.Remove(MoveDirection.Right);
            } else {
                moveDirectionStack.Insert(0, MoveDirection.Right);
            }
        }

        private void UpdateAnimationState() {
            if (moveDirectionStack.Count > 0) {
                MoveDirection moveDirection = moveDirectionStack[0];

                if (moveDirection == MoveDirection.Up) {
                    animator.SetFloat("moveY", 1);
                    animator.SetFloat("moveX", 0);
                } else if (moveDirection == MoveDirection.Down) {
                    animator.SetFloat("moveY", -1);
                    animator.SetFloat("moveX", 0);
                } else if (moveDirection == MoveDirection.Right) {
                    animator.SetFloat("moveY", 0);
                    animator.SetFloat("moveX", 1);
                } else if (moveDirection == MoveDirection.Left) {
                    animator.SetFloat("moveY", 0);
                    animator.SetFloat("moveX", -1);
                }
            }
        }

        public void ForceAxisReset() {
            if ((movementVector.x != 0 && movementVector.y != 0) || (movementVector.x == 0 && movementVector.y == 0)) { // No need to force a reset on a diag, I think
                return;
            }

            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", 0);

            if (movementVector.x > 0) {
                animator.SetFloat("moveX", 1);
                holdingRight = true;
                lastMovedDirection = MoveDirection.Right;
            } else if (movementVector.x < 0) {
                animator.SetFloat("moveX", -1);
                lastMovedDirection = MoveDirection.Left;
                holdingLeft = true;
            } else if (movementVector.y > 0) {
                animator.SetFloat("moveY", 1);
                lastMovedDirection = MoveDirection.Up;
                holdingUp = true;
            } else if (movementVector.y < 0) {
                animator.SetFloat("moveY", -1);
                lastMovedDirection = MoveDirection.Down;
                holdingDown = true;
            }
        }

        private void FixedUpdate() {
            if (movementVector != Vector3.zero) {
                UpdateAnimationState();
                Vector3 modifiedMovement = Quaternion.AngleAxis(mainCamera.transform.rotation.eulerAngles.y, Vector3.up) * movementVector;
                transform.position += ((Vector3)modifiedMovement * Speed * Time.deltaTime);
                animator.SetBool("moving", true);
            } else {
                animator.SetBool("moving", false);
            }
        }

        public void OnMove(InputAction.CallbackContext context) {
            var movement2 = context.ReadValue<Vector2>();

            movementVector = transform.forward * movement2.y + transform.right * movement2.x;
        }

        public enum MoveDirection {
            Up,
            Right,
            Down,
            Left
        }
    }
}
