using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player {
    public class PlayerMovement : MonoBehaviour
    {
        public Transform character;
        public CharacterController characterController;
        public float moveSpeed;
        Vector3 moveDirection=Vector3.zero;
        public PlayerCamera playerCamera;
        Rigidbody rb;

        void Start()
        {
            playerCamera = GetComponent<PlayerCamera>();
            rb=GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (playerCamera.cameraMode == CameraMode.ThirdPerson)
            {
                getMoveInput();
                character.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
                characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
            }
        }

        void getMoveInput()
        {
            float forwardInput = Input.GetAxis("Vertical");
            float rightInput = Input.GetAxis("Horizontal");

            Vector3 forward = character.forward;
            Vector3 right = character.right;
            forward.y = 0;
            right.y = 0;

            forward.Normalize();
            right.Normalize();

            moveDirection = ((forward * forwardInput) + (right * rightInput) + (Vector3.down*12));
        }
    }
}