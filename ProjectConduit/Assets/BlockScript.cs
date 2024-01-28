using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour
{
    //AND GATE
    public int inputAmount;
    public int step;

    public Dictionary<GameObject, GameObject> inputPorts = new Dictionary<GameObject, GameObject>();
    public Dictionary<GameObject, GameObject> outputPorts = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MainScript.Stepped += WhenStepped;
    }
    void WhenStepped(int step)
    {
        if (this.step == step && outputPorts != null)
        {
            bool result = AndGate();

            foreach (KeyValuePair<GameObject, GameObject> pr in outputPorts)
            {
                pr.Key.GetComponent<WireScript>().powered = result;
            }
        }
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
