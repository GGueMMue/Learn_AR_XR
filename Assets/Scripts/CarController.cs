using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float rotateSpeed = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                RaycastHit hitinfo;

                if (Physics.Raycast(ray, out hitinfo, Mathf.Infinity, 1 << 0))
                {
                    Vector3 deltaPos = touch.deltaPosition;

                    transform.Rotate(transform.up, deltaPos.x * -1.0f * rotateSpeed);
                }
            }
        }
    }
}
