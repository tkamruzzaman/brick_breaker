using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public Action<int> OnScoreChanged = delegate { };
    public Action<string, int> OnBestScoreChanged = delegate { };
    public Action<bool> OnGameOver = delegate { };

    private void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                Brick brick = Instantiate(BrickPrefab, position, Quaternion.identity);
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
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
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

    private void AddPoint(int point)
    {
        m_Points += point;
        OnScoreChanged?.Invoke(m_Points);
    }

    private void TryToSetBestScore(int score)
    {
        if (score >= DataManager.Instance.highScore)
        {
            DataManager.Instance.highScore = score;

            DataManager.Instance.Save();

            OnBestScoreChanged(DataManager.Instance.currentPlayerName, DataManager.Instance.highScore);
        }
    }

    public void GameOver()
    {
        m_GameOver = true;
        TryToSetBestScore(m_Points);
        OnGameOver?.Invoke(true);
    }
}
