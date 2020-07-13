using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        
        for (int x = 0; x < 4; x++)
        {     
            for (int y = 0; y < 4; y++)
            {
                //NumberBox box = Instantiate(boxPrefab, new Vector2(x, y), Quaternion.identity);
                
                grid[x, y] = RegisterIcon(new CCell(x, y));

                //box.Init(x, y, n + 1, sprites[n]);
                //n++;
            }
        }
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
        box.Init(pos.x, pos.y, index, sprites[index]);

       
        return box;
    }
    public Vector3 GetIconCenterByCell(CCell cell)
    {
        return new Vector3(
            startPosX + cell.y * outX,
            startPosY - cell.x * outY,
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
