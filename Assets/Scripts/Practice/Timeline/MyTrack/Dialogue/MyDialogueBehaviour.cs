using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

[Serializable]
// A behaviour that is attached to a playable
public class MyDialogueBehaviour : PlayableBehaviour
{
  public string CharacterName;
  public string Content;

  //该段播放了多少时间
  private float _playTime = 0;
  //该段动画对应的Director
  private PlayableDirector playableDirector;
  private bool waitForResume = false;

  //每一帧都会调用的方法
  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    //UI显示出来
    MyUIManager.Instance.SetDialog(CharacterName,Content);
    _playTime += info.deltaTime;
  }

  //播放一段动画之后暂停
  public override void OnBehaviourPause(Playable playable, FrameData info)
  {
    //播放到结尾的时候
    if (_playTime!=0)
    {
      _playTime= 0;
      waitForResume = true;
      this.playableDirector.Pause();
    }
  }

  public override void OnGraphStart(Playable playable)
  {
    //Debug.Log("GraphStart");
  }

  public override void OnPlayableCreate(Playable playable)
  {
    //Debug.Log("PlayableCreate");
    this.playableDirector = playable.GetGraph().GetResolver() as PlayableDirector;
    //注册事件
    this.playableDirector.gameObject.GetComponent<TimelineBase>().Resume = ResumePlayable;
  }

  public override void OnGraphStop(Playable playable)
  {
    //Debug.Log("GraphStop");
  }

  public override void OnBehaviourPlay(Playable playable, FrameData info)
  {
    //Debug.Log("OnBehaviourPlay");
  }

  public void ResumePlayable()
  {
    if (waitForResume && Input.GetKeyDown(KeyCode.Space))
    {
      this.playableDirector.Play();
    }
  }
}
