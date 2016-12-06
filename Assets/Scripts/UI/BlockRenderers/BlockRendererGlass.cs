using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GameGUI
{
  public class BlockRendererGlass : BlockRendererBase
  {
    public Glass      m_glass;
    public Text       m_pointsText;


    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    protected override void UpdateImpl()
    {
      //TODO: Move to Callback form glass
      if (!m_glass || !m_block)
        return;

      RectTransform tf = GetComponent<RectTransform>();

      var field = m_glass.Field;
      float fCenterX = m_glass.m_nSizeX/2.0f;
      float fCenterY = m_glass.m_nSizeY/2.0f;
      float fSizeX = tf.rect.width/m_glass.m_nSizeX;
      float fSizeY = tf.rect.height/m_glass.m_nSizeY;
      for (int x = 0; x < m_glass.m_nSizeX; x++)
      {
        for (int y = 0; y < m_glass.m_nSizeY; y++)
        {
          Block fieldBlock = field[x, y];
          if (fieldBlock != null)
          {
            GameObject block = GetNextBlock();
            //Vector3 vPos = new Vector3(fSizeX*(x - fCenterX), fSizeY*(y - fCenterY), 0.0f);

            RectTransform btf = block.GetComponent<RectTransform>();

            btf.anchoredPosition = new Vector3(fSizeX * (x - fCenterX), fSizeY * (y - fCenterY));
            btf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fSizeX);
            btf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fSizeY);
            if (fieldBlock.m_eType == Block.EType.COLLAPSING)
              block.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f);
            else
              block.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f);
          }
        }
      }

      if (m_pointsText)
        m_pointsText.text = m_glass.Score.ToString();
    }
  }
}