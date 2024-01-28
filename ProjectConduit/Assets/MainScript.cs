using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class MainScript : MonoBehaviour
{
    public static event Action<int> Stepped;
    public GameObject wireTemplate;

    public bool userState = false;

    float RadToDeg(float input)
    {
        return input * 180 / math.PI;
    }
    float DegToRad(float input)
    {
        return input / 180 * math.PI;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if (!userState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("Clicked");

                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    userState = true;
                    Debug.Log(hit.transform.name);

                    if (hit.transform.gameObject.layer == 7)
                    {
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                    else if (hit.transform.gameObject.layer == 8)
                    {
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                }
            }
        }
    }
    void RenderWire(GameObject wire, Vector2 start, Vector2 end)
    {
        wire.transform.position = start + (end - start) / 2;
        wire.transform.eulerAngles = new Vector3(0, 0, RadToDeg(math.atan2(end.y - start.y, end.x - start.x)));
        wire.transform.localScale = new Vector2((end - start).magnitude, wire.transform.localScale.y);
    }
    

    IEnumerator MakeWireProcess(GameObject port1)
    {
        GameObject wire = Instantiate(wireTemplate);
        WireScript wireScript = wire.GetComponent<WireScript>();
        GameObject potentialPort;
        int targetPortType = 0;

        if (port1.layer == 7)
        {
            wireScript.attachment1 = port1;
            targetPortType = 8;
        }
        else
        {
            wireScript.attachment2 = port1;
            targetPortType = 7;
        }

        wireScript.attachment1 = port1;


        while (userState)
        {
            potentialPort = null;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.layer == targetPortType)
                {
                    if (hit.transform.parent != port1.transform.parent)
                    {
                        potentialPort = hit.transform.gameObject;
                    }
                }
            }

            if (potentialPort != null)
            {
                RenderWire(wire, port1.transform.position, potentialPort.transform.position);

                if (Input.GetMouseButtonDown(0))
                {
                    if (targetPortType == 7)
                    {
                        wireScript.attachment1 = potentialPort;
                    }
                    else
                    {
                        wireScript.attachment2 = potentialPort;
                    }
                    break;
                }
            }
            else
            {
                RenderWire(wire, port1.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            yield return null;
        }

        userState = false;
    }
}
