using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextOSC : MonoBehaviour
{
    private OscMessage localMsg;

    public float pakageID;
    public float firstPakageTimestamp;
    public float lastPakageTimestamp;
    public string text;

    public string mood;

    bool dataReceived = false;

    //beeinhaltet alle aktiven Sprechblasen
    private List<GameObject> bubbles = new List<GameObject>();

    private GameObject posOSC;


    // Start is called before the first frame update
    void Start()
    {
        posOSC = GameObject.Find("OSCPosition");
    }

    // Update is called once per frame
    void Update()
    {
        if (dataReceived)
        {
            //übernimmt die Sprechblasenliste aus dem Positions-Skript
            bubbles = posOSC.GetComponent<PositionOSC>().bubbles;

            //geht jeder Sprechblase durch und sucht die ID der Textnachricht
            foreach (GameObject b in bubbles)
            {
                bubble script = b.GetComponent<bubble>();
                if (script.activeID == pakageID) {
                    script.text = text;
                    script.uiText.text = text;

                    //Möglichkeit eine Stimmung einzubringen
                    /*
                    if (mood == "angry")
                    {
                        Debug.Log(mood);
                        b.GetComponent<Image>().color = new Color(255f,0f,0f,0.5f);
                    }*/
                }
            }
            dataReceived = false;
        }
    }

    //Speicherung der OSC Inhalte in Variablen
    public void GetData( OscMessage value )
    {
        localMsg = value;

        pakageID = float.Parse(localMsg.Values[0].ToString());
        firstPakageTimestamp = float.Parse(localMsg.Values[1].ToString());
        lastPakageTimestamp = float.Parse(localMsg.Values[2].ToString());
        text = localMsg.Values[3].ToString();

        dataReceived = true;
    }
}
