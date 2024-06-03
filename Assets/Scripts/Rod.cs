using Player;
using Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Rod : MonoBehaviour
    {
        public Animator animator;
        public Animator uiAnimator;
        Animator bobberAnimator;
        public PlayerMovement playerMovement;
        public PlayerCamera playerCamera;
        public Transform character;
        public GameObject bobber;
        public UIHandler uiHandler;
        GameObject currentBobber;
        float lastCast = 0f;
        float lastReel = 0f;
        bool bite = false;
        float reelPct = 50;
        float distanceFromWater;
        public Water[] bodiesOfWater;
        public int reelResistance = 2;
        public UnityEvent caughtFish;
        [SerializeField] Tutorial tutorial;
        [SerializeField] Player player;

        void Update()
        {
            CalculateWaterDistance();
            //print(distanceFromWater);
            if (Input.GetMouseButtonDown(0) && playerCamera.cameraMode == CameraMode.ThirdPerson && Time.time - lastCast > 3 && distanceFromWater < 2 && gameObject.activeSelf)
            {
                Cast();
                lastCast = Time.time;
            }
            if (Input.GetMouseButtonDown(1) && playerCamera.cameraMode == CameraMode.Fishing && Time.time - lastCast > 1)
            {
                Uncast();
                playerCamera.cameraMode = CameraMode.ThirdPerson;
                lastCast = Time.time;
            }
            if (Time.time - lastCast > 10 && playerCamera.cameraMode == CameraMode.Fishing && !bite)
            {
                Bite();
            }
            if (bite)
            {
                uiHandler.SetProgress(reelPct / 100);
                if (Time.time-lastReel>.1) //Resistance
                {
                    reelPct -= reelResistance;
                    lastReel = Time.time;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    reelPct += 5;
                }
                if (reelPct <= 0) //Fish Gets Off
                {
                    Uncast();
                    playerCamera.cameraMode = CameraMode.ThirdPerson;
                    return;
                }

                if (reelPct >= 100) { //Fish Reeled In
                    CatchFish();
                    Uncast();
                    return;
                }
            }
        }
        void Cast()
        {
            if (tutorial)
            {
                if (tutorial.scriptStage != 6)
                {
                    return; //Only should be able to cast at stage 5
                }
            }
            character.LookAt(new Vector3(bodiesOfWater[0].transform.position.x,character.position.y, bodiesOfWater[0].transform.position.z));
            animator.SetTrigger("Cast");
            playerMovement.moveSpeed = 0f;
            playerCamera.cameraMode = CameraMode.Fishing;
            playerCamera.animateCamera(character.position+-character.forward+new Vector3(0,4,0), character.rotation * Quaternion.Euler(new Vector3(30,0,0)));
            currentBobber = Instantiate(bobber);
            bobberAnimator = currentBobber.transform.Find("Bobber").GetComponent<Animator>();
            currentBobber.transform.position = character.position + character.forward*10 - new Vector3(0,character.lossyScale.y,0); //10 units in front, at ground height
        }
        void Uncast()
        {
            uiHandler.toggleProgress(false);
            bite = false;
            reelPct = 50;
            animator.SetTrigger("Uncast");
            playerMovement.moveSpeed = 20f;
            Destroy(currentBobber);
        }
        void Bite()
        {
            if (tutorial)
            {
                tutorial.StartNext();
            }
            uiHandler.toggleProgress(true);
            bite = true;
            bobberAnimator.SetBool("Bite", true);
        }
        void CatchFish()
        {
            playerCamera.cameraMode = CameraMode.Scriptable;
            uiHandler.toggleProgress(false);
            bite = false;
            playerMovement.moveSpeed = 20f;
            uiAnimator.SetTrigger("CatchFish");
            caughtFish.Invoke();
            player.currentWeight = 5.3f;
            transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled=false;
        }
        void CalculateWaterDistance()
        {
            distanceFromWater = 6;
            RaycastHit hit;
            LayerMask layerMask = LayerMask.GetMask("Water");
            Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);
            Vector3 to = new Vector3(bodiesOfWater[0].transform.position.x, 0, bodiesOfWater[0].transform.position.z);
            if (Physics.Raycast(from, to - from, out hit, 10.0f, layerMask))
            {
                distanceFromWater = hit.distance;
            }
        }
    }
}