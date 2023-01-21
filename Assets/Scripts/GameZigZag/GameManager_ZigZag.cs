using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_ZigZag : MonoBehaviour
{
    [SerializeField] private GameObject[] Chunks;
    [SerializeField] private GameObject[] StartChunks;
    [SerializeField] private Transform ChunksGroup;
    [SerializeField] private Transform Player;
    [Space]
    [SerializeField] private Text Score;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject StartButton;
    private List<GameObject> ChunksOnScene = new List<GameObject>();
    private int score = 0;

    private void Start()
    {
        foreach (GameObject temp in StartChunks)
            ChunksOnScene.Add(temp);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Player.position.x >= ChunksOnScene[ChunksOnScene.Count - 1].transform.position.x - 30)
            CreateNewChunk();
    }

    private void CreateNewChunk()
    {
        Time.timeScale += 0.1f;
        if (ChunksOnScene.Count > 10)
        {
            Destroy(ChunksOnScene[0]);
            ChunksOnScene.Remove(ChunksOnScene[0]);
        }
        GameObject NewChunk = Instantiate(Chunks[Random.Range(0, Chunks.Length)], ChunksGroup);
        NewChunk.transform.position = ChunksOnScene[ChunksOnScene.Count - 1].transform.GetChild(0).transform.position;
        NewChunk.transform.Translate(new Vector3(NewChunk.GetComponent<ChunkData>().x, 0, NewChunk.GetComponent<ChunkData>().z));
        ChunksOnScene.Add(NewChunk);
    }

    public void PickUpLoot()
    {
        score += 15;
        Score.text = "Score: " + score.ToString();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        StartButton.SetActive(false);
        Score.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        Time.timeScale = 0;
        Score.gameObject.SetActive(false);
        EndPanel.SetActive(true);
        EndPanel.transform.Find("Score").GetComponent<Text>().text = "Your score: " + score;
        Wallet.Add(score);
    }
}
