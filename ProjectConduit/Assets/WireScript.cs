using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireScript : MonoBehaviour
{
    public Material mat;
    public GameObject attachment1;
    public GameObject attachment2;
    public bool powered;
    private Color disabledColor = new Color(152,145,78, 255);
    private Color enabledColor = new Color(255,245,114, 255);
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = transform.GetComponent<SpriteRenderer>();
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
