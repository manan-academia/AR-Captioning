using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using TMPro;

public class ServerHandler : MonoBehaviour
{
  private GameObject Screen;
  SocketIOComponent socket;
  bool active = false;
  private Queue<string> MessageQueue;
  private TextMesh TextElement;

  private void Awake()
  {
    Screen = GetComponent<GameObject>();
  }

  // Start is called before the first frame update
  void Start()
  {
    socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
    MessageQueue = new Queue<string>();
    socket.On("message", onMessageEvent);
    TextElement = Screen.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMesh>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void onMessageEvent(SocketIOEvent evt)
  {
    Debug.Log("Message: " + evt.data.GetField("message"));
    MessageQueue.Enqueue(evt.data.GetField("message").ToString());
    // active = !active;
    Debug.Log("Queue: " + MessageQueue.Count + "TextElement: " + TextElement);
    displayText();
  }

  void displayText()
  {
    while (MessageQueue.Count > 0)
    {
      // TextElement.text += (MessageQueue.Dequeue() + " ");
    }
  }
}
