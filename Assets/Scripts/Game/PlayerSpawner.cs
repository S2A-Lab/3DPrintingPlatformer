using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public static PlayerSpawner Instance;

    [Header("Player Setup")]
    public GameObject playerPrefab;
    private GameObject currentPlayer;

    [Header("References to pass to Player")]
    public Grid grid;
    public UiManager uiManager;
    public PlayerInventory inventory;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SpawnPlayerForScene(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("CutScene"))
        {
            Debug.Log($"Scene '{scene.name}' starts with 'CutScene', skipping player spawn.");
            return;
        }

        SpawnPlayerForScene(scene.name);
    }

    private void SpawnPlayerForScene(string sceneName)
    {
        if (currentPlayer != null)
            Destroy(currentPlayer);

        Vector3 spawnPos = Vector3.zero;
        GameObject spawnObj = GameObject.FindWithTag("SpawnPoint");

        if (spawnObj != null)
        {
            spawnPos = spawnObj.transform.position;
        }
        else
        {
            Debug.LogWarning($"No SpawnPoint found with tag 'SpawnPoint' in scene '{sceneName}', defaulting to Vector3.zero");
        }

        currentPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        var builderMode = currentPlayer.GetComponent<BuilderMode>();
        if (builderMode != null)
        {
            builderMode.grid = grid;
            builderMode.inventory = inventory;
        }
    }
}
