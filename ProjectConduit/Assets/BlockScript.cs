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
    public GameObject outputPort;

    // Start is called before the first frame update
    void Start()
    {
        MainScript.Stepped += WhenStepped;
    }

    void WhenStepped(int step)
    {
        if (this.step == step && outputPort != null)
        {
            bool result = true;

            foreach (GameObject wire in inputPorts)
            {
                if (!wire.GetComponent<WireScript>().powered)
                {
                    result = false;
                    break;
                }
            }

            outputPort.GetComponent<WireScript>().powered = result;
        }
    }
}
