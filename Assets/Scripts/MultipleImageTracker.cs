using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MultipleImageTracker : MonoBehaviour
{
    ARTrackedImageManager imageManager;
    // Start is called before the first frame update
    void Start()
    {
        imageManager = GetComponent<ARTrackedImageManager>();

        imageManager.trackedImagesChanged += OnTrackedImage;

    }

    public void OnTrackedImage(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage tracked in args.added)
        {
            string imgName = tracked.referenceImage.name;

            GameObject imgPrefab = Resources.Load<GameObject>(imgName);

            if (imgPrefab != null)
            {

                if (tracked.transform.childCount < 1)
                {
                    GameObject go = Instantiate(imgPrefab, tracked.transform.position, tracked.transform.rotation);
                    go.transform.SetParent(tracked.transform);
                }
            }
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.transform.childCount > 0)
            {
                trackedImage.transform.GetChild(0).position = trackedImage.transform.position;
                trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
            }
        }
    }
}
