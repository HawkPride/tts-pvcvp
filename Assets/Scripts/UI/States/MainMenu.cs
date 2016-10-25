using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GUI
{

  public class MainMenu : UIGameState
  {

    public Canvas m_cnvScreen;


    //-----------------------------------------------------------------------------------
    public override EGameState GetStateType()
    {
      return EGameState.MAIN_MENU;
    }

    // Use this for initialization
    //-----------------------------------------------------------------------------------
    void Start()
    {

    }

    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    void Update()
    {


    }


    //-----------------------------------------------------------------------------------
    public void OnPlay()
    {
      UserInterface.Instance().SwitchToState(EGameState.GAME_SINGLE);
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      UserInterface.Instance().SwitchToState(EGameState.PLAYER_SCORE);
    }

    //-----------------------------------------------------------------------------------
    public void OnSettings()
    {

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