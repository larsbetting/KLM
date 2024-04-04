using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UnityEvent ParkEvent;
    
    #region variables
    public GameObject[] hangars;
    public GameObject[] planes;
    public Text[] hangarIDs;

    public Button ParkButton;
    public Button LightsOnButton;
    public Button LightsOffButton;
    
    [SerializeField]
    private Canvas UICanvas;

    [SerializeField]
    private Font normalFont;

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        hangars = GameObject.FindGameObjectsWithTag("hangar");
        hangarIDs = new Text[hangars.Length];

        planes = GameObject.FindGameObjectsWithTag("plane");
        for(int i = 0; i < planes.Length; i++)
        {
           // ParkEvent.AddListener(planes[i].GetComponent<Plane>().Park);
        }
        InitText();

        
        ParkButton.onClick.AddListener(StartParkEvent);

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
    }

    public void StartParkEvent()
    {
        ParkEvent.Invoke();
    }


}
