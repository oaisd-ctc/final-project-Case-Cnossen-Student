using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Fly : MonoBehaviour
    {
        Animator animator;
        Rigidbody rb;
        PlayerCamera cameraSettings;

        public bool canFly = true;
        // Start is called before the first frame update
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            cameraSettings = gameObject.GetComponent<PlayerCamera>();
            rb = gameObject.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && cameraSettings.cameraMode == CameraMode.Flying && canFly)
            {
                animator.SetTrigger("FlapWings");
                rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            }
        }
    }
}