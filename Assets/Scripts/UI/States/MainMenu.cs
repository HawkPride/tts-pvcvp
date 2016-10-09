using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

  public Canvas m_cnvScreen;

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {


  }

  public void OnPlay()
  {
    SceneManager.LoadScene("GameSingle");
  }

  public void OnScore()
  {
    GUI.MessageBox(m_cnvScreen, "HELLO WORLD");
  }

  public void OnSettings()
  {

  }

  public void OnQuit()
  {
    Application.Quit();
  }
}
