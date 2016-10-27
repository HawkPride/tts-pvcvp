using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GUI
{

  public class MainMenu : UIGameState
  {

    //-----------------------------------------------------------------------------------
    public override EGameState GetStateType()
    {
      return EGameState.MAIN_MENU;
    }

    //-----------------------------------------------------------------------------------
    public override void OnStart()
    {
      
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {
      

    }


    //-----------------------------------------------------------------------------------
    public void OnPlay()
    {
      Game.Instance.Ui.SwitchToState(EGameState.GAME_SINGLE);
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      Game.Instance.Ui.SwitchToState(EGameState.PLAYER_SCORE);
    }

    //-----------------------------------------------------------------------------------
    public void OnSettings()
    {
      MessageBox.Create(GetCanvas(), "Hello world", MessageBox.EType.OK, OnTest);
    }

    //-----------------------------------------------------------------------------------
    public void OnQuit()
    {
      Application.Quit();
    }

    //-----------------------------------------------------------------------------------
    public void OnTest()
    {
      StatsProviderBase sp = new StatsProviderLocal();
      StatsProviderBase.Stats stats = new StatsProviderBase.Stats();
      stats.m_strPlayerName = "Hawk";
      stats.m_nScore = 100;
      sp.AddStats(stats);
    }

  }
}