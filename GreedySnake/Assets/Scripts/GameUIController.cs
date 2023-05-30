using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour, IScoreUpdater

{
    public int score;

    public int length;

    public Text msgText;

    public Text scoreText;

    public Text lengthText;

    public Button homeButton;

    public Button statusButton;

    public Sprite[] statusSprites = new Sprite[2];

    public SnakeHead snakeHead;

    public SnakeSprites sprites;

    private bool _stopped = false;

    private Config _config;

    public GameUIController()
    {
    }

    private void Awake()
    {
        _config = GameManager.Instance.Config;
        snakeHead.bodySprites = sprites.body[_config.theme];
        snakeHead.Updater = this;
        homeButton.onClick.AddListener(BackHome);
        statusButton.onClick.AddListener(StartOrStopGame);
        StartGame(_config);
    }

    public void UpdateScore(int s = 1, int l = 1)
    {
        score += s;
        length += l;
        scoreText.text = "" + score;
        lengthText.text = "" + length;
        GameManager.Instance.StoreScore(score, length);
    }


    private void StartOrStopGame()
    {
        if (_stopped)
        {
            StartGame(_config);
        }
        else
        {
            StopGame();
        }
    }

    private void StartGame(Config config)
    {
        msgText.text = config.mode == Options.Mode.Border ? "边界模式" : "自由模式";
        // msgText.color = 

        var borders = GameObject.FindGameObjectsWithTag("Border");
        foreach (var border in borders)
        {
            border.GetComponent<Image>().enabled = config.mode == Options.Mode.Border;
        }

        _config = config;

        snakeHead.GetComponent<Image>().sprite = sprites.head[_config.theme];
        Image button = statusButton.GetComponent<Image>();
        button.sprite = statusSprites[1];
        homeButton.gameObject.SetActive(false);
        snakeHead.StartMove();
        _stopped = false;
    }

    private void StopGame()
    {
        Image button = statusButton.GetComponent<Image>();
        button.sprite = statusSprites[0];
        homeButton.gameObject.SetActive(true);
        snakeHead.StopMove();
        _stopped = true;
    }

    private void BackHome()
    {
        StopGame();
        GameManager.Instance.FinishGame();
    }
}