using UnityEngine;
using TMPro;

public class DisplayUsername : MonoBehaviour
{
    public TMP_Text usernameText;

    void Start()
    {
        if (UserSession.Instance != null)
        {
            usernameText.text = UserSession.Instance.Username;
        }
        else
        {
            usernameText.text = "Guest";
        }
    }
}
