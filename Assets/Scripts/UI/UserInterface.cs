using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUI
{


  public class UserInterface
  {

    //-----------------------------------------------------------------------------------
    public void Init()
    {
      
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
      //TODO: error
    }

  }
}