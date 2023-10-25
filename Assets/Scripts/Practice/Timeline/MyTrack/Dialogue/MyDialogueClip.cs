using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class MyDialogueClip : PlayableAsset, ITimelineClipAsset
{
  public ClipCaps clipCaps { get; }

  public MyDialogueBehaviour Template;

  // Factory method that generates a playable based on this asset
  public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
  {
    var playable = ScriptPlayable<MyDialogueBehaviour>.Create(graph, Template);
    
    return playable;
  }
}
