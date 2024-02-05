using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour
{
    //AND GATE
    public int blockType;
    private GameObject light;

    public Dictionary<GameObject, GameObject> inputPorts = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> outputPorts = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (blockType == 1)
        {
            light = transform.Find("Light").gameObject;
            MainScript.UpdateState += onUpdateState;
        }

        MainScript.Stepped += WhenStepped;
    }
    void WhenStepped(GameObject Block)
    {
        if (this.gameObject == Block)
        {
            bool result = false;

            if (blockType == 0) //Start block
            {
                result = true;
            }
            else if (blockType == 1) //Output block
            {
                OutputBlock();
            }
            else if (blockType == 2) //and gate
            {
                result = AndGate();
            }
            else if (blockType == 3) //or gate
            {
                result = OrGate();
            }
            else if (blockType == 4) //or gate
            {
                result = NotGate();
            }
            else
            {
                //undefined block
                print("Undefined block");
            }

            foreach (KeyValuePair<GameObject, GameObject> pr in outputPorts)
            {
                pr.Key.GetComponent<WireScript>().powered = result;
            }
        }
    }

    void onUpdateState(bool placeholder)
    {

       if (blockType == 1)
       {
            print("RESETLIGHT");
            OutputBlock();
       }
    }

    //Block functions:

    void OutputBlock()
    {
        bool result = false;
        foreach (KeyValuePair<GameObject, GameObject> pr in inputPorts)
        {
            if (pr.Key.GetComponent<WireScript>().powered)
            {
                print("LightPowered");
                result = true;
                break;
            }
        }

        light.SetActive(result);
    }
    bool AndGate()
    {
        foreach (KeyValuePair<GameObject, GameObject> pr in inputPorts)
        {
            if (!pr.Key.GetComponent<WireScript>().powered)
            {
                return false;
                break;
            }
        }

        return true;
    }

    bool OrGate()
    {
        foreach (KeyValuePair<GameObject, GameObject> pr in inputPorts)
        {
            if (pr.Key.GetComponent<WireScript>().powered)
            {
                return true;
                break;
            }
        }

        return false;
    }

    bool NotGate()
    {
        bool answer = false;
        foreach (KeyValuePair<GameObject, GameObject> pr in inputPorts)
        {
            answer = pr.Key.GetComponent<WireScript>().powered;
        }

        return !answer;
    }
}