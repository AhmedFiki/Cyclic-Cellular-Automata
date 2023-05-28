using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public Vector2Int size;
    public Cell[,] cells;
    public float cellSize = 1;
    public GameObject cellPrefab;

    public Camera camera;

    public int maxState = 2;
    public int minState = 0;
    public int range = 1;
    public int threshold = 2;
    public int neighborhoodCount;

    public bool play = false;
    public float playSpeed = 0.5f;

    public Color[] colorPalette;

    public Vector2Int testCellPos = Vector2Int.zero;
    //neighborhood mode;

    private void Start()
    {
        camera = Camera.main;
        cells = new Cell[size.x, size.y];
        neighborhoodCount = CalculateNeighborCount(range);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.G))
        {
            GenerateCells();
        }
        if (Input.GetKey(KeyCode.E))
        {
            Iterate();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            play = true;
            StartCoroutine(IteratorTimer());
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            play = false;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            UpdateCells();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            GetNeighborCells(cells[testCellPos.x, testCellPos.y]);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
        }
    }
    IEnumerator IteratorTimer()
    {
      
            Iterate();
            yield return new WaitForSeconds(playSpeed);
        
        if(play)
        StartCoroutine(IteratorTimer());

    }
    public void Iterate()
    {

        foreach (Cell cell in cells)
        {
           // Debug.Log(cell.GetPosition()+"  "+CheckNeighborCells(GetNeighborCells(cell), cell.state));

            if(CheckNeighborCells(GetNeighborCells(cell), cell.state) >= threshold)
            {
                
                    //Debug.Log(cell.GetPosition() + "Iterated" +cell.nextState);

                if(cell.state == maxState)
                {
                    cell.nextState = minState;
                }
                else
                {
                    cell.IterateNextState();

                }

               // Debug.Log(cell.GetPosition() + "Iterated" + cell.nextState);

            }
            else
            {
                cell.nextState = cell.state;
            }

        }
        UpdateCells();

    }
    void UpdateCells()
    {

        foreach (Cell cell in cells)
        {
            cell.UpdateCell();
        }


    }
    public int GetCellState(int x, int y)
    {
        return cells[x, y].state;
    }
    public int CalculateNeighborCount(int range)
    {
        //Moore neighborhood: Regular
        int count = 0;

        for (int i = 1; i <= range; i++)
        {
            count += i * 8;
        }
        return count;
    }
    public Cell[] GetNeighborCells(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();
        //Moore neighborhood: Regular
        //Von neighborhood: Cross-shaped

        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (i == 0 && j == 0)
                    continue;  // Skip the input cell itself

                if (cell.x + i >= size.x || cell.y + j >= size.y || cell.x + i < 0 || cell.y + j < 0)
                {
                    //Debug.Log("unreachable element");
                    continue;
                }
                cellsList.Add(cells[cell.x + i, cell.y + j]);
            }
        }
        return cellsList.ToArray();
    }

    public int CheckNeighborCells(Cell[] c, int current)
    {
        int count = 0;
        //Debug.Log(current+"< >"+c.Length) ;
        foreach (Cell cell in c)
        {
            if (cell.state == current + 1)
            {
                count++;
            }else if(cell.state == minState && current == maxState)
            {
                count++;
            }

        }

        return count;
    }


    public void GenerateCells()
    {
        UpdateCameraPosition();
        KillChildren();
        cells = new Cell[size.x, size.y];
        neighborhoodCount = CalculateNeighborCount(range);

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Vector2 pos = new Vector2(i, j);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);
                cell.transform.SetParent(transform);
                Cell cellScript = cell.GetComponent<Cell>();

                //generate random number per cell,  (CHANGE LATER)
                int randomNumber = Random.Range(0, maxState + 1);

                cellScript.SetPosition(i, j);
                cellScript.SetCellScale(cellSize);
                //Debug.Log("grid palette len: " + colorPalette.Length);
                cellScript.SetColorPalette(colorPalette);
                cellScript.SetState(randomNumber);
                
                cells[i, j] = cellScript;

            }
        }
    }

    public void KillChildren()
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;
            Destroy(child);
        }
    }

    public void LoadColorPalette(Color[] palette)
    {
        colorPalette = palette;
    }

    void UpdateCameraPosition()
    {
        float xOffset = size.x * 0.4f;

        camera.orthographicSize = size.x/2 +5;
        camera.transform.position = new Vector2(size.x/2 +xOffset ,size.y/2);

    }

}
