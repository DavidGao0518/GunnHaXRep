using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class MainScript : MonoBehaviour
{
    public static event Action<int> Stepped;
    public static event Action<string> InputCommand;
    public GameObject wireTemplate;
    public List<GameObject> blockList;

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
        InputCommand += WhenSelfInputCommand;
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

                    if (hit.transform.gameObject.layer == 7) //Gates, make wire
                    {
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                    else if (hit.transform.gameObject.layer == 8) //Gates, make wire
                    {
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                    else if (hit.transform.gameObject.layer == 9) //Blocks, edit block
                    {
                        StartCoroutine(MakeWireProcess(hit.transform.gameObject));
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1)) {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                print("Clicked right");
                print(hit.transform);

                if (hit.collider != null)
                {
                    Debug.Log(hit.transform.name);

                    if (hit.transform.gameObject.layer == 6) //Wire, delete
                    {
                        ClearWire(hit.transform.gameObject);
                    }
                }
            }
        }
    }

    //Events:

    void WhenSelfInputCommand(string cmd)
    {
        if (cmd == "BuildBlock")
        {

        }
    }



    //Actual functions:
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

        wireScript.attachment1.GetComponent<BlockScript>().outputPorts.Remove(wire);
        wireScript.attachment2.GetComponent<BlockScript>().inputPorts.Remove(wire);
        Destroy(wire);
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
            wireScript.attachment2 = parentBlock;
            wire.transform.Find("Arrow").transform.eulerAngles = new Vector3(0, 0, 90);
            targetPortType = 8;
        }
        else
        {
            wireScript.attachment1 = parentBlock;
            wire.transform.Find("Arrow").transform.eulerAngles = new Vector3(0, 0, -90);
            targetPortType = 7;
        }

        userState = true;

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
                            if (targetPortType == 7 && blockScript.inputPorts.ContainsValue(parentBlock)) {
                                //nah
                            }
                            else if (targetPortType == 8 && blockScript.outputPorts.ContainsValue(parentBlock)) {
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
                        wireScript.attachment2 = potentialPort.transform.parent.gameObject;
                    }
                    else
                    {
                        wireScript.attachment1 = potentialPort.transform.parent.gameObject;
                    }

                    wireScript.attachment1.GetComponent<BlockScript>().outputPorts.Add(wire, wireScript.attachment2);
                    wireScript.attachment2.GetComponent<BlockScript>().inputPorts.Add(wire, wireScript.attachment1);
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
    void ClearBlock(GameObject block)
    {
        //to do
    }
    IEnumerator MakeBlockProcess(GameObject blockTemplate)
    {
        userState = true;

        while (userState)
        {
            yield return null;
            break;
        }

        userState = false;
    }
}
