using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Ins { get; private set; }

    public FirebaseFirestore db { get; private set; }
    public FirebaseAuth auth { get; private set; }
    FirebaseApp app;

    public CollectionReference playerCol { get; private set; }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (Ins != null && Ins != this)
        {
            Debug.Log("firebase manager ada 2" + Ins);
            Destroy(gameObject);
            yield break;
        }

        Ins = this;
        DontDestroyOnLoad(gameObject);

        var checkTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => checkTask.IsCompleted);

#if UNITY_EDITOR
        app = FirebaseApp.Create(FirebaseApp.DefaultInstance.Options, Random.Range(-100f, 100f).ToString());
        db = FirebaseFirestore.GetInstance(app);
        auth = FirebaseAuth.GetAuth(app);
#else
        app = FirebaseApp.DefaultInstance;
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
#endif

#if UNITY_EDITOR
        playerCol = db.Collection("Player-Test");
#else
        playerCol = db.Collection("Player");
#endif

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Dictionary<string, object> data = new Dictionary<string, object> {
                { "nama", "Budi" },
                { "email", "Budi@gmail.com" }
            };
            db.Collection("Player-Test").Document("Budi").SetAsync(data).ContinueWithOnMainThread(task => {
                Debug.Log("Save success!");
                Debug.Log("Exception: " + (task.Exception != null ? task.Exception.Message + "\n" + task.Exception.StackTrace : ""));
            });
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Dictionary<string, object> data = new Dictionary<string, object> {
                { "nama", "Budi Update" },
            };
            db.Collection("Player-Test").Document("Budi").UpdateAsync(data).ContinueWithOnMainThread(task => {
                Debug.Log("Update done!");
                Debug.Log("Exception: " + (task.Exception != null ? task.Exception.Message + "\n" + task.Exception.StackTrace : ""));
            });
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Dictionary<string, object> data = new Dictionary<string, object> {
                { "email", "Budi@update.com" }
            };
            db.Collection("Player-Test").Document("Budi").SetAsync(data, SetOptions.MergeAll).ContinueWithOnMainThread(task => {
                Debug.Log("Save success!");
                Debug.Log("Exception: " + (task.Exception != null ? task.Exception.Message + "\n" + task.Exception.StackTrace : ""));
            });
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(TestLoadCor());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            db.Collection("Player-Test").Document("Budi").DeleteAsync();
            Debug.Log("Delete success!");
        }
    }

    IEnumerator TestLoadCor()
    {
        var getTask = db.Collection("Player-Test").Document("Budi").GetSnapshotAsync();
        yield return new WaitUntil(() => getTask.IsCompleted);

        if(getTask.Exception != null)
        {
            Debug.LogError("Eror testLoadCor: " + getTask.Exception.Message + "\n" + getTask.Exception.StackTrace);
        }

        DocumentSnapshot snapshot = getTask.Result;
        PlayerData playerData = new PlayerData();

        snapshot.TryGetValue(nameof(playerData.nama), out playerData.nama);
        snapshot.TryGetValue(nameof(playerData.email), out playerData.email);
        Debug.Log("Result playerData: " + JsonUtility.ToJson(playerData) + " ** nama: " + playerData.nama + " ** email: " + playerData.email);

        //foreach (var c in getTask.Result.ToDictionary())
        //{
        //    Debug.Log(c.Key + ": " + c.Value);
        //}
        Debug.Log("Load success!");
    }

    private void OnDisable()
    {
        db.TerminateAsync();
        auth.Dispose();
        app.Dispose();
    }
}

[System.Serializable]
public class PlayerData
{
    public string nama;
    public string email;
}
