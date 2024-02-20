using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.UI;

public class CanvasControl : MonoBehaviour
{
    public GameObject blockButtonTemplate;

    public List<Texture2D> blockTextures;

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
        float Xcounter = -345;//blockButtonTemplate.GetComponent<RectTransform>().sizeDelta.x;

        foreach (GameObject block in blockList)
        {
            GameObject blockButton = Instantiate(blockButtonTemplate);
            blockButton.transform.SetParent(transform.Find("BlockPanel"));

            blockButton.name = block.name + "Button";

            RectTransform blockRect = blockButton.GetComponent<RectTransform>();
            blockRect.localScale = new Vector3(1, 1, 1);
            blockRect.localPosition = new Vector2(Xcounter, -15); //Why offset? IDK
            Xcounter += blockRect.sizeDelta.x + 5;

            BuildButtonBehavior blockScript = blockButton.GetComponent<BuildButtonBehavior>();
            blockScript.blockTemplate = block;

            TextMeshProUGUI blockText = blockButton.transform.Find("ButtonName").GetComponent<TextMeshProUGUI>();
            blockText.text = block.name;
        }
    }
}
