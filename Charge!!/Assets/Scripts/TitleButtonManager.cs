using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonManager : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnEndButton()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
