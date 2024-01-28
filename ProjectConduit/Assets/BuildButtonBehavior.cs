using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildButtonBehavior : MonoBehaviour
{
    public static event Action<GameObject> BuildBlockCommand;
    public GameObject blockTemplate;

    public void WhenClicked()
    {
        print("Build bloc");
        BuildBlockCommand?.Invoke(blockTemplate);
    }
}
