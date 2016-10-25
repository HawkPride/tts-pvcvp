using UnityEngine;
using System.Collections;

namespace GUI
{
  public enum EGameState
  {
    MAIN_MENU,
    GAME_SINGLE,
    PLAYER_SCORE,
    MESSAGE_BOX,
  }

  public abstract class UIGameState : MonoBehaviour
  {
    public abstract EGameState GetStateType();


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
    public void DeleteLayout()
    {
      Destroy(gameObject);
    }
  }


}