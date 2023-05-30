using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FoodGenerator : MonoBehaviour
{
    private static FoodGenerator _instance;

    public static FoodGenerator instance => _instance;

    private int xlimit = 20;

    private int ylimit = 11;

    private int xoffset = 6;

    public GameObject foodPrefab;

    public GameObject rewardPrefab;

    public Sprite[] sprites;

    private GameObject _foodHolder;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _foodHolder = GameObject.Find("Foods");
        for (int i = 0; i < 6; i++)
        {
            Generate();
        }
    }

    public void Generate()
    {
        int idx = Random.Range(0, sprites.Length);
        Sprite sprite = sprites[idx];
        GameObject food = Instantiate(foodPrefab, _foodHolder.transform, false);
        Image image = food.GetComponent<Image>();
        image.sprite = sprites[idx];
        int x = Random.Range(-xlimit + xoffset, xlimit);
        int y = Random.Range(-ylimit, ylimit);
        food.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        GenerateReward();
    }

    private void GenerateReward()
    {
        int random = Random.Range(0, 100);
        if (random < 25)
        {
            GameObject reward = Instantiate(rewardPrefab, _foodHolder.transform, false);
            int x = Random.Range(-xlimit + xoffset, xlimit);
            int y = Random.Range(-ylimit, ylimit);
            reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }
}