using UnityEngine;

public class TouchWatcher
{
    private bool _isPressing;

    public bool IsPressing => _isPressing;

    private float _lastTapTime = 0f;

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }


    public Direction Watch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            var now = Time.time;
            if (touch.phase == TouchPhase.Began) // 触摸开始
            {
                _lastTapTime = Time.time;
            }
            
            if (now - _lastTapTime > 0.3f)
            {
                _isPressing = true; // 设置为正在持续按住屏幕
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isPressing = false; // 设置为不再持续按住屏幕
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