using UnityEngine;
using System.Collections;

namespace GUI.States
{
  //Creation params
  public class MainMenuParams : GameStateParams
  {
    public override string GetSceneName() { return "MainMenu"; }
    public override EGameStateType GetStateType() { return EGameStateType.MAIN_MENU; }
  }



  public class MainMenu : GameState
  {

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.MAIN_MENU;
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
      Game.Instance.Ui.SwitchToState(new GameSingleParams());
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(1));
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