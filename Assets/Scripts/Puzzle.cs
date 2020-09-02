using System;
using System.Collections;
using UnityEngine;


public class Puzzle : MonoBehaviour
{
    public static Puzzle instance;
    public NumberBox boxPrefab;
    public NumberBox[,] grid;
    public Sprite[] sprites;
    public float startPosX;
    public float startPosY;
    public float outX;
    public float outY;
    public NumberBox StartMovingAnimation;
    public static bool win = false;
    public GameBehavior mGameOver = null;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        grid = new NumberBox[4, 4];

        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                grid[x, y] = RegisterIcon(new CCell(x, y));
            }
        }

        System.Random rnd = new System.Random();
        Shuffle(rnd, grid);


    }
    public void GameFinish()
    {
        int currentIndex = 0;
        int lastIndex = 0;
        int i = 1;

        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                currentIndex = grid[x, y].GetComponent<NumberBox>().index;
                if ((currentIndex - lastIndex) != 1) { break; }

                if ((i == currentIndex) && ((currentIndex - lastIndex) == 1))
                {
                    i++;
                }
                if (i == 16)
                {
                    Debug.Log("Finish!");
                    mGameOver.GameOver();
                   
                }
                

                lastIndex = currentIndex;
            }
        }
    }

    public void ClickToSwap(int x, int y)
    {
        int dx = GetDx(x, y);
        int dy = GetDy(x, y);

        NumberBox currentBox = grid[x, y];
        NumberBox nextBox = grid[x + dx, y + dy];

        grid[x, y] = nextBox;
        grid[x + dx, y + dy] = currentBox;

        currentBox.UpdatePos(x + dx, y + dy);
        currentBox.StartMovingAnimation(GetIconCenterByCell(new CCell(x, y)), GetIconCenterByCell(new CCell(x + dx, y + dy)));

        nextBox.UpdatePos(x, y);
        nextBox.transform.position = GetIconCenterByCell(new CCell(x, y));
    }

    public int GetDx(int x, int y)
    {
        if (x < 3 && grid[x + 1, y].IsEmpty())
            return 1;

        if (x > 0 && grid[x - 1, y].IsEmpty())
            return -1;

        return 0;
    }

    public int GetDy(int x, int y)
    {
        if (y < 3 && grid[x, y + 1].IsEmpty())
            return 1;

        if (y > 0 && grid[x, y - 1].IsEmpty())
            return -1;

        return 0;
    }

    public const int kMaxX = 4;
    public const int kMaxY = 4;
    private NumberBox RegisterIcon(CCell pos)
    {

        NumberBox box = Instantiate(boxPrefab);
        box.transform.SetParent(this.transform);
        box.transform.localScale = Vector3.one;
        box.transform.position = GetIconCenterByCell(pos);
        int index = pos.y + kMaxY * pos.x;
        box.Init(pos.x, pos.y, index + 1, sprites[index], ClickToSwap, GameFinish);

        return box;
    }

    public void Shuffle(System.Random random, NumberBox[,] array)
    {
        int lengthRow = array.GetLength(1);

        for (int i = array.Length - 1; i > 0; i--)
        {
            int x0 = i % lengthRow;
            int y0 = i / lengthRow;

            int j = random.Next(i + 1);
            int x1 = j % lengthRow;
            int y1 = j / lengthRow;

            NumberBox temp = array[x0, y0];
            array[x0, y0] = array[x1, y1];
            array[x1, y1] = temp;

            temp.UpdatePos(x1, y1);
            temp.transform.position = GetIconCenterByCell(new CCell(x1, y1));

            array[x0, y0].UpdatePos(x0, y0);
            array[x0, y0].transform.position = GetIconCenterByCell(new CCell(x0, y0));
        }
    }

    public Vector3 GetIconCenterByCell(CCell cell)
    {
        return new Vector3(
            startPosX + cell.x * outX,
            startPosY - cell.y * outY,
            this.transform.position.z
        );
    }
    public class CCell
    {
        public int x;
        public int y;
        public CCell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
