using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine.UI;

public class FindDir : MonoBehaviour
{
    public ARFaceManager afm;
    public GameObject smallSphere;
    public GameObject sunglass;

    NativeArray<ARCoreFaceRegionData> regions;

    List<GameObject> faceSphere = new List<GameObject>();
    ARCoreFaceSubsystem subsys;

    public Text vertIndex;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(smallSphere);
            faceSphere.Add(go);
            go.SetActive(true);
        }
        afm.facesChanged += OnDetectFaceAll;
        // 델리게이트, Face 매니져가 얼굴이 바뀔 때 마다 함수 실행.
        subsys = (ARCoreFaceSubsystem)afm.subsystem;
    }

    void OnDetectThreePoints(ARFacesChangedEventArgs facesChangedEventArgs)
    {
        if (facesChangedEventArgs.updated.Count > 0)
        {
            subsys.GetRegionPoses(facesChangedEventArgs.updated[0].trackableId, Allocator.Persistent, ref regions);

            for(int i =0; i < regions.Length; i++)
            {
                faceSphere[i].transform.position = regions[i].pose.position;
                faceSphere[i].transform.rotation = regions[i].pose.rotation;
                faceSphere[i].SetActive(true);
            }
        }
        else if (facesChangedEventArgs.removed.Count > 0)
        {
            for(int i =0; i < regions.Length;i++)
            {
                faceSphere[i].SetActive(false);
            }
        }
    }

    void OnDetectFaceAll(ARFacesChangedEventArgs faceChangedEventArgs)
    {
        Debug.Log("Start Func");
        if (faceChangedEventArgs.updated.Count > 0)
        {
            Debug.Log("Start init");

            Vector3 sunVert = faceChangedEventArgs.updated[0].vertices[151];
            sunVert = faceChangedEventArgs.updated[0].transform.TransformPoint(sunVert);
            sunglass.SetActive(true);
            sunglass.transform.position = sunVert;
            Debug.Log("썬글라스 어디임? : " + sunglass.transform.position);

            int num = int.Parse(vertIndex.text);

            Vector3 vert = faceChangedEventArgs.updated[0].vertices[num];
            vert = faceChangedEventArgs.updated[0].transform.TransformPoint(vert);

            faceSphere[0].SetActive(true);
            faceSphere[0].transform.position = vert;
            Debug.Log("End init");

        }
        else if (faceChangedEventArgs.removed.Count > 0)
        {
            Debug.Log("Missing head");

            faceSphere[0].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
