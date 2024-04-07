using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenuFunctions : MonoBehaviour
{
    public GameObject belongsTo;
    public int action = 0;
    public Vector3 placementLoc;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(100, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void setAction(int i)
    {
        action = i;
    }
    
    public int getAction()
    {
        return action;
    }

    public void setObject(GameObject caller)
    {
        action = 0;
        belongsTo = caller;
    }

    public GameObject getObject()
    {
        return belongsTo;
    }

    public void resetMenu()
    {
        transform.position = new Vector3(100, 100, 100);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetPlacementLoc(Vector3 pos)
    {
        placementLoc = pos;
    }

    public Vector3 GetPlacementLoc()
    {
        return placementLoc;
    }
}
