using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    private GameObject _plane;
    private int _score = 0;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI winText;
    public Text toastText;

    public float speed;

    public GameObject foodPrefab;

    public int foodCount;

    private GameObject player;


    // private void Awake()
    // {
    //     Application.targetFrameRate = 60;
    // }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("游戏开始了");
        rb = GetComponent<Rigidbody>();
        _plane = GameObject.Find("Plane");
        scoreText.text = "Score: " + _score;
        
        player = GameObject.Find("Player");

        Random random = new Random();
        for (int i = 0; i < foodCount; i++)
        {
            GameObject food = Instantiate(foodPrefab);
            float x = random.Next(-90, 90);
            float z = random.Next(-45, 45);
            // 设置对象的位置和缩放
            food.transform.position = new Vector3(x, 3f, z);
            // food.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    float lastTapTime = 0f;
    float doubleTapDelay = 0.3f;  
    // Update is called once per frame
    
    void Update()
    {
        // Debug.Log("游戏运行中");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // 触摸开始
            {
                if (Time.time - lastTapTime < doubleTapDelay)
                {
                    player.transform.position = new Vector3(0, 3, 0);
                }
                lastTapTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                float touchSpeed = speed * 1.5f;
                Vector2 deltaPosition = touch.deltaPosition;

                if (Mathf.Abs(deltaPosition.x) > Mathf.Abs(deltaPosition.y))
                {
                    if (deltaPosition.x > 0)
                    {
                        // 向右滑动
                        rb.velocity = new Vector3(1, 0, 0) * touchSpeed;
                    }
                    else if (deltaPosition.x < 0)
                    {
                        // 向左滑动
                        rb.velocity = new Vector3(-1, 0, 0) * touchSpeed;
                    }
                }
                else
                {
                    if (deltaPosition.y > 0)
                    {
                        // 向上滑动
                        rb.velocity = new Vector3(0, 0, 1) * touchSpeed;
                    }
                    else if (deltaPosition.y < 0)
                    {
                        // 向下滑动
                        rb.velocity = new Vector3(0, 0, -1) * touchSpeed;
                    }
                }
            }

            return;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // Debug.Log("Horizontal: " + h);
        // Debug.Log("Vertical: " + v);
        rb.velocity = new Vector3(h, 0, v) * speed;
        // Transform planeTransform = _plane.transform;
        // float maxX = planeTransform.right.x;
        // float maxZ = planeTransform.forward.z;
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("FOOD"))
    //     {
    //         Destroy(collision.gameObject);
    //         Debug.Log("Bingo");
    //         // GetComp
    //     }
    // }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FOOD"))
        {
            _score++;
            Destroy(other.gameObject);
            Debug.Log("Bingo");
            scoreText.text = "Score: " + _score;

            if (_score == foodCount)
            {
                winText.gameObject.SetActive(true);

                await Task.Delay(2000);

                SceneManager.LoadScene("MainScene");
            }
        }
    }
    

    private void showToast(string text)
    {
        toastText.text = text;
        toastText.gameObject.SetActive(true);
        Task.Delay(2000);
        toastText.gameObject.SetActive(false);
    }
}