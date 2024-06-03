using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class UIHandler : MonoBehaviour
{
    public Player.Player player;
    public Player.PlayerCamera playerCamera;
    public TMP_Text coins;
    public TMP_Text interactions;
    [SerializeField] RectTransform progressBar;

    private void Update()
    {
        UpdateCoinDisplay();
    }

    void UpdateCoinDisplay()
    {
        coins.text = "Coins: " + player.GetCoins().ToString();
    }

    public void displayInteraction(char key, string name)
    {
        interactions.text = key + " | " + name;
    }
    public void removeInteraction()
    {
        interactions.text = "";
    }
    public void toggleFishDisplay()
    {
        if (Camera.main.transform.position != new Vector3(0, -3, 15)) //When the fish is not displayed...
        {
            playerCamera.cameraMode = CameraMode.Scriptable; //...Display the fish
            Camera.main.transform.position = new Vector3(0, -3, 15);
            Camera.main.transform.rotation = Quaternion.identity;
        }
        else //When the fish IS displayed...
        {
            playerCamera.cameraMode = CameraMode.ThirdPerson; //...Return to third person standard camera
        }
    }
    public void SetProgress(float progress)
    {
        float xPos = 480f * progress - 480;
        progressBar.localScale = new Vector3(progress, 1, 1);
        progressBar.localPosition = new Vector3(xPos, 0, 0);
    }
    public void toggleProgress(bool enabled)
    {
        progressBar.parent.gameObject.SetActive(enabled);
    }
}
