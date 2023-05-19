using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonClick()
    {
        Debug.Log("OnButtonClick");
        SceneManager.LoadScene("GameScene");
    }
}