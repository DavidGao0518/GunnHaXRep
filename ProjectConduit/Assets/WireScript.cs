using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    public GameObject attachment1;
    public GameObject attachment2;
    public bool powered;

    // Start is called before the first frame update
    void Start()
    {
        MainScript.Stepped += WhenStepped;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void WhenStepped(int step)
    {

    }
}
