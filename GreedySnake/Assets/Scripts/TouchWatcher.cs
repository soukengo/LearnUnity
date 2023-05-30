using UnityEngine;

public class TouchWatcher
{
    private float _lastTapTime = 0f;
    // private float _doubleTapDelay = 0.3f;

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }


    public Direction GetSlideDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // 触摸开始
            {
                _lastTapTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPosition = touch.deltaPosition;

                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                {
                    if (deltaPosition.x > 0)
                    {
                        // 向右滑动
                        return Direction.Right;
                    }
                    else if (deltaPosition.x < 0)
                    {
                        // 向左滑动
                        return Direction.Left;
                    }
                }
                else
                {
                    if (deltaPosition.y > 0)
                    {
                        // 向上滑动
                        return Direction.Up;
                    }
                    else if (deltaPosition.y < 0)
                    {
                        // 向下滑动
                        return Direction.Down;
                    }
                }
            }
        }

        return Direction.None;
    }
}