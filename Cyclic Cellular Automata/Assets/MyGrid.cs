using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MyGrid : MonoBehaviour
{
    public Vector2Int size;
    public Cell[,] cells;
    public float cellSize = 1;
    public GameObject cellPrefab;

    public bool gridVisible=false;

    public Camera camera;

    public int maxState = 2;
    public int minState = 0;
    public int range = 1;
    public int threshold = 2;
    public int neighborhoodCount;

    public int neighborhood = 0;
    public bool warp = false;
    public Toggle warpToggle;
    public bool play = false;
    public float playSpeed = 0.5f;

    public Color[] colorArray;

    public Vector2Int testCellPos = Vector2Int.zero;

    public ColorsPanel colorsPanel;

    [Header("Sliders")]

    public Slider gridSizeSlider;
    public TMP_Text gridSizeText;

    public Slider speedSlider;
    public TMP_Text speedText;

    public Slider statesSlider;
    public TMP_Text statesText;

    public Slider thresholdSlider;
    public TMP_Text thresholdText;

    public Slider rangeSlider;
    public TMP_Text rangeText;

    public TMP_Dropdown neighborhoodDropdown;

    private void Start()
    {
        camera = Camera.main;
        cells = new Cell[size.x, size.y];
        neighborhoodCount = CalculateNeighborCount(range);

        gridSizeSlider.onValueChanged.AddListener(SetGridSizeFromSlider);
        speedSlider.onValueChanged.AddListener(SetSpeedFromSlider);
        statesSlider.onValueChanged.AddListener(SetStatesFromSlider);
        thresholdSlider.onValueChanged.AddListener(SetThresholdFromSlider);
        rangeSlider.onValueChanged.AddListener(SetRangeFromSlider);
        neighborhoodDropdown.onValueChanged.AddListener(SetNeighborhoodFromDropdown);
    }

    IEnumerator IteratorTimer()
    {

        Iterate();
        yield return new WaitForSeconds(playSpeed);

        if (play)
            StartCoroutine(IteratorTimer());

    }
    public void Play()
    {
        play = true;
        StartCoroutine(IteratorTimer());
    }
    public void Pause()
    {
        play = false;

    }
    public void Iterate()
    {

        foreach (Cell cell in cells)
        {
            if(HandleThreshold(neighborhood, cell))
            {
                if (cell.state == maxState)
                {
                    cell.nextState = minState;
                }
                else
                {
                    cell.IterateNextState();
                }
            }
            else
            {
                cell.nextState = cell.state;
            }
        }
        UpdateCells();

    }
    bool HandleThreshold(int neighborhood, Cell cell)
    {
        switch (neighborhood)
        {
            case 0:
                return CheckNeighborCells(GetNeighborCellsMoore(cell), cell.state) >= threshold;
                break;
            case 1:
                return CheckNeighborCells(GetNeighborCellsCross(cell), cell.state) >= threshold;
                break;
            case 2:
                return CheckNeighborCells(GetNeighborCellsCustom(cell), cell.state) >= threshold;
                break;
            case 3:
                return CheckNeighborCells(GetNeighborCellsRemoteMoore(cell), cell.state) >= threshold;
                break;
            case 4:
                return CheckNeighborCells(GetNeighborCellsVonNeumann(cell), cell.state) >= threshold;
                break;
            case 5:
                return CheckNeighborCells(GetRemoteNeighborCellsVonNeumann(cell), cell.state) >= threshold;
                break;
            case 6:
                return CheckNeighborCells(GetNeighborCellsS(cell), cell.state) >= threshold;
                break;
            case 7:
                return CheckNeighborCells(GetNeighborCellsBlade(cell), cell.state) >= threshold;
                break;
            case 8:
                return CheckNeighborCells(GetNeighborCellsCorners(cell), cell.state) >= threshold;
                break;
            case 9:
                return CheckNeighborCells(GetNeighborCellsTickMark(cell), cell.state) >= threshold;
                break;
            default: 
                return false;
        }
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
        int count = 0;
        for (int i = 1; i <= range; i++)
        {
            count += i * 8;
        }
        return count;
    }

    public Cell[] GetNeighborCellsTickMark(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Blade Neighborhood
        for (int i = 1; i <= range; i++)
        {
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);
            AddNeighborCell(cellsList, cell.x - i, cell.y - i);
            AddNeighborCell(cellsList, cell.x + i, cell.y - i);
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);
        }

        // Remote Moore Neighborhood
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (Mathf.Abs(i) == range || Mathf.Abs(j) == range)
                {
                    AddNeighborCell(cellsList, cell.x + i, cell.y + j);
                }
            }
        }

        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsCorners(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Check the +x, +y direction
        for (int i = 1; i <= range; i++)
        {
            for (int j = i; j >= 1; j--)
            {
                AddNeighborCell(cellsList, cell.x + j, cell.y + i);
            }
        }

        // Check the -x, -y direction
        for (int i = 1; i <= range; i++)
        {
            for (int j = i; j >= 1; j--)
            {
                AddNeighborCell(cellsList, cell.x - j, cell.y - i);
            }
        }

        // Check the +x, -y direction
        for (int i = 1; i <= range; i++)
        {
            for (int j = i; j >= 1; j--)
            {
                AddNeighborCell(cellsList, cell.x + j, cell.y - i);
            }
        }

        // Check the -x, +y direction
        for (int i = 1; i <= range; i++)
        {
            for (int j = i; j >= 1; j--)
            {
                AddNeighborCell(cellsList, cell.x - j, cell.y + i);
            }
        }

        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsBlade(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Check the diagonal directions
        for (int i = 1; i <= range; i++)
        {
            // Upper-right diagonal
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);

            // Upper-left diagonal
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);

            // Lower-right diagonal
            AddNeighborCell(cellsList, cell.x + i, cell.y - i);

            // Lower-left diagonal
            AddNeighborCell(cellsList, cell.x - i, cell.y - i);
        }

        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsS(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Iterate over the cells in the positive quadrant
        for (int i = 1; i <= range; i++)
        {
            // +x, +y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);

            // -x, +y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);
        }

        // Iterate over the cells in the negative quadrant
        for (int i = -1; i >= -range; i--)
        {
            // +x, -y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);

            // -x, -y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);
        }

        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsCustom(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Custom neighborhood: Random positions within the range (-7,7) to (7,-7)
        int minOffset = -range;
        int maxOffset = range;

        for (int i = 0; i < range; i++)
        {
            int offsetX = Random.Range(minOffset, maxOffset + 1);
            int offsetY = Random.Range(minOffset, maxOffset + 1);

            // Apply wrapping if enabled
            if (warp)
            {
                offsetX = (cell.x + offsetX + size.x) % size.x;
                offsetY = (cell.y + offsetY + size.y) % size.y;
            }

            // Check if neighbor cell is valid and add it to the list
            if (IsValidCellIndex(offsetX, offsetY))
                cellsList.Add(cells[offsetX, offsetY]);
        }

        return cellsList.ToArray();
    }

    private bool IsValidCellIndex(int x, int y)
    {
        return x >= 0 && x < size.x && y >= 0 && y < size.y;
    }

    public Cell[] GetRemoteNeighborCellsVonNeumann(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Check north neighbor
        if (cell.y + range < size.y)
            cellsList.Add(cells[cell.x, cell.y + range]);
        else if (warp)
            cellsList.Add(cells[cell.x, cell.y + range - size.y]);

        // Check south neighbor
        if (cell.y - range >= 0)
            cellsList.Add(cells[cell.x, cell.y - range]);
        else if (warp)
            cellsList.Add(cells[cell.x, size.y + cell.y - range]);

        // Check east neighbor
        if (cell.x + range < size.x)
            cellsList.Add(cells[cell.x + range, cell.y]);
        else if (warp)
            cellsList.Add(cells[cell.x + range - size.x, cell.y]);

        // Check west neighbor
        if (cell.x - range >= 0)
            cellsList.Add(cells[cell.x - range, cell.y]);
        else if (warp)
            cellsList.Add(cells[size.x + cell.x - range, cell.y]);

        for (int i = 1; i <= range; i++)
        {
            // +x, +y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);

            // -x, +y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);

            // +x, -y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y - i);

            // -x, -y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y - i);
        }

        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsVonNeumann(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Check north neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.y + i < size.y)
                cellsList.Add(cells[cell.x, cell.y + i]);
            else if (warp)
                cellsList.Add(cells[cell.x, cell.y + i - size.y]);
        }

        // Check south neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.y - i >= 0)
                cellsList.Add(cells[cell.x, cell.y - i]);
            else if (warp)
                cellsList.Add(cells[cell.x, size.y + cell.y - i]);
        }

        // Check east neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.x + i < size.x)
                cellsList.Add(cells[cell.x + i, cell.y]);
            else if (warp)
                cellsList.Add(cells[cell.x + i - size.x, cell.y]);
        }

        // Check west neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.x - i >= 0)
                cellsList.Add(cells[cell.x - i, cell.y]);
            else if (warp)
                cellsList.Add(cells[size.x + cell.x - i, cell.y]);
        }
        for (int i = 1; i <= range; i++)
        {
            // +x, +y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y + i);

            // -x, +y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y + i);

            // +x, -y direction
            AddNeighborCell(cellsList, cell.x + i, cell.y - i);

            // -x, -y direction
            AddNeighborCell(cellsList, cell.x - i, cell.y - i);
        }

        return cellsList.ToArray();
    }

    private void AddNeighborCell(List<Cell> cellsList, int x, int y)
    {
        if (warp)
        {
            x = WrapGridIndex(x, size.x);
            y = WrapGridIndex(y, size.y);
        }
        else if (x >= size.x || y >= size.y || x < 0 || y < 0)
        {
            return;
        }

        cellsList.Add(cells[x, y]);
    }

    private int WrapGridIndex(int index, int gridSize)
    {
        return (index % gridSize + gridSize) % gridSize;
    }

    public Cell[] GetNeighborCellsMoore(Cell cell)
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
                int neighborX = cell.x + i;
                int neighborY = cell.y + j;
                if (warp)
                {
                    //warp
                    // Wrap around the grid if neighborX or neighborY is outside the grid bounds
                    if (neighborX >= size.x)
                        neighborX -= size.x;
                    else if (neighborX < 0)
                        neighborX += size.x;

                    if (neighborY >= size.y)
                        neighborY -= size.y;
                    else if (neighborY < 0)
                        neighborY += size.y;
                }
                else
                {
                    // Skip the neighbor cell if it's outside the grid bounds
                    if (neighborX >= size.x || neighborY >= size.y || neighborX < 0 || neighborY < 0)
                        continue;
                }


                cellsList.Add(cells[neighborX, neighborY]);
            }
        }
        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsRemoteMoore(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();
       
        for (int i = -range; i <= range; i++)
        {
            for (int j = -range; j <= range; j++)
            {
                if (Mathf.Abs(i) == range || Mathf.Abs(j) == range)
                {
                    AddNeighborCell(cellsList, cell.x + i, cell.y + j);
                }
            }
        }
        return cellsList.ToArray();
    }

    public Cell[] GetNeighborCellsCross(Cell cell)
    {
        List<Cell> cellsList = new List<Cell>();

        // Check north neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.y + i < size.y)
                cellsList.Add(cells[cell.x, cell.y + i]);
            else if (warp)
                cellsList.Add(cells[cell.x, cell.y + i - size.y]);
        }

        // Check south neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.y - i >= 0)
                cellsList.Add(cells[cell.x, cell.y - i]);
            else if (warp)
                cellsList.Add(cells[cell.x, size.y + cell.y - i]);
        }

        // Check east neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.x + i < size.x)
                cellsList.Add(cells[cell.x + i, cell.y]);
            else if (warp)
                cellsList.Add(cells[cell.x + i - size.x, cell.y]);
        }

        // Check west neighbor
        for (int i = 1; i <= range; i++)
        {
            if (cell.x - i >= 0)
                cellsList.Add(cells[cell.x - i, cell.y]);
            else if (warp)
                cellsList.Add(cells[size.x + cell.x - i, cell.y]);
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
            }
            else if (cell.state == minState && current == maxState)
            {
                count++;
            }

        }

        return count;
    }

    public void GenerateCells()
    {
        LoadColorArray(colorsPanel.GetColorArray());
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
                cellScript.SetColorPalette(colorArray);
                cellScript.SetState(randomNumber);

                cells[i, j] = cellScript;

            }
        }
    }
    public void SetGridSizeFromSlider(float gridSize)
    {
        size = new Vector2Int((int)gridSize, (int)gridSize);
        gridSizeText.text = (int)gridSize + "x" + (int)gridSize;
    }
    public void SetSpeedFromSlider(float speed)
    {
        playSpeed = speed;
        speedText.text = "Speed: " + (int)(1 / speed) + " FPS";

    }
    public void SetStatesFromSlider(float s)
    {
        maxState = (int)s - 1;
        statesText.text = "States: " + s;

    }
    public void SetThresholdFromSlider(float t)
    {
        threshold = (int)t;
        thresholdText.text = "Threshold: " + t;
    }
    public void SetRangeFromSlider(float r)
    {
        range = (int)r;
        rangeText.text = "Range: " + r;

    }
    public void SetNeighborhoodFromDropdown(int r)
    {
        neighborhood = (int)r;

    }
    public void ToggleWarpToggle()
    {
        warp = !warp;
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

    public void LoadColorArray(Color[] array)
    {
        colorArray = array;
    }

    void UpdateCameraPosition()
    {
        float xOffset = size.x * 0.4f;

        camera.orthographicSize = size.x / 2 + 5;
        camera.transform.position = new Vector2(size.x / 2 + xOffset, size.y / 2);

    }


}
