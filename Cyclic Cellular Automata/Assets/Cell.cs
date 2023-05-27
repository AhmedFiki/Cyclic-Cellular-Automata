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
    public Color color;
    public TextMesh text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMesh>();
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
        switch (c)
        {
            case 0:
                color = Color.white; 
                break;
                case 1:
                color = Color.red; break;
                case 2:
                color = Color.gray;
                break;
                case 3:
                color = Color.black;
                break;
                default: color = Color.gray; break;

        }

        GetComponent<SpriteRenderer>().color = color;
    }
    public void SetCellScale(float k)
    {
       gameObject.transform.localScale = new Vector3(k,k,k);
    }
    public void UpdateCell()
    {
        SetState(nextState);
        //SetNextState(nextState);
    }
    public void SetState(int s)
    {
        state = s;
        text.text = state + "";
        nextState = state;
        SetSpriteColor(s);
    }
    public void SetNextState(int n) {
        nextState = n;
    }public void IterateNextState() {
        nextState +=1;
    }
}
