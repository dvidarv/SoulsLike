using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager instance;
    [SerializeField] int worldSceneIndex = 1;
    private void Awake()
    {
        // THERE CAN ONLY BE ONE INSTANCE OF THIS SCRIPT AT A TIME, IF ANOTHER EXISTS, DESTROY IT
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public IEnumerator LoadNewGame()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        yield return null;
    }
}
