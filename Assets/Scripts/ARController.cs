using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using SocketIO;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ARController : MonoBehaviour
{

  public GameObject GridPrefab;
  public GameObject ARCamera;
  public GameObject Portal;
  public TextMeshProUGUI TextElement;
  private List<DetectedPlane> m_NewDetectedPlanes = new List<DetectedPlane>();
  SocketIOComponent socket;
  private Queue<string> MessageQueue;

  // Start is called before the first frame update
  void Start()
  {
    socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
    MessageQueue = new Queue<string>();
    socket.On("message", onMessageEvent);
  }

  // Update is called once per frame
  void Update()
  {
    if (Session.Status != SessionStatus.Tracking)
    {
      return;
    }

    Session.GetTrackables<DetectedPlane>(m_NewDetectedPlanes, TrackableQueryFilter.New);

    for (int i = 0; i < m_NewDetectedPlanes.Count; i++)
    {
      GameObject grid = Instantiate(GridPrefab, Vector3.zero, Quaternion.identity, transform);

      grid.GetComponent<GridVisualizer>().Initialize(m_NewDetectedPlanes[i]);
    }

    Touch touch;
    if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
    {
      return;
    }

    TrackableHit hit;
    if (Frame.Raycast(touch.position.x, touch.position.y, TrackableHitFlags.PlaneWithinPolygon, out hit))
    {
      Portal.SetActive(true);

      Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);
      Portal.transform.position = hit.Pose.position;
      Portal.transform.rotation = hit.Pose.rotation;

      Vector3 cameraPosition = ARCamera.transform.position;
      cameraPosition.y = hit.Pose.position.y;

      Portal.transform.LookAt(cameraPosition, Portal.transform.up);

      Portal.transform.parent = anchor.transform;
    }
  }

  void onMessageEvent(SocketIOEvent evt)
  {
    Debug.Log("Message: " + evt.data.GetField("message"));
    string jsonData = evt.data.GetField("message").ToString();
    string text = jsonData.Substring(1, jsonData.Length - 2);
    MessageQueue.Enqueue(text);
    // Debug.Log("Queue: " + MessageQueue.Count + "TextElement: " + TextElement);
    displayText();
  }

  void displayText()
  {
    while (MessageQueue.Count > 0)
    {
      TextElement.text += MessageQueue.Dequeue() + " ";
    }
  }

}