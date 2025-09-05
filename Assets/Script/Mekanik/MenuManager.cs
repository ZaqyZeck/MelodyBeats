using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public async void PlayGameAsync(String SceneName)
    {
        await Task.Delay(200);
        SceneTransitionManager.instance.LoadSceneWithFade(SceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenPopUp(GameObject popUp)
    {
        popUp.SetActive(true);
    }

    public void ClosePopUp(GameObject popUp)
    {
        popUp.SetActive(false);
    }
}
