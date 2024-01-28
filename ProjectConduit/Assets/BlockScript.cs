using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BlockScript : MonoBehaviour
{
    //AND GATE
    public int inputAmount;
    public int step;

    public List<GameObject> inputPorts;
    public List<GameObject> outputPorts;

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

            foreach (GameObject wire in outputPorts)
            {
                wire.GetComponent<WireScript>().powered = result;
            }
        }
    }

    bool AndGate()
    {
        foreach (GameObject wire in inputPorts)
        {
            if (!wire.GetComponent<WireScript>().powered)
            {
                return false;
                break;
            }
        }

        return true;
    }

    bool OrGate()
    {
        foreach (GameObject wire in inputPorts)
        {
            if (!wire.GetComponent<WireScript>().powered)
            {
                return true;
                break;
            }
        }

        return false;
    }
}
