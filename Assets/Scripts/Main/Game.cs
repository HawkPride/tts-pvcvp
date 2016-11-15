using UnityEngine;
using System.Collections;
using GUI;

public class Game
{
  public static Game Instance { get { if (s_game == null) CreateGame(); return s_game; } }

  public UserInterface Ui { get { return m_ui; } }
  public Data.GameData GetConfig() { return m_gameData;  }

  public GameResults Results { get { return m_results; } set { m_results = value; } }

  //-----------------------------------------------------------------------------------
  public static void CreateGame()
  {
    if (s_game == null)
    {
      s_game = new Game();
    }
    else
    {
      //TODO: error
    }
  }

  //-----------------------------------------------------------------------------------
  public void Init(Data.GameData gameData)
  {
    m_gameData = gameData;

    m_ui = new UserInterface();
    m_ui.Init();


    m_ui.SwitchToState(new GUI.States.MainMenuParams());
  }

  //-----------------------------------------------------------------------------------
  public void Update()
  {

  }




  //Members
  //-----------------------------------------------------------------------------------
  static Game s_game;

  UserInterface   m_ui;
  Data.GameData   m_gameData;

  GameResults     m_results;
}
