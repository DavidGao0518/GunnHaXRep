using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour
{
    //AND GATE
    public int blockType;

    public Dictionary<GameObject, GameObject> inputPorts = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> outputPorts = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
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
                //what?
            }
            else if (blockType == 2) //and gate
            {
                result = AndGate();
            }
            else if (blockType == 3) //or gate
            {
                result = OrGate();
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



    //Block functions:

    bool StartBlock()
    {
        return true;
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
            if (!pr.Key.GetComponent<WireScript>().powered)
            {
                return true;
                break;
            }
        }

        return false;
    }
}
