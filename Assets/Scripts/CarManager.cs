using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManager : MonoBehaviour
{
    public GameObject indicator;
    ARRaycastManager raycastManager;
    public GameObject myCar;
    GameObject placedObject;
    // Start is called before the first frame update
    void Start()
    {
        indicator.SetActive(false);
        raycastManager = GetComponent<ARRaycastManager>();
        Debug.Log("AR START");
    }

    // Update is called once per frame
    void Update()
    {
        DetectGround();

        if (EventSystem.current.currentSelectedGameObject)
            return;

        if (indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // 첫번째 터치를 가져온다. 10번째까지 셀 수 있음.

            if (touch.phase == TouchPhase.Began) // 터치가 시작 됐다면.
            {
                if(placedObject == null)
                    placedObject = Instantiate(myCar, indicator.transform.position, indicator.transform.rotation);
                else placedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
            }
        }
    }

    void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfo = new List<ARRaycastHit>();


        if(raycastManager.Raycast(screenSize, hitInfo, TrackableType.Planes))
        {
            indicator.SetActive(true);
            indicator.transform.position = hitInfo[0].pose.position;
            indicator.transform.rotation = hitInfo[0].pose.rotation;

            indicator.transform.position += indicator.transform.up * 0.1f;
        }
        else
        {
            indicator.SetActive(false );
        }
    }
}
