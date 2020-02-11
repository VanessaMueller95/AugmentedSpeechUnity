using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCCon : MonoBehaviour
{
    [HideInInspector] public string RemoteIP = "127.0.0.1"; //127.0.0.1 signifies a local host 
    [HideInInspector] public int SendToPort = 9000; //the port you will be sending from
    public int ListenerPort = 8080; //the port you will be listening on

    private Osc handler;
    private UDPPacketIO udp;

    public string posAdress;
    public string textAdress;

    public GameObject textInput;
    public GameObject posInput;

    private TextOSC textScript;
    private PositionOSC posScript;

    void Start()
    {
        udp = new UDPPacketIO();
        udp.init(RemoteIP, SendToPort, ListenerPort);
        handler = new Osc();
        handler.init(udp);
        handler.SetAllMessageHandler(AllMessageHandler);
        Debug.Log("OSC Connection initialized");

        textInput = GameObject.Find("OSCText");
        posInput = GameObject.Find("OSCPosition");
        textScript = textInput.GetComponent<TextOSC>();
        posScript = posInput.GetComponent<PositionOSC>();
    }

    void OnDisable()
    {
        udp.Close();
    }

    public void AllMessageHandler(OscMessage oscMessage)
    {
        string msgString = Osc.OscMessageToString(oscMessage); //the message and value combined
        string msgAddress = oscMessage.Address; //the message address
        Debug.Log(msgString);//log the message and values coming from OSC
        bool sentMessage = false;
        
        if (textAdress.Equals(msgAddress))
        {
            textScript.GetData(oscMessage);
            sentMessage = true;

        }
        if (posAdress.Equals(msgAddress))
        {
            posScript.GetData(oscMessage);
            sentMessage = true;

        }
        if (!sentMessage)
        {
            Debug.Log("OSC message with address " + msgAddress + " was not sent to any object");

        }

    }
}
