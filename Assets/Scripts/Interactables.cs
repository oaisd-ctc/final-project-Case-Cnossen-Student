using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class Interactables : MonoBehaviour
    {
        public KeyCode trigger = KeyCode.E;
        public bool on = true;
        public int maxDistance;
        public string interactionName;
        [SerializeField] UnityEvent onInteract = new UnityEvent();

        public void Interact()
        {
            onInteract.Invoke();
        }
    }
}