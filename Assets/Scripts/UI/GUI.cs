using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
  static GUI s_gui;

  public string     m_strStartScene;
  public GameObject m_dlgMessageBox;


  void Awake()
  {
    s_gui = this;
    DontDestroyOnLoad(this);
  }

  void Start()
  {
    SceneManager.LoadScene(m_strStartScene);
  }

  public static GUI Instance()
  {
    return s_gui;
  }

  public static void MessageBox(Canvas target, string message)
  {
    GUI gui = Instance();

    if (!gui.m_dlgMessageBox)
      return;

    GameObject msgBox = GameObject.Instantiate<GameObject>(gui.m_dlgMessageBox);
    msgBox.transform.SetParent(target.transform, false);
    Text txt = msgBox.GetComponentInChildren<Text>();
    txt.text = message;
    //msgBox.GetComponent<Anima>()
  }
}
