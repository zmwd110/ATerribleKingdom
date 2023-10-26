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
  public bool PauseAfterPlay;

  //该段播放了多少时间
  private float _playTime = 0;
  //该段动画对应的Director
  private PlayableDirector playableDirector;
  //内部是否在等待恢复播放
  private bool waitForResume = false;


  //每一帧都会调用的方法
  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    //只有整段不处在暂停过程中才显示
    if (playableDirector.state != PlayState.Paused)
    {
      _playTime += info.deltaTime;
      //UI显示出来
      MyUIManager.Instance.SetDialog(CharacterName, Content);
    }
  }


  //播放一段动画之后暂停
  public override void OnBehaviourPause(Playable playable, FrameData info)
  {
    
    //播放到结尾的时候
    if (_playTime!=0)
    {
      //清0播放时间
      _playTime = 0;

      //需要暂停
      if (PauseAfterPlay)
      {
        //进入等待恢复暂停的事件
        waitForResume = true;
        this.playableDirector.Pause();

        //显示暂停的文本
        MyUIManager.Instance.ShowPauseTxt();

        //暂停之后注册恢复暂停事件
        this.playableDirector.gameObject.GetComponent<TimelineBase>().Resume = ResumePlayable;
      }
      //继续往下播放
      else
      {
        MyUIManager.Instance.HideDialog();
      }
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

  }

  public override void OnGraphStop(Playable playable)
  {
    //Debug.Log("GraphStop");
  }

  public override void OnBehaviourPlay(Playable playable, FrameData info)
  {
    //Debug.Log("OnBehaviourPlay");
  }

  //恢复播放
  public void ResumePlayable()
  {
    if (waitForResume && Input.GetKeyDown(KeyCode.Space))
    {
      //恢复播放
      this.playableDirector.Play();
      //把窗口隐藏掉
      MyUIManager.Instance.HideDialog();
    }
  }

  public override void OnPlayableDestroy(Playable playable)
  {
    //Debug.Log("OnPlayableDestroy");
  }
}
