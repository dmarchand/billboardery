
using Dan.Player;
using Dan.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Dan.PlayerInput {
    public class PlayerInputHandler : MonoBehaviour {
        private GameService gameService;
        private PlayerMovementController playerMovementController;

        [HideInInspector]
        public Vector3 MoveVector;

        private void Start() {
            gameService = GameService.Instance;
            playerMovementController = gameService.PlayerGameObject.GetComponent<PlayerMovementController>();
        }

        public void Fire(InputAction.CallbackContext context) {
            Debug.Log("Fire!");
        }

        public void OnMove(InputAction.CallbackContext callbackContext) {
            Vector2 inputVector = callbackContext.ReadValue<Vector2>();
            MoveVector = new Vector3(inputVector.x, inputVector.y, 0);
            if (playerMovementController.gameObject.activeInHierarchy) {
                playerMovementController.OnMove(callbackContext);
            }
        }

        public void MoveUp(InputAction.CallbackContext context) {
            playerMovementController.MoveUp(context);
        }

        public void MoveLeft(InputAction.CallbackContext context) {
            playerMovementController.MoveLeft(context);
        }

        public void MoveRight(InputAction.CallbackContext context) {
            playerMovementController.MoveRight(context);
        }

        public void MoveDown(InputAction.CallbackContext context) {
            playerMovementController.MoveDown(context);
        }
    }
}
