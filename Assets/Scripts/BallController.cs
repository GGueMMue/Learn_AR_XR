using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    Rigidbody body;
    bool isReady = true;
    Vector2 startPos;
    float reStartTime = 3f;
    public float captureRate = .3f;
    public Text result;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        result.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady) return;

        SetBallPos(Camera.main.transform);


        if ((Input.touchCount > 0) && isReady)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                float dragDistance = touch.position.y - startPos.y;

                Vector3 thorwAngle = (Camera.main.transform.forward + Camera.main.transform.up).normalized;

                body.isKinematic = false;
                isReady = false;

                body.AddForce(thorwAngle * dragDistance * 0.005f, ForceMode.VelocityChange);


                Invoke("ResetBall", reStartTime);
            }
        }
    }

    void SetBallPos(Transform tr)
    {
        Vector3 offset = tr.forward * 0.5f + tr.up * -0.2f;
        transform.position = offset + tr.position;
    }

    void ResetBall()
    {
        body.isKinematic = true;
        body.velocity = Vector3.zero;

        isReady = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (isReady) return;

        float draw = Random.Range(0, 1.0f);

        if (draw <= captureRate)
        {
            result.text = "Æ÷È¹ ¼º°ø";
        }
        else
        {
            result.text = "Æ÷È¹ ½ÇÆÐ";
        }

        Instantiate(effect, collision.gameObject.transform.position, Camera.main.transform.rotation);


        Destroy(collision.gameObject);

        gameObject.SetActive(false);
    }
}
