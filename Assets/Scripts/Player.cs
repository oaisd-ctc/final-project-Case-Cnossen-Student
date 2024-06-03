using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.Rendering;

namespace Player {
    public enum CameraMode
    {
        ThirdPerson,
        Placement,
        Fishing,
        Scriptable,
        Flying
    }
    public class Player : MonoBehaviour {
        Collider playerCollider;
        private int Coins;
        UnityEvent coinChange;
        public UIHandler uiHandler;

        [SerializeField] Tutorial tutorial;

        //Player Components
        PlayerCamera playerCamera;
        Fly flyScript;
        CharacterController characterController;
        Rigidbody rb;
        public float currentWeight=0;

        private void Start()
        {
            //Set variables
            flyScript = GetComponent<Fly>();
            playerCamera = GetComponent<PlayerCamera>();
            playerCollider = GetComponent<Collider>();
            characterController = GetComponent<CharacterController>();
            rb = GetComponent<Rigidbody>();
            //Coin Data Persistence
            if (coinChange == null)
            {
                coinChange = new UnityEvent();
            }
            Coins=LoadCoins(); //Retrieve Coins from PlayerPrefs
            coinChange.Invoke();
            if (coinChange == null)
            {
                coinChange = new UnityEvent();
            }
        }
        void Update()
        {
            //Check for Interactable Objects in Focus
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Interactables>())
                {
                    Interactables interaction = hit.transform.gameObject.GetComponent<Interactables>();
                    if ((Camera.main.transform.position - hit.transform.position).magnitude < interaction.maxDistance)
                    {
                        uiHandler.displayInteraction(((char)interaction.trigger), interaction.interactionName);
                        if (Input.GetKeyDown(interaction.trigger) && interaction.on)
                        {
                            interaction.Interact();
                            uiHandler.removeInteraction();
                        };
                    }
                } else
                {
                    uiHandler.removeInteraction();
                }
            }
        }
        public void enterFlight(Collider collision)
        {
            rb.isKinematic = false;
            if (tutorial)
            {
                if (tutorial.scriptStage < 2) { return; } //Prevent bugs
            }
            if (playerCamera.cameraMode == CameraMode.ThirdPerson) //Start Flying
            {
                flyScript.canFly = true;
                playerCamera.cameraMode = CameraMode.Flying;
                characterController.enabled = false;
                transform.rotation = Quaternion.LookRotation(collision.gameObject.transform.Find("Start").transform.localPosition / 2) * Quaternion.Euler(90, 180, 180);
                rb.freezeRotation = true;
                transform.position = collision.gameObject.transform.Find("Start").transform.position + new Vector3(0, 1, 0);
                rb.velocity = new Vector3(10, 0, 0) * (collision.gameObject.transform.Find("Start").transform.localPosition.x / 2);
            }
            else if (playerCamera.cameraMode == CameraMode.Flying) //Stop Flying
            {
                if (tutorial) //Move onto next step if we're in the tutorial
                {
                    tutorial.StartNext();
                }
                transform.position = collision.gameObject.transform.Find("End").transform.position + new Vector3(0, 1, 0);
                playerCamera.cameraMode = CameraMode.ThirdPerson;
                characterController.enabled = true;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Pipes"))
            {
                playerCollider.isTrigger = true;
                flyScript.canFly = false;

            }
            if (collision.gameObject.CompareTag("RestartObject"))
            {
                playerCollider.isTrigger = false;
                characterController.enabled = false;
                playerCamera.cameraMode = CameraMode.ThirdPerson;
                transform.position = new Vector3(0,2,0);
                transform.transform.rotation = Quaternion.identity;
                characterController.enabled = true;
                currentWeight = 0;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("FlightBar"))
            {
                enterFlight(other);
            }
            if (other.gameObject.CompareTag("RestartObject"))
            {
                playerCollider.isTrigger = false;
                characterController.enabled = false;
                transform.position = new Vector3(0, 2, 0);
                transform.transform.rotation = Quaternion.identity;
                characterController.enabled = true;
                playerCamera.cameraMode = CameraMode.ThirdPerson;
                currentWeight = 0;
            }
        }

        private void OnDestroy()
        {
            SaveCoins(); //Store Coins to PlayerPrefs
        }
        public void AddCoins(int amount) { Coins += amount; coinChange.Invoke(); }
        public void RemoveCoins(int amount) {Coins -= amount; coinChange.Invoke(); }
        public int GetCoins() { return Coins; }
        public int LoadCoins()
        {
            return PlayerPrefs.GetInt("Coins",0);
        }
        public void SaveCoins() 
        {
            PlayerPrefs.SetInt("Coins", Coins);
        }
    }
}