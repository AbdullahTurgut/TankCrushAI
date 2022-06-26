using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    float rSpeed = 5;
    Vector3 dir = Vector3.zero;
    float mSpeed = 2;

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray.origin,ray.direction,out hit))
            {
                dir = hit.point;
            }
            transform.position = Vector3.Lerp(transform.position, hit.point, mSpeed * Time.deltaTime);
            
        }
    }
}
