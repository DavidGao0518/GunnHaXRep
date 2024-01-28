using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasControl : MonoBehaviour
{
    public GameObject blockButton;

    // Start is called before the first frame update
    void Start()
    {
        MainScript.InputCommand += WhenInputCommand;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WhenInputCommand(string cmd)
    {
        InitializeBuildList();
    }
    void InitializeBuildList()
    {
        TabButton buttonFunc = blockButton.GetComponent<TabButton>();
        
    }
}
