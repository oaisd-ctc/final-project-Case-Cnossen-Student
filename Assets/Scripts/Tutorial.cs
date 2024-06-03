using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Player
{
    public class Tutorial : MonoBehaviour
    {
        public PlayerCamera playerCamera;
        public PlayerMovement playerMovement;
        public Animator uiAnimator;
        public GameObject rod;
        public UITypeWrite tutorialText;
        public Button nextButton;
        public Transform store;
        public GameObject direction;
        UnityEvent tutorialEvent;
        UnityEvent caughtEvent;
        Tween.Tween tutorialTween;
        Tween.Tween characterTween;
        [SerializeField] string[] script;
        public int scriptStage = 1;
        // Start is called before the first frame update
        void Start()
        {
            //Freeze player upon tutorial start
            playerCamera.cameraMode = CameraMode.Scriptable;
            playerMovement.moveSpeed = 0;
            tutorialEvent = tutorialText.doneTyping;
            tutorialEvent.AddListener(nextTutorial);
            tutorialText.text = script[0];
            rod.GetComponent<Rod>().reelResistance = 0;
            caughtEvent = rod.GetComponent<Rod>().caughtFish;
            caughtEvent.AddListener(StartNext);
        }

        // Update is called once per frame
        void Update()
        {
            if (tutorialTween != null)
            {
                tutorialTween.TweenFrame();
            }
            if (Input.GetKeyDown(KeyCode.E) & nextButton.IsActive())
            {
                StartNext();
            }
        }

        IEnumerator setScriptable()
        {
            new WaitForSeconds(2);
            new WaitForSeconds(2);
            playerCamera.cameraMode = CameraMode.Scriptable;
            yield return null;
        }

        void nextTutorial()
        {
            if (scriptStage>=script.Length) { return; } //If the tutorial is finished, return
            tutorialText.text = script[scriptStage++];
            if (scriptStage == 2 || scriptStage == 3 || scriptStage == 5)
            {
                nextButton.gameObject.SetActive(true);
            }
            if (scriptStage == 5)
            {
                rod.SetActive(true);//Give rod after telling player about it
            }
        }
        public void StartNext()
        {
            tutorialText.displayText.text = "";
            StartCoroutine(tutorialText.writeText(tutorialText.waitTime));
            nextButton.gameObject.SetActive(false);
            tutorialCallback();
        }
        void tutorialCallback()
        {
            if (scriptStage==2)
            {
                playerCamera.cameraMode = CameraMode.ThirdPerson;
                playerMovement.moveSpeed = 20;
            }
        }
    }
}