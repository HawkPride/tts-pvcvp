using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GUI
{

  public class MainMenu : MonoBehaviour
  {

    public Canvas m_cnvScreen;

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
      SceneManager.LoadScene("GameSingle");
    }

    //-----------------------------------------------------------------------------------
    public void OnScore()
    {
      MessageBox.Create(m_cnvScreen, "HELLO WORLD", MessageBox.EType.OK, OnTest);
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

    }
  }
}