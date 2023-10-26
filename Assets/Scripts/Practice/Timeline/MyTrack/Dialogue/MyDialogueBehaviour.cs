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

  //�öβ����˶���ʱ��
  private float _playTime = 0;
  //�öζ�����Ӧ��Director
  private PlayableDirector playableDirector;
  //�ڲ��Ƿ��ڵȴ��ָ�����
  private bool waitForResume = false;


  //ÿһ֡������õķ���
  public override void ProcessFrame(Playable playable, FrameData info, object playerData)
  {
    //ֻ�����β�������ͣ�����в���ʾ
    if (playableDirector.state != PlayState.Paused)
    {
      _playTime += info.deltaTime;
      //UI��ʾ����
      MyUIManager.Instance.SetDialog(CharacterName, Content);
    }
  }


  //����һ�ζ���֮����ͣ
  public override void OnBehaviourPause(Playable playable, FrameData info)
  {
    
    //���ŵ���β��ʱ��
    if (_playTime!=0)
    {
      //��0����ʱ��
      _playTime = 0;

      //��Ҫ��ͣ
      if (PauseAfterPlay)
      {
        //����ȴ��ָ���ͣ���¼�
        waitForResume = true;
        this.playableDirector.Pause();

        //��ʾ��ͣ���ı�
        MyUIManager.Instance.ShowPauseTxt();

        //��֮ͣ��ע��ָ���ͣ�¼�
        this.playableDirector.gameObject.GetComponent<TimelineBase>().Resume = ResumePlayable;
      }
      //�������²���
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

  //�ָ�����
  public void ResumePlayable()
  {
    if (waitForResume && Input.GetKeyDown(KeyCode.Space))
    {
      //�ָ�����
      this.playableDirector.Play();
      //�Ѵ������ص�
      MyUIManager.Instance.HideDialog();
    }
  }

  public override void OnPlayableDestroy(Playable playable)
  {
    //Debug.Log("OnPlayableDestroy");
  }
}
