using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public void ToGameScene()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ToGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}