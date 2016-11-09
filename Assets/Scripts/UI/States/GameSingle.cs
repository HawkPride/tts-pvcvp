using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GUI.States
{
  //Creation params
  public class GameSingleParams : GameStateParams
  {
    public override string          GetSceneName() { return "GameSingle"; }
    public override EGameStateType  GetStateType() { return EGameStateType.GAME_SINGLE; }
  }




  public class GameSingle : GameState
  {

    //-----------------------------------------------------------------------------------
    public override EGameStateType GetStateType()
    {
      return EGameStateType.GAME_SINGLE;
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
    public void OnGameEnd()
    {
      GameResults res = new GameResults();
      res.Score = 100;
      Game.Instance.GameResults = res;
      Game.Instance.Ui.SwitchToState(new PlayersScoreParams(10));
    }
  }
}