using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class SellFish : MonoBehaviour
    {
        public Player player;
        public PlayerCamera playerCamera;
        public int nextScene;
        [SerializeField] Tutorial tutorial;
        public int calculateValue(double weight)
        {
            return (int)weight * 25;
        }

        public void Sell()
        {
            if (player.currentWeight==0)
            {
                playerCamera.Shake(2f);
                return;
            }
            player.AddCoins(calculateValue(player.currentWeight));
            player.currentWeight = 0;
            if (tutorial)
            {
                tutorial.StartNext();
            }
            SceneManager.LoadScene(nextScene);
        }
    }
}