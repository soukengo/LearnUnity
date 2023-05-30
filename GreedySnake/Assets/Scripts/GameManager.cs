using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private const string LastResultKey = "result.last";
    private const string BestResultKey = "result.best";
    private const string LastConfigKey = "config.last";
    public static readonly GameManager Instance = new GameManager();


    private Config _config;

    public Config Config => _config;


    private GameManager()
    {
        _config = new Config(Options.Theme.Blue, Options.Mode.Border);
    }

    public void StartGame(Config config)
    {
        _config = config;
        StoreConfig(config);
        SceneManager.LoadScene("GameScene");
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public Data.Result GetLastResult()
    {
        if (!PlayerPrefs.HasKey(LastResultKey))
        {
            return new Data.Result();
        }

        var result = PlayerPrefs.GetString(LastResultKey);
        return JsonUtility.FromJson<Data.Result>(result);
    }

    public Data.Result GetBestResult()
    {
        if (!PlayerPrefs.HasKey(BestResultKey))
        {
            return new Data.Result();
        }

        var result = PlayerPrefs.GetString(BestResultKey);
        return JsonUtility.FromJson<Data.Result>(result);
    }

    public void StoreScore(int score, int length)
    {
        var last = new Data.Result(score, length);
        var resultJson = JsonUtility.ToJson(last);
        PlayerPrefs.SetString(LastResultKey, resultJson);
        var best = GetBestResult();
        if (last.score > best.score && last.length > best.score)
        {
            PlayerPrefs.SetString(BestResultKey, resultJson);
        }
    }

    private void StoreConfig(Config config)
    {
        var json = JsonUtility.ToJson(config);
        PlayerPrefs.SetString(LastConfigKey, json);
    }

    public Config GetLastConfig()
    {
        if (!PlayerPrefs.HasKey(BestResultKey))
        {
            return new Config();
        }

        var result = PlayerPrefs.GetString(LastConfigKey);
        return JsonUtility.FromJson<Config>(result);
    }
}