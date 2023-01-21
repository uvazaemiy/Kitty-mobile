using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_Circle : MonoBehaviour
{
    [SerializeField] private GameObject StartPanel, EndPanel;
    [SerializeField] private Text Score;
    [Space]
    [SerializeField] private GameObject Sticks, StickPref;

    [System.NonSerialized] public bool border = false;
    private int k, n = 1, score = 0;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void MoveTrack()
    {
        Sticks.transform.position += Vector3.left;
        Invoke("MoveTrack", 0);
    }

    public void BorderExit(GameObject OldStick, int type)
    {
        if (type == 1)
            Destroy(OldStick.gameObject);
        else
        {
            GameObject NewStick = Instantiate(StickPref, Sticks.transform);
            NewStick.transform.localScale = new Vector3(Random.Range(200, 500), 30, 1);
            if (!border)
                k = Random.Range(-1, 0);
            else
                k = -k;
            k = k == 0 ? 1 : k;
            NewStick.transform.localRotation = Quaternion.Euler(0, 0, k > 0 ? Random.Range(30, 15) : Random.Range(-30, -15));
            NewStick.transform.position = OldStick.transform.GetChild(0).transform.position;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartPanel.SetActive(false);
        Score.gameObject.SetActive(true);
        MoveTrack();
        ChangeScore();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameCircle");
    }

    public void EndGame()
    {
        CancelInvoke("MoveTrack");
        CancelInvoke("ChangeScore");
        Time.timeScale = 0;
        Score.gameObject.SetActive(false);
        EndPanel.SetActive(true);
        EndPanel.transform.Find("Score").GetComponent<Text>().text = "Your score: " + score.ToString();
        Wallet.Add(score);
    }

    private void ChangeScore()
    {
        Score.text = "Score: " + score.ToString();
        score += 5 * n;
        if (score >= 40 * n)
        {
            Time.timeScale += 0.3f;
            n++;
        }
        Invoke("ChangeScore", 2);
    }
}
