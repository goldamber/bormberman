using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Actions : MonoBehaviour
{
    public Image preloader;
    public GameObject img;

    public void Start()
    {
        img.SetActive(false);

        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.SetInt("Score", 0);
    }

    public void StartClick()
    {
        StartCoroutine(LoadAScene());

        foreach (Text item in preloader.transform.GetComponentsInChildren<Text>())
        {
            item.enabled = false;
        }
        foreach (Button item in preloader.transform.GetComponentsInChildren<Button>())
        {
            item.enabled = false;
        }
        foreach (Image item in preloader.transform.GetComponentsInChildren<Image>())
        {
            item.enabled = false;
        }
        img.SetActive(true);
    }

    IEnumerator LoadAScene()
    {
        yield return new WaitForSeconds(3);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}