using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 moveInputs;
    public float moveSpeed;
    float zoomInputs;
    Camera currentCamera;
    float zoomLevel;
    public float zoomSpeed;
    void Start()
    {
        currentCamera = transform.GetComponent<Camera>();
        zoomLevel = currentCamera.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3)moveInputs * Time.deltaTime * moveSpeed;

        zoomLevel -= zoomInputs * zoomSpeed * Time.deltaTime;
        currentCamera.orthographicSize = math.max(1, zoomLevel);
        //print(moveInputs);
    }

    void OnMove(InputValue action)
    {
        moveInputs = action.Get<Vector2>();
        //transform.position = (Vector2) transform.position + moveInputs;
    }

    void OnZoom(InputValue action)
    {
        zoomInputs = action.Get<float>();
    }
}
