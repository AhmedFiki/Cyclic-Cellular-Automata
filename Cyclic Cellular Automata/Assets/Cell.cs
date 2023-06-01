using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Cell : MonoBehaviour
{
    public int x;
    public int y;
    public int state;
    public int nextState;
    public Color[] colorPalette;
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        nextState = state;
    }
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public Vector2Int GetPosition()
    {
        return new Vector2Int(x, y);
    }

    public void SetSpriteColor(int c)
    {
       
        spriteRenderer.color = colorPalette[c];

    }
    public void SetColorPalette(Color[] palette)
    {
        colorPalette = new Color[palette.Length];
        colorPalette = palette;
    }
    public void SetCellScale(float k)
    {
        gameObject.transform.localScale = new Vector3(k, k, k);
    }
    public void UpdateCell()
    {
        if(nextState > 7) { Debug.Log("FUCKIN UPDATE CELL"); }
        SetState(nextState);

    }
    public void SetState(int s)
    {
        state = s;
        nextState = state;
       /* if (s > colorPalette.Length - 1)
        {
            Debug.Log("s:" + s + ", cp.len:" + colorPalette.Length + ", cell:"+x+" "+y);
            return;
        }*/
        SetSpriteColor(s);
    }
    public void SetNextState(int n)
    {
        nextState = n;
    }
    public void IterateNextState()
    {
        nextState += 1;
    }
}
