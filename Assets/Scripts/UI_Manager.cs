using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public ARFaceManager faceManager;

    public Text indexText;
    int vertNum = 0;
    int vertCount = 468;

    private void Start()
    {
        indexText.text = vertNum.ToString();

    }

    public void IndexIncrease()
    {
        int number = Mathf.Min(++vertNum, vertCount-1);
        indexText.text = number.ToString();
    }
    public void IndexDecrease()
    {
        int number = Mathf.Max(--vertNum, 0);
        indexText.text = number.ToString();
    }

    public void ToggleMaskImage()
    {
        foreach (ARFace face in faceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                face.gameObject.SetActive(!face.gameObject.activeSelf);
            }
        }
    }
}
