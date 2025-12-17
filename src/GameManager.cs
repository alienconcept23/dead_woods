using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum Difficulty { Easy, Normal, Hard, Hardcore }
    public Difficulty difficulty = Difficulty.Normal;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetDifficulty(Difficulty d)
    {
        difficulty = d;
        // apply global scaling rules here (placeholder)
    }
}
