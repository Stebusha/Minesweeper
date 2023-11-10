using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileKind
    {
        Blank,
        Clue,
        Mine
    }

    public enum TileState
    {
        Normal,
        Flagged
    }

    public TileKind tileKind = TileKind.Blank;

    public TileState tileState = TileState.Normal;

    public bool isCovered = true;

    public bool didCheck = false;

    public Sprite coveredSprite;

    public Sprite mineClicked;
    
    public Sprite flagSprite;

    private Sprite defaultSprite;

    public Sprite wrongSprite;

    public static int countFlags = 0;

    private void Start()
    {
        defaultSprite = GetComponent<SpriteRenderer>().sprite;
        GetComponent<SpriteRenderer>().sprite = coveredSprite;
    }

    public void SetIsCovered(bool covered)
    {
        isCovered = covered;
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }

    public void SetWrongMine()
    {
        GetComponent<SpriteRenderer>().sprite = wrongSprite;
    }
    public void SetFindedMine()
    {
        GetComponent<SpriteRenderer>().sprite = flagSprite;
    }
    public void SetClickedMine()
    {
        GetComponent<SpriteRenderer>().sprite = mineClicked;
    }


    private void OnMouseOver()
    {
        if (isCovered)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (tileState == TileState.Normal)
                {
                    tileState = TileState.Flagged;
                    GetComponent<SpriteRenderer>().sprite = flagSprite;
                    countFlags++;
                }
                else
                {
                    tileState = TileState.Normal;
                    GetComponent<SpriteRenderer>().sprite = coveredSprite;
                    countFlags--;
                }
            }
        }  
    }
}
