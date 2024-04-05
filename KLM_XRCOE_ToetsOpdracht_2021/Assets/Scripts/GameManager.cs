using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Create events
    #region events
    public UnityEvent ParkEvent;
    public UnityEvent LightsOnEvent;
    public UnityEvent LightsOffEvent;
    #endregion

    #region variables
    public GameObject[] hangars { get; private set; } // Array storing hangars in the scene
    public GameObject[] planes { get; private set; } // Array storing planes in the scene
    public Text[] hangarIDs { get; private set; }
    public Image image; // Create reference to congratulations image

    // create button references
    public Button parkButton;
    public Button lightsOnButton;
    public Button lightsOffButton;

    // create bool to check if the player has parked all planes
    private bool _isParked = false;

    // create reference to font that is used for hangar ID's
    [SerializeField]
    private Font normalFont;

    #endregion

  
    void Start()
    {
        //init events
        ParkEvent = new UnityEvent();
        LightsOnEvent = new UnityEvent();
        LightsOffEvent = new UnityEvent();

        // find all hangars in the scene and store them into an array
        hangars = GameObject.FindGameObjectsWithTag("hangar");
        hangarIDs = new Text[hangars.Length]; //init same size array of text objects

        // store all planes in the scene into an array
        planes = GameObject.FindGameObjectsWithTag("plane");

        // Add the respective event functions for each plane as a listener to the event in this class
        for (int i = 0; i < planes.Length; i++)
        {
            int currentIndex = i;
            ParkEvent.AddListener(delegate { planes[currentIndex].GetComponent<Plane>().Park(hangars[currentIndex].transform.position); });
            LightsOnEvent.AddListener(planes[currentIndex].GetComponent<Plane>().LightsOn);
            LightsOffEvent.AddListener(planes[currentIndex].GetComponent<Plane>().LightsOff);
        }


        // initialize text on the hangars
        InitText();

        // Set the respective buttonfunctions to be called on click
        parkButton.onClick.AddListener(StartParkEvent);
        lightsOnButton.onClick.AddListener(StartLightsOnEvent);
        lightsOffButton.onClick.AddListener(StartLightsOffEvent);

    }

    private void InitText()
    {
        for (int i = 0; i < hangars.Length; i++)
        {
            // Create new Canvas for each hangar in the scene
            GameObject canvasObject = new GameObject("Canvas for hangar " + i);
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;

            // Set canvas position equal to their hangar
            canvas.transform.position = new Vector3(hangars[i].transform.position.x, hangars[i].transform.position.y + 1, hangars[i].transform.position.z);

            // Set canvas scale, size and rotation, currently i'm using raw data
            // based on trial and error but I'm sure there is a more efficient way of doing this
            canvas.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(200f, 100f);
            canvas.transform.Rotate(new Vector3(90, 180, 0)); // makes sure the text is facing the right direction

            // Create a new Text GameObject inside the canvas
            GameObject textObject = new GameObject("Text" + i);
            Text text = textObject.AddComponent<Text>();

            // Set text properties
            text.font = normalFont;
            text.fontSize = 24;
            text.color = Color.black;
            text.text = "Hangar " + i;

            // Set text position within the canvas
            textObject.transform.SetParent(canvasObject.transform, false);
            textObject.transform.localPosition = Vector3.zero;
        }
    } // Initializes the text on each of the hangars

    public void StartParkEvent()
    {
        ParkEvent.Invoke();
    } // triggers the parking event

    public void StartLightsOnEvent()
    {
        LightsOnEvent.Invoke();
    } // triggers the lights on event

    public void StartLightsOffEvent()
    {
        LightsOffEvent.Invoke();
    } // triggers the lights off event

    public void DisplayCongratulations()
    {
        image.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (!_isParked)
        {
            int total = 0;
            for (int i = 0; i < planes.Length; i++)
            {
                // check's if the distance between the plane and the hangar is "almost" the same
                if (Vector3.Distance(planes[i].GetComponent<Plane>().navMeshAgent.transform.position, hangars[i].transform.position) < 0.01f)
                {
                    total++;
                }
            }
            if(total == planes.Length) // if all planes have been parked
            {
                DisplayCongratulations();
                _isParked = true;
            }
        }

    }

}
