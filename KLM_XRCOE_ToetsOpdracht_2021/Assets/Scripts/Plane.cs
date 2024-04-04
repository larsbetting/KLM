using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // reference to the planeData class
    public PlaneData planeData;

    public string brand { get; private set; }
    public string type { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //initialize the components from the scriptable object
        //ID = planeData.ID;
        brand = planeData.brand;
        type = planeData.type;
        //IDText.text = ID.ToString(); // convert the ID to a string to be displayed on top of the plane
        gameObject.GetComponent<Renderer>().material = planeData.material;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 screenPosition = Camera.main.WorldToScreenPoint(this.transform.position);
        //IDText.transform.position = screenPosition;
    }
}
