using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class ServerHandler : MonoBehaviour
{
  public GameObject Cube;
  SocketIOComponent socket;
  // Start is called before the first frame update
  bool active = false;
  void Start()
  {
    socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
    Cube.SetActive(active);
    socket.On("message", onMessageEvent);
  }

  // Update is called once per frame
  void Update()
  {

  }

  void onMessageEvent(SocketIOEvent evt)
  {
    Debug.Log("Message: " + evt.data.GetField("message"));
    Cube.SetActive(active);
    active = !active;
  }
}
