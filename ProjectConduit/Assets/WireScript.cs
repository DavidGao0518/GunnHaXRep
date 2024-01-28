using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    public Material mat;
    public GameObject attachment1;
    public GameObject attachment2;
    public bool powered;

    private GameObject litWire;

    // Start is called before the first frame update
    void Start()
    {
        litWire = transform.Find("PoweredWire").gameObject;
        MainScript.UpdateState += onUpdateState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onUpdateState(bool placeholder)
    {
        if (litWire != null)
        {
            litWire.SetActive(powered);
        }
    }

}
