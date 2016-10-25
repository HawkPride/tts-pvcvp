using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI
{


  public class UserInterface : MonoBehaviour
  {
    static UserInterface s_gui;
    
    public GameObject m_dlgMessageBox;

    //-----------------------------------------------------------------------------------
    public static UserInterface Instance()
    {
      return s_gui;
    }

    //-----------------------------------------------------------------------------------
    void Awake()
    {
      s_gui = this;
      DontDestroyOnLoad(gameObject);
    }

    //-----------------------------------------------------------------------------------
    void Start()
    {
      SwitchToState(EGameState.MAIN_MENU);
      
    }

    //-----------------------------------------------------------------------------------
    public void SwitchToState(EGameState eState)
    {
      string strSceneName = "";
      switch (eState)
      {
      case EGameState.MAIN_MENU:
        strSceneName = "MainMenu";
        break;
      case EGameState.GAME_SINGLE:
        strSceneName = "GameSingle";
        break;
      case EGameState.PLAYER_SCORE:
        strSceneName = "PlayerScore";
        break;
      case EGameState.MESSAGE_BOX:
        strSceneName = "PopUp";
        break;
      }

      if (strSceneName.Length > 0)
        SceneManager.LoadScene(strSceneName);
      //else
      //  Logger
    }

  }
}