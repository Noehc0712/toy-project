using UnityEngine;
using TMPro;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.SceneManagement;

public class GameDir : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int point = 0;
    public float timeleft = 60.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI pointText;

    void Start()
    {
        this.point = 0;
    }

    public void AddPoint()
    {
        this.point++;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddPoint();
        }
        
        DisplayPoint(point);
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            DisplayTime(timeleft);
            DisplayPoint(point);
        }
        else
        {
            timeleft = 0;
            DisplayTime(timeleft);
            SceneManager.LoadScene("Main");
        }
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisplayPoint(int point)
    {
        pointText.text = string.Format("{0}", point);
    }
}
