using System;
using UnityEngine;


public class Puzzle : MonoBehaviour
{
    public NumberBox boxPrefab;
    public NumberBox[,] grid;
    public Sprite[] sprites;

    public float startPosX;
    public float startPosY;
    public float outX;
    public float outY;


    void Start()
    {
        grid = new NumberBox[4, 4];
        //int n = 0;

        for (int y = 3; y >= 0; y--)
            for (int x = 0; x < 4; x++)
            {
                //NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                
                grid[x, y] = RegisterIcon(new CCell(x, y));

                //box.Init(x, y, n + 1, sprites[n]);
                //n++;
            }

        System.Random rnd = new System.Random();
        Shuffle(rnd, grid);
    }

    public const int kMaxX = 4;
    public const int kMaxY = 4;
    private NumberBox RegisterIcon(CCell pos)
    {
        
        NumberBox box = Instantiate(boxPrefab);
        box.transform.SetParent(this.transform);
        box.transform.localScale = Vector3.one;
        box.transform.position = GetIconCenterByCell(pos);
        int index = pos.x + kMaxX * pos.y;
        box.Init(pos.x, pos.y, index, sprites[index]);
        
        return box;
    }


    public void Shuffle(System.Random random, NumberBox[,] array)
    {
        int lengthRow = array.GetLength(1);

        for (int i = array.Length - 1; i > 0; i--)
        {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = random.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            NumberBox temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;

            temp.UpdatePos(j0, j1);
            temp.transform.position = GetIconCenterByCell(new CCell(j0,j1));

            array[i0, i1].UpdatePos(i0, i1);
            array[i0, i1].transform.position = GetIconCenterByCell(new CCell(i0, i1));
        }
    }

    public Vector3 GetIconCenterByCell(CCell cell)
    {
        return new Vector3(
            startPosX + cell.y * outX,
            startPosY + cell.x * outY,
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
