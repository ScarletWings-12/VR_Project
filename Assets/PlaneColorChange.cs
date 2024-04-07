using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneColorChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color(0.9f, 0.6f, 0.5f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
