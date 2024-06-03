using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tween;

namespace Player {
    public class PlayerCamera : MonoBehaviour
    {

        public Transform character;
        public float cameraSensitivity;
        public Vector3 cameraOffset = new Vector3(1,1,-7);
        float xAxis=0;
        float yAxis=0;
        float shakeEnd;
        Vector3 mousePosition;
        private GridPlacement gridPlacement;
        Vector3 placementPosition = new Vector3(0,10,0);
        Vector3 plotSize;
        bool panning = false;
        public CameraMode cameraMode;
        Tween.Tween cameraTween;
        [SerializeField] Transform rod;
        void Start()
        {
            //Lock Cursor for Third Person
            Cursor.lockState = CursorLockMode.Locked;

            //Initiate Grid Placement Variables for Placement mode
            //gridPlacement = GetComponent<GridPlacement>();
            //placementPosition = gridPlacement.getPlotPosition() + placementPosition;
            //plotSize = gridPlacement.getPlotSize();
            //plotSize = gridPlacement.getPlotSize();

            //Create Tween Object for Fishing Mode
            cameraTween = new Tween.Tween(Camera.main.transform,1,Tween.EasingStyle.Back,Tween.EasingDirection.Out);
        }
        void LateUpdate()
        {
            switch (cameraMode)
            {
                case CameraMode.ThirdPerson:
                    Cursor.lockState = CursorLockMode.Locked;
                    zoomThirdPerson();
                    rotateCamera();
                    break;
                case CameraMode.Placement:
                    Cursor.lockState = CursorLockMode.None;
                    setCamera();
                    zoomCamera();
                    panCamera();
                    break;
                case CameraMode.Fishing:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
                case CameraMode.Scriptable:
                    Cursor.lockState= CursorLockMode.None;
                    break;
                case CameraMode.Flying:
                    Cursor.lockState = CursorLockMode.Locked;
                    setFlyCam();
                    break;
                default:
                    break;
            }
        }
        void Update() {
            if (Time.time < shakeEnd) { //Shake Camera
                Camera.main.transform.position += new Vector3(Random.Range(-.1f,.1f),Random.Range(-.1f,.1f),Random.Range(-.1f,.1f));
            }
            cameraTween.TweenFrame(); //Place in update loop for all tween objects, if not in an animation, nothing will happen.
        }
        void rotateCamera()
        {
            float mouseX = -Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            yAxis -= mouseX * Time.deltaTime * cameraSensitivity;
            xAxis -= mouseY * Time.deltaTime * cameraSensitivity;
            xAxis = Mathf.Clamp(xAxis, -15, 60);
            Vector3 angleOffset = Quaternion.Euler(xAxis, yAxis, 0) * cameraOffset;
            Camera.main.transform.position = character.position + angleOffset;
            Camera.main.transform.rotation = Quaternion.Euler(xAxis, yAxis, 0);
        }
        void setFlyCam()
        {
            Camera.main.transform.position = character.position + new Vector3(0, 0,-10);
            Camera.main.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
        }
        void zoomThirdPerson()
        {
            float zoomMagnitude = cameraOffset.magnitude;
            zoomMagnitude = Mathf.Clamp(zoomMagnitude - Input.mouseScrollDelta.y * 300 * Time.deltaTime,4.5f,7.5f);
            cameraOffset = cameraOffset.normalized * zoomMagnitude;
        }
        void setCamera() {
            if (!panning)
            {
                Camera.main.transform.rotation = Quaternion.Euler(90, 0, 0);
                Camera.main.transform.position = placementPosition;
            }        
        }
        void panCamera()
        {
            if (Input.GetMouseButton(2))
            {
                if (!panning)
                {
                    mousePosition = Input.mousePosition;
                    panning = true;
                }
                else
                {
                    Vector3 newPos = placementPosition + new Vector3(-(Input.mousePosition.x - mousePosition.x)*.05f, 0, -(Input.mousePosition.y - mousePosition.y)*.05f);
                    Camera.main.transform.position = new Vector3(Mathf.Clamp(newPos.x, placementPosition.x-plotSize.x/2,placementPosition.x+plotSize.x/2),Camera.main.transform.position.y,
                        Mathf.Clamp(newPos.z,placementPosition.z-plotSize.z/2,placementPosition.z+plotSize.z/2));
                }
            }
            else
            {
                placementPosition = Camera.main.transform.position;
                panning = false;
            }        
        }
        void zoomCamera()
        {
            Vector3 newPosition = Camera.main.transform.position - new Vector3(0, Input.mouseScrollDelta.y * 100 * Time.deltaTime, 0);
            newPosition = new Vector3(newPosition.x, Mathf.Clamp(newPosition.y, 7.5f, 20), newPosition.z);
            Camera.main.transform.position = newPosition;
        }
        //Shake Camera
        public void Shake(float duration) {
            shakeEnd = Time.time+duration;
        }

        public void enterPlacement()
        {
            cameraMode = CameraMode.Scriptable;
            cameraMode = CameraMode.Placement;
        }

        public void animateCamera(Vector3 position, Quaternion rotation)
        {
            cameraTween.TweenPositionRotation(position, rotation);
        }
    }
}