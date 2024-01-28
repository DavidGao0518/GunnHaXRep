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
                    Debug.Log(hit.transform.name);

                    if (hit.transform.gameObject.layer == 7)
                    {
                        userState = true;
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                    else if (hit.transform.gameObject.layer == 8)
                    {
                        userState = true;
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    userState = true;
                    Debug.Log(hit.transform.name);

                    if (hit.transform.gameObject.layer == 6)
                    {
                        ClearWire(hit.transform.gameObject);
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

        Transform arrow = wire.transform.Find("Arrow");
        arrow.localScale = new Vector2(3, 0.3f / wire.transform.localScale.x);
    }
    
    void ClearWire(GameObject wire) {
        WireScript wireScript = wire.GetComponent<WireScript>();
    }
    IEnumerator MakeWireProcess(GameObject port1)
    {
        GameObject wire = Instantiate(wireTemplate);
        WireScript wireScript = wire.GetComponent<WireScript>();
        GameObject potentialPort;
        GameObject parentBlock = port1.transform.parent.gameObject;
        int targetPortType = 0;

        if (port1.layer == 7)
        {
            wireScript.attachment2 = port1;
            wire.transform.Find("Arrow").transform.eulerAngles = new Vector3(0, 0, 90);
            targetPortType = 8;
        }
        else
        {
            wireScript.attachment1 = port1;
            wire.transform.Find("Arrow").transform.eulerAngles = new Vector3(0, 0, -90);
            targetPortType = 7;
        }

        wireScript.attachment1 = port1;


        while (userState)
        {
            yield return null;
            potentialPort = null;
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.layer == targetPortType)
                {
                    if (hit.transform.parent != port1.transform.parent)
                    {
                        BlockScript blockScript = hit.transform.parent.GetComponent<BlockScript>();

                        if (blockScript != null) {
                            print("there a blc");
                            if (targetPortType == 7 && blockScript.inputPorts.Contains(parentBlock)) {
                                //nah
                            }
                            else if (targetPortType == 8 && blockScript.outputPorts.Contains(parentBlock)) {
                                //nah
                            }
                            else {
                                potentialPort = hit.transform.gameObject;
                            }
                        }
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
                        wireScript.attachment2 = potentialPort;
                    }
                    else
                    {
                        wireScript.attachment1 = potentialPort;
                    }

                    wireScript.attachment1.transform.parent.GetComponent<BlockScript>().outputPorts.Add(wireScript.attachment2.transform.parent.gameObject);
                    wireScript.attachment2.transform.parent.GetComponent<BlockScript>().inputPorts.Add(wireScript.attachment1.transform.parent.gameObject);
                    break;
                }
            }
            else
            {
                RenderWire(wire, port1.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (Input.GetMouseButtonDown(0))
                {
                    Destroy(wire);
                    break;
                }
            }
        }

        userState = false;
    }
}
