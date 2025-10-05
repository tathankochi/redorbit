using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<LevelData> levels; // Danh sách các level
    [SerializeField] private Pipe _cellPrefab;

    public int n_count = 0;
    private bool hasGameFinished;
    private Pipe[,] pipes;
    private List<Pipe> startPipes;
    private int count;
    private string jsonFile;

    private void Awake()
    {
        jsonFile = Application.persistentDataPath + "/count.json";
        count = JsonFileIO.ReadCountJson(jsonFile);
        //JsonFileIO_COUNT.WriteCountJson(jsonFile, 0);
        n_count = JsonFileIO_COUNT.ReadCountJson(jsonFile);
        Instance = this;
        hasGameFinished = false;
        SpawnLevel(n_count);
    }

    private void SpawnLevel(int n_count)
    {
        Debug.Log("Entering SpawnLevel with n_count: " + n_count);

        // Kiểm tra xem n_count có hợp lệ và level có tồn tại không
        if (n_count < 0 || n_count >= levels.Count || levels[n_count] == null)
        {
            Debug.LogError($"LevelData is null or invalid for n_count = {n_count}!");
            return;
        }

        LevelData currentLevel = levels[n_count];
        pipes = new Pipe[currentLevel.Row, currentLevel.Column];
        startPipes = new List<Pipe>();

        for (int i = 0; i < currentLevel.Row; i++)
        {
            for (int j = 0; j < currentLevel.Column; j++)
            {
                Vector2 spawnPos = new Vector2(j * 2f + 2f * 0.5f, i * 2f + 2f * 0.5f);
                Pipe tempPipe = Instantiate(_cellPrefab);
                // Scale cả Pipe (bao gồm sprite & colliders con)
        tempPipe.transform.localScale = Vector3.one * 2f;
                tempPipe.transform.position = spawnPos;
                tempPipe.Init(currentLevel.Data[i * currentLevel.Column + j]);
                pipes[i, j] = tempPipe;
                if (tempPipe.PipeType == 1)
                {
                    startPipes.Add(tempPipe);
                }   
            }
        }

        //Camera.main.orthographicSize = Mathf.Max(currentLevel.Row, currentLevel.Column);
        float mapWidth = currentLevel.Column * 2f;
        float mapHeight = currentLevel.Row * 2f;
        float maxDimen = Mathf.Max(mapWidth, mapHeight) * 0.5f;
        Camera.main.orthographicSize = (maxDimen) + 3f;
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = currentLevel.Column * 1f;
        cameraPos.y = currentLevel.Row * 1f;
        Camera.main.transform.position = cameraPos;

        StartCoroutine(ShowHint());
    }

    private void Update()
    {
        if (hasGameFinished) return;


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int row = Mathf.FloorToInt(mousePos.y/2);
        int col = Mathf.FloorToInt(mousePos.x/2);
        if (row < 0 || col < 0) return;
        if (row >= levels[n_count].Row) return;
        if (col >= levels[n_count].Column) return;

        if (Input.GetMouseButtonDown(0))
        {
            pipes[row, col].UpdateInput();
            StartCoroutine(ShowHint());
        }
    }

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(0.1f);
        CheckFill();
        CheckWin();
    }

    private void CheckFill()
    {
        if (n_count < 0 || n_count >= levels.Count || levels[n_count] == null)
        {
            Debug.LogError($"Level array is null in CheckFill for n_count = {n_count}!");
            return;
        }

        LevelData currentLevel = levels[n_count];
        int rowCount = currentLevel.Row;
        int colCount = currentLevel.Column;

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                Pipe tempPipe = pipes[i, j];
                if (tempPipe.PipeType != 0)
                {
                    tempPipe.IsFilled = false;
                }
            }
        }

        Queue<Pipe> check = new Queue<Pipe>();
        HashSet<Pipe> finished = new HashSet<Pipe>();
        foreach (var pipe in startPipes)
        {
            check.Enqueue(pipe);
        }

        while (check.Count > 0)
        {
            Pipe pipe = check.Dequeue();
            finished.Add(pipe);
            List<Pipe> connected = pipe.ConnectedPipes();
            foreach (var connectedPipe in connected)
            {
                if (!finished.Contains(connectedPipe))
                {
                    check.Enqueue(connectedPipe);
                }
            }
        }

        foreach (var filled in finished)
        {
            filled.IsFilled = true;
        }

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                Pipe tempPipe = pipes[i, j];
                tempPipe.UpdateFilled();
            }
        }
    }

    private void CheckWin()
    {
        if (n_count < 0 || n_count >= levels.Count || levels[n_count] == null)
        {
            Debug.LogError($"Level array is null in CheckWin for n_count = {n_count}!");
            return;
        }

        LevelData currentLevel = levels[n_count];
        for (int i = 0; i < currentLevel.Row; i++)
        {
            for (int j = 0; j < currentLevel.Column; j++)
            {
                if (!pipes[i, j].IsFilled)
                {
                    return;
                }
            }
        }

        count++;
        JsonFileIO.WriteCountJson(jsonFile, count);
        hasGameFinished = true;
        StartCoroutine(GameFinished());
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(2f);
        n_count++;

        // Kiểm tra nếu n_count vượt quá độ dài của levels thì quay về 0
        if (n_count >= levels.Count)
        {
            n_count = 0;
        }

        JsonFileIO.WriteCountJson(jsonFile, n_count);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Plants");
    }
}
