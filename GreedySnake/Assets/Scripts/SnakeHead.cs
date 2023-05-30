using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour
{
    public float velocity = 0.35f;
    public int step = 30;

    public GameObject bodyPrefab;
    public Sprite[] bodySprites = new Sprite[2];

    private int _x;

    private int _y;

    private Vector3 _headPos;

    private readonly List<Transform> _bodyList = new List<Transform>();

    private bool _stopped;

    private TouchWatcher touchWatcher = new TouchWatcher();

    public IScoreUpdater Updater { private get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _headPos = gameObject.transform.localPosition;
        _x = step;
        _y = 0;
        gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (_stopped)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartMove(0.15f);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartMove(velocity);
        }

        var slideDirection = touchWatcher.GetSlideDirection();

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || slideDirection == TouchWatcher.Direction.Up) &&
            _y != -step)
        {
            _x = 0;
            _y = step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) ||
             slideDirection == TouchWatcher.Direction.Down) && _y != step)
        {
            _x = 0;
            _y = -step;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) ||
             slideDirection == TouchWatcher.Direction.Left) && _x != step)
        {
            _x = -step;
            _y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) ||
             slideDirection == TouchWatcher.Direction.Right) && _x != -step)
        {
            _x = step;
            _y = 0;
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Move()
    {
        var o = gameObject;
        _headPos = o.transform.localPosition;
        o.transform.localPosition = new Vector3(_headPos.x + _x, _headPos.y + _y, _headPos.z);
        if (_bodyList.Count <= 0) return;
        for (var i = _bodyList.Count - 2; i >= 0; i--)
        {
            _bodyList[i + 1].localPosition = _bodyList[i].localPosition;
        }

        _bodyList[0].localPosition = _headPos;
    }

    public void StartMove()
    {
        StartMove(velocity);
    }

    private void StartMove(float speed)
    {
        StopMove();
        _stopped = false;
        InvokeRepeating("Move", 0, speed);
    }

    public void StopMove()
    {
        _stopped = true;
        CancelInvoke("Move");
    }


    private void Grow()
    {
        var idx = _bodyList.Count % 2 == 0 ? 0 : 1;
        var parent = GameObject.Find("Player");
        var body = Instantiate(bodyPrefab, new Vector3(2000, 2000), Quaternion.identity, parent.transform);
        body.GetComponent<Image>().sprite = bodySprites[idx];
        _bodyList.Add(body.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            Grow();
            Updater.UpdateScore(1, 1);
            FoodGenerator.instance.Generate();
        }

        if (other.gameObject.CompareTag("Reward"))
        {
            Debug.Log("666，获得奖励了");
            Destroy(other.gameObject);
            var score = Random.Range(5, 10);
            Updater.UpdateScore(1, 0);
        }

        if (other.gameObject.CompareTag("SnakeBody"))
        {
            Die();
            return;
        }

        if (other.gameObject.CompareTag("Border"))
        {
            if (GameManager.Instance.Config.mode == Options.Mode.Border)
            {
                Die();
                return;
            }

            switch (other.gameObject.name)
            {
                case "LeftBorder":
                    gameObject.transform.localPosition = new Vector3(-_headPos.x + 140, _headPos.y + _y, _headPos.z);
                    break;
                case "RightBorder":
                    gameObject.transform.localPosition = new Vector3(-_headPos.x + 140, _headPos.y + _y, _headPos.z);
                    break;
                case "TopBorder":
                    gameObject.transform.localPosition = new Vector3(_headPos.x, -_headPos.y, _headPos.z);
                    break;
                case "BottomBorder":
                    gameObject.transform.localPosition = new Vector3(_headPos.x, -_headPos.y, _headPos.z);
                    break;
            }
        }
    }

    private void Die()
    {
        GameManager.Instance.FinishGame();
    }
}