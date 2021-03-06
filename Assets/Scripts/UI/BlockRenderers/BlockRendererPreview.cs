﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Math;

namespace GameGUI
{
  public class BlockRendererPreview : BlockRendererBase
  {

    //-----------------------------------------------------------------------------------
    // Update is called once per frame
    //-----------------------------------------------------------------------------------
    protected override void UpdateImpl()
    {
      Logic.GlassLocal glass = (Logic.GlassLocal)m_glass;
      //TODO: Move to Callback form glass
      if (glass == null || m_block == null)
        return;

      RectTransform tf = GetComponent<RectTransform>();

      const int PREVIEW_SIZE = 4;
      float fSizeX = tf.rect.width/PREVIEW_SIZE;
      float fSizeY = tf.rect.height/PREVIEW_SIZE;

      Logic.Figure fig = glass.nextFigure;
      var mtx = fig.matrix;
      VecInt2 vCenter = fig.GetCenterPoint();

      for (int x = 0; x < PREVIEW_SIZE; x++)
      {
        if (x > mtx.GetLength(0) - 1)
          continue;
        for (int y = 0; y < PREVIEW_SIZE; y++)
        {
          if (y > mtx.GetLength(1) - 1)
            continue;
          int nVal = mtx[x, y];
          if (nVal != 0)
          {
            GameObject block = GetNextBlock();
            //Vector3 vPos = new Vector3(fSizeX*(x - fCenterX), fSizeY*(y - fCenterY), 0.0f);

            RectTransform btf = block.GetComponent<RectTransform>();

            btf.anchoredPosition = new Vector3(fSizeX * (x - vCenter.x), fSizeY * (y - vCenter.y));
            btf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, fSizeX);
            btf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, fSizeY);
          }
        }
      }
    }

  }


}