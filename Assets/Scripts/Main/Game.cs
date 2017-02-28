using UnityEngine;
using System.Collections;
using GameGUI;

public class Game
{
  public static Game instance { get { if (s_game == null) CreateGame(); return s_game; } }

  public UserInterface ui { get { return m_ui; } }
  public Data.GameData config() { return m_gameData;  }

  public GameResults results { get { return m_results; } set { m_results = value; } }

  public StatsProviderBase stats { get { return m_stats; } }

  public Ads.AdsManager adsMan { get { return m_adsMan; } }

  public Net.NetManager netMan { get { return m_netMan; } }

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

    m_stats = new StatsProviderLocal();
    m_stats.Load();


    if (m_stats.GetConfig().m_strPlayerName.Length == 0)
    {
      m_stats.GetConfig().m_strPlayerName = "Player_" + UnityEngine.Random.Range(1000, 9000000).ToString();
      m_stats.Save();
    }

    m_adsMan = new Ads.AdsManager();
    m_adsMan.Init();

    m_netMan = new Net.NetManager();
    m_netMan.Init();

    m_ui = new UserInterface();
    m_ui.Init();


    m_ui.SwitchToState(new GameGUI.States.MainMenuParams());
  }

  //-----------------------------------------------------------------------------------
  public void Update()
  {
    m_adsMan.Update();
    m_netMan.Update();
  }




  //Members
  //-----------------------------------------------------------------------------------
  static Game s_game;

  UserInterface     m_ui;
  Data.GameData     m_gameData;
  StatsProviderBase m_stats;
  Ads.AdsManager    m_adsMan;
  Net.NetManager    m_netMan;

  GameResults       m_results;
}
