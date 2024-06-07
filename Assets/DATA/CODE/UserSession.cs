using UnityEngine;

public class UserSession : MonoBehaviour
{
    public static UserSession Instance;

    public string Username { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUsername(string username)
    {
        Username = username;
    }
}
