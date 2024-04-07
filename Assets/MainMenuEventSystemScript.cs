using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuEventSystemScript : MonoBehaviour
{
    GameObject mainMenuObject, contextMenuObject, VReventSystemObject, contextMenuPlaneObject, contextMenuFurnitureObject;

    public float debounceTime = 0.5f;
    bool inputAllowed = true;
    void Awake()
    {
        mainMenuObject = GameObject.Find("MainMenuCanvas");
        contextMenuObject = GameObject.Find("ContextMenu");

        contextMenuPlaneObject = GameObject.Find("ContextMenuPlane");
        contextMenuFurnitureObject = GameObject.Find("ContextMenuFurniture");
        
        mainMenuObject.SetActive(false);
        
        VReventSystemObject = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputAllowed && Input.GetButtonDown("js0"))
        {
            var sp = mainMenuObject.GetComponent<MainMenuFunctions>();
            
            sp.stopMoving = 0;
            VReventSystemObject.SetActive(false);
            mainMenuObject.SetActive(true);
            mainMenuObject.GetComponent<EventSystem>().enabled = true;
            StartCoroutine(DebounceInput());
        }
    }
    IEnumerator DebounceInput()
    {
        inputAllowed = false;
        yield return new WaitForSeconds(debounceTime);
        inputAllowed = true;
    }

    public GameObject getVReventSystemObject()
    {
        return VReventSystemObject;
    }
    public GameObject getMainMenu()
    {
        return mainMenuObject;
    }

    public GameObject getContextMenu()
    {
        return contextMenuObject;
    }

    public GameObject getContextMenuPlane()
    {
        return contextMenuPlaneObject;
    }
    public GameObject getContextMenuFurniture(){
        return contextMenuFurnitureObject;
    }
}
