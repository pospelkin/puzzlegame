using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    List<string> highTimeList = new List<string>();
    public GameObject canvasMenu;
    public Text noRecordLabel;
    public GameObject mainScrollContent;
    public GameObject contentDataPanel;

    private void Start()
    {
        SetHighTimeData();
        HighTimeInitialized();
    }
    public void SetHighTimeData()
    {
        highTimeList = new List<string>()
        {
            {"03:00.00"},
            {"01:00.00"},
            {"09:00.00"},
            {"05:00.00"},
            {"08:00.00"},
            {"06:00.00"},
            {"07:00.00"},
            {"10:00.00"},
            {"02:00.00"},
            {"04:00.00"},
        };

    }
    void HighTimeInitialized()
    {
        if (highTimeList.Count > 0)
        {
            noRecordLabel.gameObject.SetActive(false);

            for (int i = 0; i < highTimeList.Count; i++)
            {
                string value = highTimeList[i];
                GameObject timeTextPanel = (GameObject)Instantiate(contentDataPanel);
                timeTextPanel.transform.SetParent(mainScrollContent.transform);
                timeTextPanel.transform.localScale = new Vector3(1, 1, 1);
                timeTextPanel.transform.Find("Text").GetComponent<Text>().text =(i+1) + ". " + value;

            }
        }
        else
        {
            noRecordLabel.gameObject.SetActive(true);
        }
    }

   
   
}
