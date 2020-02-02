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
    public string text = null;
    public Text uiText;
    private Transform target;

    void Start()
    {
        uiText = gameObject.transform.GetChild(0).GetComponent<Text>();
        uiText.text = "waiting...";
        target = GameObject.Find("center").transform;
    }

    void Update()
    {
        //Ausrichtung der Sprechblasen
        transform.LookAt(target, Vector3.up); ;
    }
}
