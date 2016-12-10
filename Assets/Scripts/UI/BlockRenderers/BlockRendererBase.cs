using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameGUI
{
  public abstract class BlockRendererBase : MonoBehaviour
  {
    //Fields
    protected List<GameObject> m_arVisibleBlocks = new List<GameObject>();
    protected List<GameObject> m_arFreeBlocks    = new List<GameObject>();

    //Public
    public GameObject m_block = null;
    public Glass      m_glass = null;



    //-----------------------------------------------------------------------------------
    // Use this for initialization
    //-----------------------------------------------------------------------------------
    protected virtual void Start()
    {
    }


    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    protected virtual void Update()
    {
      //Make all blocks hidden
      m_arFreeBlocks.AddRange(m_arVisibleBlocks);
      m_arVisibleBlocks.Clear();

      //Process child's logic
      UpdateImpl();

      //Show new visble blocks
      for (int i = 0; i < m_arVisibleBlocks.Count; i++)
        m_arVisibleBlocks[i].GetComponent<Image>().enabled = true;
      for (int i = 0; i < m_arFreeBlocks.Count; i++)
        m_arFreeBlocks[i].GetComponent<Image>().enabled = false;

    }


    //-----------------------------------------------------------------------------------
    //Process real update tasks, that uses block cache
    //-----------------------------------------------------------------------------------
    protected abstract void UpdateImpl();



    //-----------------------------------------------------------------------------------
    //Get one of a cached blocks or create a new one
    //-----------------------------------------------------------------------------------
    protected virtual GameObject GetNextBlock()
    {
      GameObject newRend;
      if (m_arFreeBlocks.Count == 0)
      {
        if (!m_block)
          return null;
        newRend = Object.Instantiate(m_block);
        newRend.transform.SetParent(gameObject.transform, false);
      }
      else
      {
        int nLast = m_arFreeBlocks.Count - 1;
        newRend = m_arFreeBlocks[nLast];
        m_arFreeBlocks.RemoveAt(nLast);
      }

      m_arVisibleBlocks.Add(newRend);
      return newRend;
    }
  }
}