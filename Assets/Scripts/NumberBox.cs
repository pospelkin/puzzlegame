using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

public class NumberBox : MonoBehaviour  {

    public int index = 0;
    public int x = 0;
    public int y = 0;

    public Action finish = null;
    private Action<int, int> swapFunc = null;
    float lerp = 0, duration = 0.2f;
    public bool mIsAnimationRunning = false;
    public Vector3 mStartPos;
    public Vector3 mEndPos;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
       
    private float oldPosX;
    private float oldPosY;

    private bool noCheckMouseUp = false;

    private const float deltaMove = 20;

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunc, Action finish)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(x, y);
        this.swapFunc = swapFunc;
        this.finish = finish;
        //Debug.Log("Init");
    }
    public void UpdatePos(int x1, int y1)
    {
        x = x1;
        y = y1;
        //Debug.Log("UpdatePos");
    }
   /*public void OnMouseDown()
    {
       if (Input.GetMouseButtonDown(0) && swapFunc != null)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;
                        
            Vector3 posPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
            oldPosX = posPos.x;
            oldPosY = posPos.y;

            isBeingHeld = true;
            noCheckMouseUp = false;

            //swapFunc(x, y);
        }
    }
      
    private void OnMouseUp()
    {
        isBeingHeld = false;
        if (noCheckMouseUp == false)
        { 

            if (this.gameObject.transform.localPosition.x > oldPosX - 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.x < oldPosX + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.y > oldPosY + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.y < oldPosY + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
        }
    }*/

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector3 touchPos;
            touchPos = touch.position;
            //touchPos = Camera.main.ScreenToWorldPoint(touchPos);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && index != 16)
                    {
                        startPosX = touchPos.x - this.transform.position.x;
                        startPosY = touchPos.y - this.transform.position.y;

                        Vector3 posPos = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                        oldPosX = posPos.x;
                        oldPosY = posPos.y;

                        isBeingHeld = true;
                        noCheckMouseUp = false;
                        Debug.Log("Began");
                    }
                    break;
                
                case TouchPhase.Moved:

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && index != 16)
                    {
                        if (isBeingHeld == true && Puzzle.instance.GetDx(x, y) != 0 | Puzzle.instance.GetDy(x, y) != 0)
                        {
                            Vector2 starttouchPos = new Vector2(startPosX, startPosY);

                            Vector2 newtouchPos;
                            newtouchPos = touch.position;// - starttouchPos;
                            newtouchPos = Camera.main.ScreenToWorldPoint(newtouchPos);

                            Vector3 direction = newtouchPos - starttouchPos;
                            direction.Normalize();
                            Vector3 check = direction;// * 10 - this.transform.localPosition;
                            Debug.Log("Moved00");
                            Debug.Log(newtouchPos);
                            Debug.Log(starttouchPos);
                            Debug.Log("X:" + x + " Y:" + y + " check:" + check);
                            if (Puzzle.instance.GetDx(x, y) == -1 && (check.x) > 0)
                            {
                                Debug.Log("Moved1");
                                this.gameObject.transform.position = new Vector3(newtouchPos.x, transform.position.y, 0);

                                float deltaX = Math.Abs(this.gameObject.transform.position.x - oldPosX);
                                if (deltaX > deltaMove)
                                {
                                    swapFunc(x, y);
                                    isBeingHeld = false;
                                    noCheckMouseUp = true;
                                }
                            }

                            if (Puzzle.instance.GetDx(x, y) == 1 && (check.x) < 0)
                            {
                                Debug.Log("Moved2");
                                this.gameObject.transform.localPosition = new Vector3(newtouchPos.x, transform.position.y, 0);

                                float deltaX = Math.Abs(this.gameObject.transform.position.x - oldPosX);
                                if (deltaX > deltaMove)
                                {
                                    swapFunc(x, y);
                                    isBeingHeld = false;
                                    noCheckMouseUp = true;
                                }
                            }

                            if (Puzzle.instance.GetDy(x, y) == -1 && check.y > 0)
                            {
                                Debug.Log("Moved3");
                                this.gameObject.transform.position = new Vector3(transform.position.x, newtouchPos.y, 0);
                                float deltaY = Math.Abs(this.gameObject.transform.position.y - oldPosY);

                                if (deltaY > deltaMove)
                                {
                                    swapFunc(x, y);
                                    isBeingHeld = false;
                                    noCheckMouseUp = true;
                                }
                            }

                            if (Puzzle.instance.GetDy(x, y) == 1 && check.y > 0)
                            {
                                Debug.Log("Moved4");
                                this.gameObject.transform.position = new Vector3(transform.position.x, newtouchPos.y, 0);

                                float deltaY = Math.Abs(this.gameObject.transform.position.y - oldPosY);

                                if (deltaY > deltaMove)
                                {
                                    swapFunc(x, y);
                                    isBeingHeld = false;
                                    noCheckMouseUp = true;
                                }
                            }
                        }
                    }
                    break;

                case TouchPhase.Ended:

                    
                    isBeingHeld = false;

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && index != 16)
                    {
                        Debug.Log("Ended00");
                        if (noCheckMouseUp == false)
                        {
                            float deltaX = Math.Abs(this.gameObject.transform.position.x - oldPosX);
                            float deltaY = Math.Abs(this.gameObject.transform.position.y - oldPosY);

                            if (deltaX > deltaMove)
                            {
                                Debug.Log("Ended01");
                                this.gameObject.transform.position = new Vector3(oldPosX, oldPosY, 0);
                            }
                            else if (deltaY > deltaMove)
                            {
                                Debug.Log("Ended02");
                                this.gameObject.transform.position = new Vector3(oldPosX, oldPosY, 0);
                            }
                            
                            Debug.Log("Ended");
                        }
                    }
                    break;
            }
        }


        if (mIsAnimationRunning)
        {
            lerp += Time.deltaTime / duration;
            this.transform.position = Vector3.Lerp(this.gameObject.transform.localPosition, mEndPos, lerp);
            Debug.Log("mIsAnimationRunning00");

            if (this.transform.position == mEndPos)
            {
                mIsAnimationRunning = false;
                lerp = 0.0f;

                finish();
                Debug.Log("mIsAnimationRunning");
            }
        }
    }

    public void StartMovingAnimation(Vector3 startPos, Vector3 endPos)
    {
        startPos = this.gameObject.transform.localPosition;
        mStartPos = startPos;
        mEndPos = endPos;
        mIsAnimationRunning = true;
    }
    public bool IsEmpty()
    {
        return index == 16;
    }
}

/*public class NumberBox : MonoBehaviour
{

    public int index = 0;
    public int x = 0;
    public int y = 0;

    public Action finish = null;
    private Action<int, int> swapFunc = null;
    float lerp = 0, duration = 0.2f;
    public bool mIsAnimationRunning = false;
    public Vector3 mStartPos;
    public Vector3 mEndPos;

    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    private float oldPosX;
    private float oldPosY;

    private bool noCheckMouseUp = false;

    public void Init(int i, int j, int index, Sprite sprite, Action<int, int> swapFunc, Action finish)
    {
        this.index = index;
        this.GetComponent<SpriteRenderer>().sprite = sprite;
        UpdatePos(x, y);
        this.swapFunc = swapFunc;
        this.finish = finish;
    }
    public void UpdatePos(int x1, int y1)
    {
        x = x1;
        y = y1;
    }
    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && swapFunc != null)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            Vector3 posPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, 0);
            oldPosX = posPos.x;
            oldPosY = posPos.y;

            isBeingHeld = true;
            noCheckMouseUp = false;

            //swapFunc(x, y);
        }
    }




    private void OnMouseUp()
    {
        isBeingHeld = false;
        if (noCheckMouseUp == false)
        {

            if (this.gameObject.transform.localPosition.x > oldPosX - 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.x < oldPosX + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.y > oldPosY + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
            if (this.gameObject.transform.localPosition.y < oldPosY + 0.46)
            {
                this.gameObject.transform.localPosition = new Vector3(oldPosX, oldPosY, 0);
            }
        }
    }

    void Update()
    {

        if (isBeingHeld == true && Puzzle.instance.GetDx(x, y) != 0 | Puzzle.instance.GetDy(x, y) != 0)
        {
            Vector3 mousePos = new Vector3(startPosX, startPosY, 0);

            Vector3 newmousePos;
            newmousePos = Input.mousePosition;
            newmousePos = Camera.main.ScreenToWorldPoint(newmousePos);

            Vector3 direction = newmousePos - mousePos;
            direction.Normalize();
            Vector3 check = direction * 10 - this.transform.localPosition;

            if (Puzzle.instance.GetDx(x, y) == -1 && (check.x) < 0)
            {
                this.gameObject.transform.localPosition = new Vector3(newmousePos.x - startPosX, transform.position.y, 0);

                if (this.gameObject.transform.localPosition.x < oldPosX - 0.46)
                {
                    swapFunc(x, y);
                    isBeingHeld = false;
                    noCheckMouseUp = true;

                }

            }

            if (Puzzle.instance.GetDx(x, y) == 1 && (check.x) > 0)
            {
                this.gameObject.transform.localPosition = new Vector3(newmousePos.x - startPosX, transform.position.y, 0);

                if (this.gameObject.transform.localPosition.x > oldPosX + 0.46)
                {
                    swapFunc(x, y);
                    isBeingHeld = false;
                    noCheckMouseUp = true;
                }

            }

            if (Puzzle.instance.GetDy(x, y) == -1 && check.y > 0)
            {
                this.gameObject.transform.localPosition = new Vector3(transform.position.x, newmousePos.y - startPosY, 0);
                if (this.gameObject.transform.localPosition.y > oldPosY + 0.46)
                {
                    swapFunc(x, y);
                    isBeingHeld = false;
                    noCheckMouseUp = true;
                }
            }

            if (Puzzle.instance.GetDy(x, y) == 1 && check.y < 0)
            {
                this.gameObject.transform.localPosition = new Vector3(transform.position.x, newmousePos.y - startPosY, 0);
                if (this.gameObject.transform.localPosition.y < oldPosY - 0.46)
                {
                    swapFunc(x, y);
                    isBeingHeld = false;
                    noCheckMouseUp = true;
                }
            }
        }

        if (mIsAnimationRunning)
        {
            lerp += Time.deltaTime / duration;
            this.transform.position = Vector3.Lerp(this.gameObject.transform.localPosition, mEndPos, lerp);

            if (this.transform.position == mEndPos)
            {
                mIsAnimationRunning = false;
                lerp = 0.0f;

                finish();
            }
        }
    }

    public void StartMovingAnimation(Vector3 startPos, Vector3 endPos)
    {
        startPos = this.gameObject.transform.localPosition;
        mStartPos = startPos;
        mEndPos = endPos;
        mIsAnimationRunning = true;
    }
    public bool IsEmpty()
    {
        return index == 16;
    }
}*/
