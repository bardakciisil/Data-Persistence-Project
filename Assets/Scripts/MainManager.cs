using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    private int m_best_Points;
    private string m_best_Name;
    private string m_current_Name;

    private bool m_GameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        m_current_Name = GameManager.Instance.CurrentPlayer.Name;
        m_best_Name = GameManager.Instance.BestPlayer.Name;
        m_best_Points = GameManager.Instance.BestPlayer.Score;

        BestScoreText.text = "Best Score : " + m_best_Name + " : " + m_best_Points;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if (m_Points > m_best_Points)
        {
            m_best_Points = m_Points;
            m_best_Name = m_current_Name;
            BestScoreText.text = "Best Score : " + m_best_Name + " : " + m_best_Points;
        }
    }



    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        GameManager.Instance.SaveForBestPlayer(m_best_Name, m_best_Points);
    }
}
