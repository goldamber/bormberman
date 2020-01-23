using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IceBehaviour : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero" && gameObject.tag == "Door")
            StartCoroutine(NextLevel(collision.gameObject, "Level"));
    }

    IEnumerator NextLevel(GameObject col, string txtScene)
    {
        col.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        if (col.GetComponent<Rigidbody2D>() != null)
            Destroy(col.GetComponent<Rigidbody2D>());
        if (col.GetComponent<BoxCollider2D>() != null)
            Destroy(col.GetComponent<BoxCollider2D>());
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 10 + 1 * PlayerPrefs.GetInt("Score"));
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadSceneAsync(txtScene);
    }
}