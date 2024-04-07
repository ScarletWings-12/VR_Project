using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using UnityEngine.UI;
using System;
#if !UNITY_EDITOR
using UnityEngine.XR;
#endif

public class XRCardboardInputModule : PointerInputModule
{
    [SerializeField]
    XRCardboardInputSettings settings = default;

    GameObject currentTarget;
    Outline outl;
    bool outOfRange = false;
    GameObject raycastObj = null;
    RaycastHit loc;

    public override void Process()
    {
        HandleLook();
        HandleSelection();
    }

    void HandleLook()
    {

        GameObject lineR = GameObject.Find("Line");
        GameObject myObject = GameObject.Find("MainMenuEventSystem");
        var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getMainMenu().GetComponent<MainMenuFunctions>();
        loc = lineR.GetComponent<LineRaycastFunctions>().getHitPoint(pobj.getRaycastLength());

        try
        {
            raycastObj = loc.transform.gameObject;
        }
        catch (NullReferenceException)
        {
            raycastObj = null;
        }
        

        //Disable not used ones.
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("object");

        foreach (GameObject obj in objectsWithTag)
        {
            outl = obj.GetComponent<Outline>();
            
            if (outl.enabled && (obj != raycastObj || loc.distance > pobj.getRaycastLength()))
            {
                outl.enabled = false;
                if (obj.tag == "button")
                    obj.GetComponent<Renderer>().material.color = Color.white;
            }

        }

        objectsWithTag = GameObject.FindGameObjectsWithTag("button");

        foreach (GameObject obj in objectsWithTag)
        {
            outl = obj.GetComponent<Outline>();

            if (outl.enabled && (obj != raycastObj || loc.distance > pobj.getRaycastLength()))
            {
                outl.enabled = false;
                obj.GetComponent<Renderer>().material.color = Color.white;
            }

        }

        if (raycastObj != null)
        {
            if (loc.distance < pobj.getRaycastLength())
            {
                outOfRange = false;
                outl = raycastObj.GetComponent<Outline>();
                if (outl != null)
                    outl.enabled = true;
                if (raycastObj.tag == "button")
                    raycastObj.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                outOfRange = true;
            }

        }
    }
    void HandleSelection()
    {
        GameObject handler = null;
        try
        {
            if(!outOfRange)
                handler = raycastObj;
            var selectable = handler.GetComponent<Selectable>();

            if ((selectable && selectable.interactable == false))
                throw new NullReferenceException();
            
        }
        catch (NullReferenceException)
        {
            currentTarget = null;
            return;
        }
        
        if (currentTarget != handler)
            currentTarget = handler;

        if ((currentTarget.tag == "object") && Input.GetButtonDown(settings.ClickInputX))
        {
            var obj = currentTarget;
            GameObject myObject = GameObject.Find("MainMenuEventSystem");
            var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getContextMenu().GetComponent<ContextMenuFunctions>(); 
            pobj.setObject(currentTarget);
            var pos = obj.transform.position;
            pos[0] += 1.5f;
            pobj.SetPosition(pos);

        }
        else if(Input.GetButtonDown(settings.ClickInputB) && (currentTarget.name == "Cut_button" || currentTarget.name == "Copy_button" || currentTarget.name == "Exit_button"))
        {
            
            GameObject myObject = GameObject.Find("ContextMenu");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            if (currentTarget.name == "Cut_button")
            {
                pobj.setAction(1);
                pobj.getObject().SetActive(false);
            }
            else if (currentTarget.name == "Copy_button")
            {
                pobj.setAction(2);
                
            }
            else {
                pobj.setAction(3);
            }
            pobj.resetMenu();
        }
        else if (currentTarget.name == "Plane" && Input.GetButtonDown(settings.ClickInputY))
        {

            GameObject myObject = GameObject.Find("Character");
            // var pobj = myObject.GetComponent<CharacterMovement>();
            // var tp_loc = loc.point;
            // tp_loc[1] = 0.5f;

            // pobj.Teleport(tp_loc);

            var obj = currentTarget;
            myObject = GameObject.Find("MainMenuEventSystem");
            var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getContextMenuPlane().GetComponent<ContextMenuFunctions>(); 
            pobj.setObject(currentTarget);
            var pos = loc.point;
            pos[1] += 1f;
            pobj.SetPosition(pos);
            pobj.SetPlacementLoc(loc.point);
            
        }
        else if(Input.GetButtonDown(settings.ClickInputY) && currentTarget.name == "Add_button")
        {
        
            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
           
            // pobj.setAction(4);
            

            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>(); 
            // var pobj1 = myObject1.GetComponent<ContextMenuFunctions>();
            pobj1.SetPosition(myObject.transform.position);
            pobj.resetMenu();
        }
        else if(Input.GetButtonDown(settings.ClickInputY) && currentTarget.name == "Sofa_button")
        {
        
            GameObject myObject = GameObject.Find("ContextMenuPlane");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var spawnLoc  = pobj.GetPlacementLoc();
            GameObject clonedObject = Instantiate(GameObject.Find("lounge_chair_001"), spawnLoc, Quaternion.identity);
            // pobj.setAction(4);
            

            GameObject myObject1 = GameObject.Find("MainMenuEventSystem");
            var pobj1 = myObject1.GetComponent<MainMenuEventSystemScript>().getContextMenuFurniture().GetComponent<ContextMenuFunctions>(); 
            // var pobj1 = myObject1.GetComponent<ContextMenuFunctions>();
            // pobj1.SetPosition(myObject.transform.position);
            pobj1.resetMenu();
        }
        // else if (currentTarget.name == "Plane" && Input.GetButtonDown(settings.ClickInputY))
        // {

        //     GameObject myObject = GameObject.Find("Character");
        //     var pobj = myObject.GetComponent<CharacterMovement>();
        //     var tp_loc = loc.point;
        //     tp_loc[1] = 0.5f;

        //     pobj.Teleport(tp_loc);
            
        // }
        else if (Input.GetButtonDown(settings.ClickInputA))
        {
            GameObject myObject = GameObject.Find("ContextMenu");
            var pobj = myObject.GetComponent<ContextMenuFunctions>();
            var tp_loc = loc.point;
            tp_loc[1] = 0.5f;
            if (pobj.getAction() == 1)
            {
                pobj.getObject().transform.position = tp_loc;
                pobj.getObject().SetActive(true);

            }
            else if (pobj.getAction() == 2)
            {
                GameObject clonedObject = Instantiate(pobj.getObject(), tp_loc, pobj.getObject().transform.rotation);
            }
            else if (pobj.getAction() == 3)
            {
                pobj.setObject(null);
                pobj.resetMenu();
            }
        }


    }

}