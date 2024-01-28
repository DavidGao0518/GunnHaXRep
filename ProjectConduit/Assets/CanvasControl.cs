using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasControl : MonoBehaviour
{
    public GameObject blockButtonTemplate;

    // Start is called before the first frame update
    void Start()
    {
        InitializeBuildList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WhenInputCommand(string cmd)
    {
        
    }
    void InitializeBuildList()
    {
        List<GameObject> blockList = GameObject.Find("Main Camera").GetComponent<MainScript>().blockList;
        float Xcounter = 50;

        foreach (GameObject block in blockList)
        {
            GameObject blockButton = Instantiate(blockButtonTemplate);
            blockButton.transform.parent = transform.Find("BlockPanel");

            blockButton.name = block.name + "Button";

            RectTransform blockRect = blockButton.GetComponent<RectTransform>();
            blockRect.localScale = new Vector3(1, 1, 1);
            blockRect.position = new Vector2(Xcounter, 50);
            Xcounter += blockRect.sizeDelta.x + 5;

            BuildButtonBehavior blockScript = blockButton.GetComponent<BuildButtonBehavior>();
            blockScript.blockTemplate = block;

            TextMeshProUGUI blockText = blockButton.transform.Find("ButtonName").GetComponent<TextMeshProUGUI>();
            blockText.text = block.name;
        }
    }
}
