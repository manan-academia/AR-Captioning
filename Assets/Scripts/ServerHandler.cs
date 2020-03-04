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

    TextElement = Screen.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMesh>();
  }

  // Update is called once per frame
  void Update()
  {

  }

}
