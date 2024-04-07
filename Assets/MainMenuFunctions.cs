using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MainMenuFunctions : MonoBehaviour
{
    public int raycastLength;
    public int characterSpeed, stopMoving = 1;
    public GameObject resumeButton, lengthButton, speedButton, currentButton, quitButton;
    public float debounceTime = 0.5f;
    bool useMainMenu = true, inputAllowed = true;

    // Start is called before the first frame update
    void Awake()
    {
        raycastLength = 50;
        characterSpeed = 10;
        resumeButton = GameObject.Find("Resume_button");
        lengthButton = GameObject.Find("Raycast_button");
        speedButton = GameObject.Find("Speed_button");
        quitButton = GameObject.Find("Quit_button");
        SelectDefaultButton();
    }
    public void SelectDefaultButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (resumeButton != null)
        {
            currentButton = resumeButton;
            es.SetSelectedGameObject(resumeButton);
        }
    }

    public void selectNextButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (currentButton == resumeButton)
        {
            es.SetSelectedGameObject(lengthButton);
            currentButton = lengthButton;
        }
        else if (currentButton == lengthButton)
        {
            es.SetSelectedGameObject(speedButton);
            currentButton = speedButton;
        }
        else if (currentButton == speedButton)
        {
            es.SetSelectedGameObject(quitButton);
            currentButton = quitButton;
        }
        else
        {
            es.SetSelectedGameObject(resumeButton);
            currentButton = resumeButton;
        }
    }

    public void selectPreviousButton()
    {
        EventSystem es = GetComponent<EventSystem>();
        if (currentButton == resumeButton)
        {
            es.SetSelectedGameObject(quitButton);
            currentButton = quitButton;
        }
        else if (currentButton == lengthButton)
        {
            es.SetSelectedGameObject(resumeButton);
            currentButton = resumeButton;
        }
        else if (currentButton == speedButton)
        {
            es.SetSelectedGameObject(lengthButton);
            currentButton = lengthButton;
        }
        else
        {
            es.SetSelectedGameObject(speedButton);
            currentButton = speedButton;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (useMainMenu && inputAllowed && Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if (Input.GetAxis("Vertical") > 0)
                selectNextButton();
            else
                selectPreviousButton();

            if(gameObject.activeSelf)
                StartCoroutine(DebounceInput());
        }

        if (useMainMenu && inputAllowed && Input.GetButtonDown("js5"))
        {
            Button myButton = currentButton.GetComponent<Button>();
            myButton.onClick.Invoke();
            if (gameObject.activeSelf)
                StartCoroutine(DebounceInput());
        }
        
    }

    public void onClickResume()
    {
        GameObject myObject = GameObject.Find("MainMenuEventSystem");
        var pobj = myObject.GetComponent<MainMenuEventSystemScript>().getMainMenu();
        pobj.SetActive(false);
        var eobj = myObject.GetComponent<MainMenuEventSystemScript>().getVReventSystemObject();
        eobj.SetActive(true);
        stopMoving = 1;

    }

    public void onClickLength()
    {
        if (raycastLength == 1)
        {
            raycastLength = 10;
            var txt = GameObject.Find("Raycast_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            c.text = "Raycast Length : 10";
        }
        else if (raycastLength == 10)
        {
            raycastLength = 50;
            var txt = GameObject.Find("Raycast_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            
            c.text = "Raycast Length : 50";
        }
        else
        {
            raycastLength = 1;
            var txt = GameObject.Find("Raycast_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            c.text = "Raycast Length : 1";
        }
    }

    public void onClickSpeed()
    {
        if (characterSpeed == 5)
        {
            characterSpeed = 10;
            var txt = GameObject.Find("Speed_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            c.text = "Speed: Medium";
        }
        else if (characterSpeed == 10)
        {
            characterSpeed = 20;
            var txt = GameObject.Find("Speed_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            c.text = "Speed: Fast";
        }
        else
        {
            characterSpeed = 5;
            var txt = GameObject.Find("Speed_text");
            var c = txt.GetComponent<TextMeshProUGUI>();
            c.text = "Speed: Slow";
        }
    }

    public void onClickExit()
    {
        Application.Quit();
    }

    IEnumerator DebounceInput()
    {
        inputAllowed = false;
        yield return new WaitForSeconds(debounceTime);
        inputAllowed = true;
    }

    public int getRaycastLength()
    {
        return raycastLength;
    }

    public void setRaycastLength(int l)
    {
        raycastLength = l;
    }

    public int getCharacterSpeed()
    {
        return characterSpeed;
    }

    public void setCharacterSpeed(int s)
    {
        characterSpeed = s;
    }
}
