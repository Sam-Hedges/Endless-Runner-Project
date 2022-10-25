using System;
using Project.Runtime._Scripts.Managers;
using UnityEngine;

namespace Project.Runtime._Scripts.Gameplay.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private TileSpawner tileSpawner;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private Transform dollyCart;
        [SerializeField] private float lerpSpeed = 20f;
        private CharacterController characterController;
        
        [SerializeField] private float rotationFactorPerFrame = 15f;
        [SerializeField, Range(0f, 50f)] private float gravity = 35f;
        [SerializeField, Range(0f, 1f)] private float slide = 0.5f;
        private Vector3 velocity;
        private bool IsGrounded => characterController.isGrounded;
        
        // Start is called before the first frame update
        private void Awake() {
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update() {
            
            HandleMovement();
            Gravity();
        }

        private void HandleMovement() {
            Transform t = transform;

            Vector3 newMoveVector = Vector3.Lerp(t.position, dollyCart.position, slide) - t.position;
            
            Vector3 moveDirection = Vector3.ClampMagnitude(newMoveVector, 1f) * lerpSpeed * Time.deltaTime;

            Quaternion currentRotation = transform.rotation;
            
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            Quaternion newRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
            transform.rotation = newRotation;
            
            characterController.Move(moveDirection);
        }
        
        private void Gravity()
        {

            // Works out terminal velocity of pigeon based on the gravity
            float terminalVelocity = -(Mathf.Sqrt(3f * 900f * gravity / 1.6f * 1200f * 0.4f) / 1000);

            // if the player is not grounded increase the vertical velocity
            if (!IsGrounded) { velocity.y += -gravity * Time.deltaTime; }

            //If the pigeon falls faster than terminal velocity then the fall is clamped
            if (velocity.y < terminalVelocity) { velocity.y = terminalVelocity; }

            // if the player is grounded reset the velocity
            if (IsGrounded && velocity.y < 0f)
            {
                velocity.y = 0f;

            }

            // applies the new velocity vector to the character controller
            characterController.Move(velocity * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider collider) {
            switch (collider.name) {
                case "Right Turn":
                    collider.gameObject.GetComponent<Tile>().direction = Vector3.right;
                    break;
                case "Left Turn":
                    collider.gameObject.GetComponent<Tile>().direction = Vector3.left;
                    break;
                case "Stop Lanes":
                    inputManager.DisableMovementInput();
                    break;
            }
        }

        private void OnTriggerExit(Collider collider) {
            if (collider.name == "Stop Lanes") {
                inputManager.EnableMovementInput();
            }
        }
    }
}
