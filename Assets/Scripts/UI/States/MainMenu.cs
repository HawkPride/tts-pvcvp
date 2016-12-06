using UnityEngine;
using System.Collections;

namespace GameGUI.States
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
      Game game = Game.Instance;
      if (game.AdsMan.ShouldShowAd())
        game.AdsMan.Show();
    }

    //-----------------------------------------------------------------------------------
    public override void OnUpdate()
    {

    }

    //-----------------------------------------------------------------------------------
    public override void OnEnd()
    {
      Game.Instance.AdsMan.CancelShow();
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
      MessageBox.Create(GetCanvas(), "Are you sure?", MessageBox.EType.OK_CANCEL, () => { Application.Quit(); });
    }

    //-----------------------------------------------------------------------------------
    public void OnTest()
    {
      Game.Instance.NetMan.Connect();

    }

  }
}