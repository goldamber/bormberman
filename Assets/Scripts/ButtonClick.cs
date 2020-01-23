using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void ExitClick()
    {
        StartCoroutine(LoadAScene());
    }

    IEnumerator LoadAScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Menu");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}