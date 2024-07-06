using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class AuthUI : MonoBehaviour
{
    [Header("Login")]
    [SerializeField] TMP_InputField loginEmailInp;
    [SerializeField] TMP_InputField loginPassInp;

    [Header("Login")]
    [SerializeField] TMP_InputField RegisterEmailInp;
    [SerializeField] TMP_InputField RegisterPassInp;
    [SerializeField] TMP_InputField RegisterUsernameInp;

    [Header("Info")]
    [SerializeField] TextMeshProUGUI infoTmp;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Login()
    {
        Debug.Log("Login called");
        FirebaseManager.Ins.auth.SignInWithEmailAndPasswordAsync(loginEmailInp.text, loginPassInp.text).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null) Debug.LogError(task.Exception.Message + "\n" + task.Exception.StackTrace);

            Debug.Log("login berhasil");
            infoTmp.text = "Login berhasil! " + FirebaseManager.Ins.auth.CurrentUser.Email + " " + FirebaseManager.Ins.auth.CurrentUser.DisplayName;

        });
    }

    public void GuestLogin()
    {
        FirebaseManager.Ins.auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task =>
        {
            PlayerPrefs.SetString("guestId", task.Result.User.UserId);

            Debug.Log("creationTime: " + task.Result.User.Metadata.CreationTimestamp + " ** lastLogin: " + task.Result.User.Metadata.LastSignInTimestamp);

            FirebaseManager.Ins.playerCol.Document(task.Result.User.UserId).GetSnapshotAsync().ContinueWithOnMainThread(getTask =>
            {
                if (!getTask.Result.Exists)
                {
                    Dictionary<string, object> data = new()
                {
                    { "nama", RegisterUsernameInp.text },
                };
                    FirebaseManager.Ins.playerCol.Document(task.Result.User.UserId).SetAsync(data, Firebase.Firestore.SetOptions.MergeAll);

                    task.Result.User.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
                    {
                        DisplayName = RegisterUsernameInp.text
                    });

                    infoTmp.text = "Login as guest success!" + RegisterUsernameInp.text;
                }
            });
        });
    }


    public void Register()
    {
        FirebaseManager.Ins.auth.CreateUserWithEmailAndPasswordAsync(RegisterEmailInp.text, RegisterPassInp.text).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception.Message + "\n" + task.Exception.StackTrace);
                return;
            }

            Debug.Log("Register Berhasil");
            infoTmp.text = "Register Berhasil! " + FirebaseManager.Ins.auth.CurrentUser.Email + " " +
            FirebaseManager.Ins.auth.CurrentUser.DisplayName;

            Dictionary<string, object> data = new()
            {
                { "nama", RegisterUsernameInp.text },
                { "email", FirebaseManager.Ins.auth.CurrentUser.Email }
            };
            FirebaseManager.Ins.playerCol.AddAsync(data);

            task.Result.User.UpdateUserProfileAsync(new Firebase.Auth.UserProfile
            {
                DisplayName = RegisterUsernameInp.text
            });
        });
    }
}
