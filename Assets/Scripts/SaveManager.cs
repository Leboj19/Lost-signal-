using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveEntry
{
    public string key;
    public float value;
}

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public Vector3 position;
    public float yaw;
    public List<SaveEntry> values = new List<SaveEntry>();
}

public class SaveManager : MonoBehaviour
{
    static SaveManager instance;
    static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("SaveManager");
                instance = go.AddComponent<SaveManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    static readonly Dictionary<string, float> values = new Dictionary<string, float>();

    public static bool Continuing { get; private set; }
    public static event System.Action OnBeforeSave;

    static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

    public static bool HasSave() => File.Exists(SavePath);

    public static void SetValue(string key, float value) => values[key] = value;

    public static float GetValue(string key, float fallback = 0f)
    {
        return values.TryGetValue(key, out float v) ? v : fallback;
    }

    public static bool Save()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("SaveManager: no object tagged 'Player' in this scene.");
            return false;
        }

        OnBeforeSave?.Invoke();

        var data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name,
            position = player.transform.position,
            yaw = player.transform.eulerAngles.y
        };
        foreach (var kv in values)
            data.values.Add(new SaveEntry { key = kv.Key, value = kv.Value });

        File.WriteAllText(SavePath, JsonUtility.ToJson(data, true));
        return true;
    }

    public static void Continue()
    {
        if (!HasSave()) return;

        var data = JsonUtility.FromJson<SaveData>(File.ReadAllText(SavePath));
        values.Clear();
        foreach (var e in data.values) values[e.key] = e.value;

        Continuing = true;
        Time.timeScale = 1f;
        Instance.StartCoroutine(Instance.LoadRoutine(data));
    }

    public static void DeleteSave()
    {
        if (HasSave()) File.Delete(SavePath);
    }

    IEnumerator LoadRoutine(SaveData data)
    {
        var op = SceneManager.LoadSceneAsync(data.sceneName);
        while (!op.isDone) yield return null;

        yield return null;                    // let every Start() run first
        yield return new WaitForEndOfFrame();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var cc = player.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;   // a controller overrides direct moves

            player.transform.SetPositionAndRotation(data.position, Quaternion.Euler(0f, data.yaw, 0f));

            if (cc != null) cc.enabled = true;
            var rb = player.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;
        }

        Continuing = false;
    }
}