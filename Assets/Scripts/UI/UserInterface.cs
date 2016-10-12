using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI
{
  public class UserInterface : MonoBehaviour
  {
    static UserInterface s_gui;

    public string     m_strStartScene;
    public GameObject m_dlgMessageBox;


    //-----------------------------------------------------------------------------------
    void Awake()
    {
      s_gui = this;
      DontDestroyOnLoad(gameObject);
    }

    //-----------------------------------------------------------------------------------
    void Start()
    {
      SceneManager.LoadScene(m_strStartScene);
    }

    //-----------------------------------------------------------------------------------
    public static UserInterface Instance()
    {
      return s_gui;
    }
  }
}