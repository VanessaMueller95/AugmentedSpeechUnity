using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bubble : MonoBehaviour
{
    public int activeID = 0;
    public int firstTimestamp;
    public int lastActiveTimestamp;
    public bool active = false;
    public string text;
    public Text uiText;

    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.transform.GetChild(0).GetComponent<Text>();
        target = GameObject.Find("center").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.up); ;


        if (active && text != null)
        {
            uiText.text = text;
        }
        else if (active && text == null)
        {
            uiText.text = "waiting.....";
        }

        if (!active)
        {
            firstTimestamp = 0;
            lastActiveTimestamp = 0;
            text = null;
            uiText.text = "...";
        }

    }
}
