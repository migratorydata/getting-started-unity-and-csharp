using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using com.migratorydata.client;
using UnityEngine.UI;


public class Client : MonoBehaviour
{

    private static readonly string SERVER = "127.0.0.1:8800";
    private static readonly string TOKEN = "some-token";
    private static readonly string SUBJECT = "/server/status";

    public Text statusTextField;
    public Text messageTextField;

    public Button publishButton;

    TextToDisplay messageContainer = new TextToDisplay();
    TextToDisplay statusContainer = new TextToDisplay();

    int count = 0;

    public MigratoryDataClient client;

    // Start is called before the first frame update
    void Start()
    {
        statusTextField = GameObject.Find("Status Update").GetComponent<Text>();
        messageTextField = GameObject.Find("Messages Update").GetComponent<Text>();

        publishButton = GameObject.Find("PublishButton").GetComponent<Button>();
        publishButton.onClick.AddListener(() => PublishMessage());

        // initialize the MigratoryData client
        client = new MigratoryDataClient();
        client.SetLogListener(new LogList(), MigratoryDataLogLevel.DEBUG);
        client.SetListener(new Listener(statusContainer, messageContainer));

        // set the entitlement token and the servers
        client.SetEntitlementToken(TOKEN);
        client.SetServers(new string[] { SERVER });

        // subscribe to the subject "/server/status"
        client.Subscribe(new List<string>() { SUBJECT });

        // connect to the MigratoryData server
        client.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        statusTextField.text = statusContainer.Text;

        messageTextField.text = messageContainer.Text;
    }

	void PublishMessage(){
        client.Publish(new MigratoryDataMessage("/server/status", Encoding.UTF8.GetBytes("Hello from Unity " + count++)));

		Debug.Log ("You have clicked the publish button!");
	}

    class Listener : MigratoryDataListener
    {
        private TextToDisplay status;
        private TextToDisplay message;

        public Listener(TextToDisplay status, TextToDisplay message)
        {
            this.status = status;
            this.message = message;
        }

        public void OnMessage(MigratoryDataMessage pushMessage)
        {
            Debug.Log(pushMessage.ToString());

            this.message.Text = pushMessage.ToString();
        }

        public void OnStatus(string status, string info)
        {
            Debug.Log(status + " " + info);

            this.status.Text = status + " " + info;
        }
    }
    class LogList : MigratoryDataLogListener
    {
        public void OnLog(string log, MigratoryDataLogLevel level)
        {
            string msg = string.Format("[{0:G}] [{1}] {2}", DateTime.Now, level, log);
            Debug.Log(msg);
        }
    }

    class TextToDisplay
    {
        private string text;

        public string Text { get => text; set => text = value; }
    }
}
