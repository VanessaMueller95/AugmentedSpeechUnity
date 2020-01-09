using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOSC : MonoBehaviour
{
    private OscMessage localMsg;

    public float xPos;
    public float yPos;
    public float zPos;
    public int id;
    public float activity;
    public string tag;
    public int timestamp;

    //public GameObject[] bubbles;
    public List<GameObject> bubbles = new List<GameObject>();

    private int mul = 10;
    private bool newID = true;

    private bool newData;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (newData)
        {
            newID = true;

            //Testet ob Bubbles freigegeben werden können
            foreach (GameObject b in bubbles)
            {
                bubble script = b.GetComponent<bubble>();
                if (script.lastActiveTimestamp <= timestamp - 5000)
                {
                    //script.active = false;
                    bubbles.Remove(b);
                    Destroy(b);
                    Debug.Log(bubbles.Count);
                }
            }

            // Testet ob die ID bereits in einer Bubble hinterlegt ist
            foreach (GameObject activeBubble in bubbles)
            {
                bubble script = activeBubble.GetComponent<bubble>();
                if (id == script.activeID)
                {
                    script.lastActiveTimestamp = timestamp;
                    newID = false;
                    activeBubble.transform.position = new Vector3(xPos * mul, 0, yPos * mul);
                    newData = false;
                    break;
                }
            }

            //geht weiter wenn es sich um eine neue id handelt
            if (newID)
            {
                bool stop = false;

                //Test ob die ID einer anderen sehr nah ist von der Position her und überschreibt dann die alte 
                foreach (GameObject activeBubble in bubbles)
                {
                    bubble script = activeBubble.GetComponent<bubble>();

                    //misst die Distanz zwischen der neuen und bereits existierenden Punkten und weißt neue IDs ggf. alten zu, wenn diese nah aneinander liegen
                    Debug.Log(Vector3.Distance(activeBubble.transform.position, new Vector3(xPos * mul, zPos * mul, yPos * mul)));
                    if (Vector3.Distance(activeBubble.transform.position, new Vector3(xPos * mul, 0, yPos * mul)) < 1)
                    {
                        script.activeID = id;
                        newData = false;
                        stop = true;
                        break;
                    }
                }

                //Instantziiert neue Bubble
                if (!stop)
                {
                    GameObject newBubble = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(90f, 0f, 0f)) as GameObject;
                    bubble script = newBubble.GetComponent<bubble>();
                    script.active = true;
                    script.activeID = id;
                    script.lastActiveTimestamp = timestamp;
                    script.firstTimestamp = timestamp;
                    newBubble.transform.position = new Vector3(xPos * mul, 0, yPos * mul);
                    bubbles.Add(newBubble);
                    newData = false;

                    Debug.Log(bubbles);
                    Debug.Log(bubbles.Count);
                }
            }
        }

     }

    public void GetData(OscMessage value)
    {

        localMsg = value;

        timestamp = int.Parse(localMsg.Values[0].ToString());
        id = int.Parse(localMsg.Values[1].ToString());
        xPos = float.Parse(localMsg.Values[2].ToString());
        yPos = float.Parse(localMsg.Values[3].ToString());
        zPos = float.Parse(localMsg.Values[4].ToString());
        activity = float.Parse(localMsg.Values[5].ToString());
        tag = localMsg.Values[6].ToString();

        newData = true;
    }
}
