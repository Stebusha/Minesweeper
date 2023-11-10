using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    private static int gridHeight =10;
    private static int gridWidth = 10;

    [SerializeField] private Text minesCounter;

    private static int countOfMines =10;
    public static int covered=100;
    public static bool isLevelActive = false;

    public Tile[,] grid = new Tile[gridWidth, gridHeight];

    public List<Tile> tilesToCheck = new List<Tile>();

    private void Awake()
    {
        countOfMines = DifficultySettings.countOfMines;
        gridHeight = DifficultySettings.gridHeight;
        gridWidth= DifficultySettings.gridWidth;
        covered = gridWidth * gridHeight;
        grid = new Tile[gridWidth, gridHeight];
        minesCounter.text = $"Mines: {countOfMines}";
        Tile.countFlags = 0;
    }
    private void Start()
    {
        for(int i = 0; i < countOfMines; i++)
        {
            PlaceMines();
        }
        PlaceClues();
        PlaceBlanks();
        isLevelActive = true;
        Tile.countFlags = 0;
        minesCounter.text = $"Mines: {DifficultySettings.countOfMines}";
    }
    private void Update()
    {
        if (isLevelActive)
        {
            Win();
            CheckInput();
            minesCounter.text = $"Mines: {countOfMines-Tile.countFlags}";
        }
        else if (!PauseMenu.GameIsPaused)
            minesCounter.text = "";
    }

    private void CheckInput()
    {
        if (!isLevelActive)
            return;
        if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            int x = Mathf.RoundToInt(mousePosition.x);
            int y = Mathf.RoundToInt(mousePosition.y);
            if (x < gridWidth && y < gridHeight && y >= 0 && x >= 0)
            {
                Tile tile = grid[x, y];
                if (tile.isCovered)
                {

                    if (tile.tileState == Tile.TileState.Normal)
                    {
                        if (tile.tileKind == Tile.TileKind.Mine)
                            GameOver(tile);
                        else
                            tile.SetIsCovered(false);
                        if (tile.tileKind == Tile.TileKind.Blank)
                            RevealAdjacentTilesForTileAt(x, y);
                    }
                }
                else
                    return;
            }
            GetCountOfCovered();
        }
    }

    private void GameOver(Tile tile)
    {
        tile.SetClickedMine();

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Mine");

        foreach(GameObject go in gameObjects)
        {
            Tile t = go.GetComponent<Tile>();
            if (t != tile)
            {
                t.SetIsCovered(false);
                if (t.tileState == Tile.TileState.Flagged)
                {
                    if(t.tileKind == Tile.TileKind.Mine)
                        t.SetFindedMine();
                }
            }
        }
        for(int x = 0; x < gridWidth; x++)
        {
            for(int y=0;y< gridHeight; y++)
            {
                Tile temp = grid[x, y];
                if (temp.tileState == Tile.TileState.Flagged && temp.tileKind != Tile.TileKind.Mine)
                    temp.SetWrongMine();
            }
        }
        isLevelActive = false;
        GameOverState.Instance1.Show();
    }

    private void Win()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Mine");
        int numOfFindedMines = 0;
        foreach (GameObject go in gameObjects)
        {
            Tile t = go.GetComponent<Tile>();
            if (t.tileState == Tile.TileState.Flagged)
                numOfFindedMines++;
        }
        if (covered == countOfMines)
            RevealMap();    
        else
        {
            if (Tile.countFlags==countOfMines&&numOfFindedMines==countOfMines)
                RevealMap();
        }
    }

    private void RevealMap()
    {
        isLevelActive = false;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Tile tile = grid[x, y];
                if (tile.isCovered)
                {
                    if (tile.tileKind != Tile.TileKind.Mine)
                        tile.SetIsCovered(false);
                    else
                        tile.SetFindedMine();
                }
            }
        }
        GameWinState.Instance2.Show();
    }

    void PlaceMines()
    {
        int x = Random.Range(0, gridWidth);
        int y = Random.Range(0, gridHeight);

        if(grid[x, y] == null)
        {
            Tile mineTile = Instantiate(Resources.Load("Prefabs/mine", typeof(Tile)), new Vector3(x,y,0), Quaternion.identity) as Tile;

            grid[x, y] = mineTile;

            Debug.Log("(" + x + "," + y + ")");
        }
        else
            PlaceMines();
    }

    void PlaceClues()
    {
        for(int y=0; y < gridHeight; y++)
        {
            for(int x=0; x < gridWidth; x++)
            {
                if(grid[x, y] == null)
                {
                    int numOfMines = 0;
                    //North
                    if (y + 1 < gridHeight)
                    {
                        if(grid[x,y+1] != null && grid[x,y+1].tileKind == Tile.TileKind.Mine)                       
                            numOfMines++;
                    }
                    //East
                    if(x + 1 < gridWidth)
                    {
                        if(grid[x+1,y] != null && grid[x+1, y].tileKind == Tile.TileKind.Mine)                       
                            numOfMines++;
                    }
                    //South
                    if (y - 1 >= 0)
                    {
                        if (grid[x, y - 1] != null && grid[x, y - 1].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }
                    //West
                    if (x - 1 >= 0)
                    {
                        if (grid[x - 1, y] != null && grid[x - 1, y].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }

                    //Corners

                    if (y + 1 < gridHeight && x + 1 < gridWidth)
                    {
                        if (grid[x + 1, y + 1] != null && grid[x+1, y + 1].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }

                    if (y + 1 < gridHeight && x - 1 >=0)
                    {
                        if (grid[x - 1, y + 1] != null && grid[x - 1, y + 1].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }

                    if (x + 1 < gridWidth && y - 1 >=0)
                    {
                        if (grid[x + 1, y - 1] != null && grid[x + 1, y - 1].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }

                    if (x - 1 >=0 && y - 1 >=0)
                    {
                        if (grid[x - 1, y - 1] != null && grid[x - 1, y - 1].tileKind == Tile.TileKind.Mine)
                            numOfMines++;
                    }

                    if (numOfMines > 0)
                    {
                        Tile clueTile = Instantiate(Resources.Load("Prefabs/" + numOfMines, typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                        grid[x, y] = clueTile;
                    }
                }
            }
        }
    }

    void PlaceBlanks()
    {
        for(int y = 0; y < gridHeight; y++)
        {
            for(int x=0; x < gridWidth; x++)
            {
                if(grid[x, y] == null)
                {
                    Tile blankTile = Instantiate(Resources.Load("Prefabs/blank", typeof(Tile)), new Vector3(x, y, 0), Quaternion.identity) as Tile;
                    grid[x, y] = blankTile;
                }
            }
        }
    }

    void RevealAdjacentTilesForTileAt(int x,int y)
    {
        if (y + 1 < gridHeight)
            CheckTileAt(x, y+1);

        if (x+1<gridWidth)
            CheckTileAt(x+1, y);

        if(x-1>=0)
            CheckTileAt(x-1, y);

        if(y-1>=0)
            CheckTileAt(x, y-1);

        if(y+1<gridHeight&&x+1<gridWidth)
            CheckTileAt(x+1,y+1);

        if (x - 1 >= 0 && y + 1 < gridHeight)
            CheckTileAt(x - 1, y + 1);

        if(x+1<gridWidth&&y-1>=0)
            CheckTileAt(x+1,y-1);

        if(x-1>=0&&y-1>=0)
            CheckTileAt(x-1,y-1);

        for(int i=tilesToCheck.Count-1; i>=0; i--)
        {
            if(tilesToCheck[i].didCheck)
                tilesToCheck.RemoveAt(i);
        }

        if (tilesToCheck.Count > 0)
            RevealAdjacentTilesForTiles();
    }

    private void RevealAdjacentTilesForTiles()
    {
        for(int i = 0; i < tilesToCheck.Count; i++)
        {
            Tile tile = tilesToCheck[i];

            int x = (int)tile.gameObject.transform.localPosition.x;
            int y = (int)tile.gameObject.transform.localPosition.y;

            tile.didCheck=true;
            if (tile.tileState != Tile.TileState.Flagged)
            {
                tile.SetIsCovered(false);
            }
            RevealAdjacentTilesForTileAt(x,y);
        }
    }

    private void CheckTileAt(int x, int y)
    {
        Tile tile = grid[x,y];

        if (tile.tileKind == Tile.TileKind.Blank&&tile.tileState==Tile.TileState.Normal)
        {
            tilesToCheck.Add(tile);
        }
        else if (tile.tileKind == Tile.TileKind.Clue && tile.tileState == Tile.TileState.Normal)
        {
            tile.SetIsCovered(false);
        }
    }

    public void GetCountOfCovered()
    {
        int count = 0;
        for (int x = 0; x < gridWidth; x++)
        {
            for(int y = 0; y < gridHeight; y++)
            {
                Tile t = grid[x, y];
                if(t.isCovered)
                    count++;
            }
        }
        covered=count;
        Debug.Log(covered);
    }
}
