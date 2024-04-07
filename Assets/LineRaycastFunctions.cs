using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRaycastFunctions : MonoBehaviour
{
    public Vector3[] positions = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        
        int numberOfPositions = lr.GetPositions(positions);
        for (int i = 0; i < numberOfPositions; i += 1)
            positions[i] = transform.TransformPoint(positions[i]);
    }

    public RaycastHit getHitPoint(float distance)
    {
        Vector3 startPos = positions[0];
        Vector3 direction  = (positions[1] - positions[0]).normalized;

        Ray ray = new Ray(startPos, direction);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, distance);
        return hit;

    }
}
