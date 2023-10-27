using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
  //虚拟跟随点
  [SerializeField]private Transform DummyFollow;

  //移动速度
  [SerializeField]float speed = 10f;


  // Update is called once per frame
  void Update()
  {
    var horizontal = Input.GetAxis("CameraHorizontal");
    var vertical = Input.GetAxis("CameraVertical");

    DummyFollow.position += new Vector3 (vertical, 0, -horizontal) * (Time.deltaTime * speed);
  }
}
