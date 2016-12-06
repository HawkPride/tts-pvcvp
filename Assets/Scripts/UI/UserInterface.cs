using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameGUI
{


  public class UserInterface
  {

    //-----------------------------------------------------------------------------------
    public void Init()
    {
      
    }

    //-----------------------------------------------------------------------------------
    public void SwitchToState(States.GameStateParams stateParams)
    {
      string strSceneName = stateParams.GetSceneName();
      if (strSceneName.Length > 0)
        SceneManager.LoadScene(strSceneName);
      //else
      //TODO: error
    }

  }
}