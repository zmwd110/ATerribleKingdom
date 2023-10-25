using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteInEditMode]
public class MyUIManager : MonoBehaviour
{
  [Header("角色对话")]
  [SerializeField] GameObject DialogueUI;
  [SerializeField] TextMeshProUGUI Content;
  [SerializeField] TextMeshProUGUI CharacterName;

  public static MyUIManager Instance { get; private set; }

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
  }

  private void OnEnable()
  {
    //编辑器内操作
#if UNITY_EDITOR
    if (Instance == null)
    {
      Instance = this;
    }
#endif
  }

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetDialog(string name, string content)
  {
    DialogueUI.SetActive(true);
    CharacterName.text = name;
    Content.text = content;
  }
}
