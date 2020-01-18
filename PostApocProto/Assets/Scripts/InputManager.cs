using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private int leftMouseButton = 0;
    private int middleMouseButton = 2;
    private int rightMouseButton = 1;

    //Selected unit/object
    public GameObject selectedObject;
    private ObjectInfo selectedInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(rightMouseButton))
        {
            RightClick();
        }
        else if (Input.GetMouseButtonDown(leftMouseButton))
        {
            LeftClick();
        }
        
    }

    //Manipulating Game Objects
    public void RightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100))
        {
            if(hit.collider.tag == "Ground" && selectedInfo != null)
            {
                selectedInfo.getAgent().destination = hit.point;
                Debug.Log("Moving to point: " + hit.point);
            } else
            {
                Debug.Log("Nothing Selected");
            }
        }
    }

    //Selecting Game Objects
    public void LeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.collider.tag == "Ground")
            {
                selectedInfo.isSelected = false;
                selectedObject = null;
                selectedInfo = null;
                Debug.Log("Deselected");
            }
            else if (hit.collider.tag == "Player")
            {
                selectedObject = hit.collider.gameObject;
                selectedInfo = selectedObject.GetComponent<ObjectInfo>();

                selectedInfo.isSelected = true;

                Debug.Log("Selected" + selectedInfo.objectName);
            }
        }
    }
}
