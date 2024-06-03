using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    public class GridPlacement : MonoBehaviour
    {
        public static PlayerCamera playerCamera;
        [SerializeField] float gridScale = 2f;
        [SerializeField] Transform placementPlot;
        public static int length = 30;
        public static int width = 30;
        private Vector3 offset = Vector3.zero;
        public PlacementObject placementObject;
        private PlacementObject displayObject;
        public Material placementPossible;
        public Material placementImpossible;

        [SerializeField] public bool[,] grid;
        // Start is called before the first frame update
        void Start()
        {
            grid = new bool[(int)placementPlot.localScale.x,(int)placementPlot.localScale.z];
            offset = CalculateOffset();
            playerCamera = GetComponent<PlayerCamera>();
            for (int i = 0; i < placementPlot.localScale.x; i++)
            {
                for (int j = 0; j < placementPlot.localScale.z; j++)
                {
                    grid[i, j] = true;
                }
            }
        }

        Vector3 CalculateOffset()
        {
            float offsetX = (placementObject.length%2)/2f;
            float offsetZ=(placementObject.width%2)/2f;
            return new Vector3(offsetX,0,offsetZ);
        }

        // Update is called once per frame
        void Update()
        {
            if (playerCamera.cameraMode == CameraMode.Placement)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    playerCamera.cameraMode = CameraMode.ThirdPerson;
                    Destroy(displayObject.gameObject);
                    return;
                }
                if (!displayObject) {
                    displayObject = Instantiate(placementObject);
                }
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                displayObject.gameObject.GetComponent<Renderer>().material = placementPossible;
                float x=0;
                float z=0;
                if (Physics.Raycast(ray, out hit))
                {
                    //Snap to grid with scale
                    //Clamp pos between bounds of plot & Round(pos/scale)*scale
                    x = Mathf.Round(Mathf.Clamp(hit.point.x/gridScale, (placementPlot.position.x-(placementPlot.localScale.x/2-offset.x))/gridScale, (placementPlot.position.x + placementPlot.localScale.x / 2-offset.x)/gridScale))*gridScale+offset.x;
                    z = Mathf.Round(Mathf.Clamp(hit.point.z/gridScale, (placementPlot.position.z - (placementPlot.localScale.z / 2-offset.z))/gridScale, (placementPlot.position.z + placementPlot.localScale.z / 2-offset.z)/gridScale))*gridScale+offset.z;

                    displayObject.gameObject.transform.position = new Vector3(x, displayObject.height, z);
                    setPlacementMaterial(checkPlacement((int)(x + placementPlot.localScale.x/2 -offset.x - placementPlot.position.x), (int)(z + placementPlot.localScale.z/2 - offset.z - placementPlot.position.z)));
                }
                if (Input.GetMouseButtonDown(0))
                {
                    print(tryPlace((int)(x + placementPlot.localScale.x / 2 - offset.x - placementPlot.position.x), (int)(z + placementPlot.localScale.z / 2 - offset.z - placementPlot.position.z)));
                }
            }
        }
        bool checkPlacement(int x, int z)
        {
            int widthModifier = (placementObject.width + 1)%2;
            int lengthModifier = (placementObject.length + 1)%2;
            for (int i = x-placementObject.width/2; i<=x+(placementObject.width/2)-widthModifier;i++)
            {
                if (i < 0 || i >= (int)placementPlot.localScale.x) return false;
                for (int j = z-placementObject.length/2; j <= z + (placementObject.length/2)-widthModifier; j++)
                {
                    if (j < 0 || j >= (int)placementPlot.localScale.z || !grid[i,j]) return false;
                }
            }
            return true;
        }
        void setPlacementMaterial(bool possible)
        {
            if (possible)
            {
                displayObject.gameObject.GetComponent<Renderer>().material = placementPossible;
            }else
            {
                displayObject.gameObject.GetComponent<Renderer>().material = placementImpossible;
            }
        }
        bool tryPlace(int x, int z)
        {
            int widthModifier = (placementObject.width + 1) % 2;
            int lengthModifier = (placementObject.length + 1) % 2;
            if (checkPlacement(x,z)) {
                PlacementObject placed = Instantiate(placementObject);
                placed.gameObject.transform.position = displayObject.transform.position;
                for (int i = x - placementObject.width / 2; i <= x + (placementObject.width/2)-widthModifier; i++)
                {
                    for (int j = z - placementObject.length / 2; j <= z + (placementObject.length/2)-lengthModifier; j++)
                    {
                        grid[i,j]=false;
                    }
                }
                return true;
            }else
            {
                return false;
            }
        }

        void changeObject(PlacementObject newObject)
        {
            Destroy(displayObject.gameObject);
            placementObject= newObject;
            displayObject = Instantiate(newObject.gameObject).GetComponent<PlacementObject>();
            CalculateOffset();
        }

        public Vector3 getPlotPosition()
        {
            return placementPlot.position;
        }
        public Vector3 getPlotSize()
        {
            return placementPlot.localScale;
        }
    }
}