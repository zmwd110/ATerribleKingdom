using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

[ExecuteInEditMode]
public class TimelineBase : MonoBehaviour
{
  [SerializeField] PlayableDirector playableDirector;
  public Action Resume;


  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    Resume?.Invoke();
  }

}
