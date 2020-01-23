using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    float speed = 3;

    private void FixedUpdate()
    {
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (item.GetComponent<Animator>().GetBool("IsDead") == true)
                StartCoroutine(CheckEmeny((item)));
        }
        if (hero != null && hero.GetComponent<Animator>().GetBool("IsDead") == true)
            StartCoroutine(CheckHero());

        if (hero == null || hero.GetComponent<Rigidbody2D>() == null)
            return;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 1);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x - 1 * speed * Time.deltaTime), (hero.transform.position.y)));
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 2);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x + 1 * speed * Time.deltaTime), (hero.transform.position.y)));
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 3);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x), (hero.transform.position.y + 1 * speed * Time.deltaTime)));
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            hero.GetComponent<Animator>().SetInteger("Direction", 4);
            hero.GetComponent<Rigidbody2D>().MovePosition(new Vector2((hero.transform.position.x), (hero.transform.position.y - 1 * speed * Time.deltaTime)));
        }
        else
            hero.GetComponent<Animator>().SetInteger("Direction", 0);
    }
    
    IEnumerator CheckHero()
    {
        yield return new WaitForSeconds(2.5f);
        GameObject hero = GameObject.FindGameObjectWithTag("Hero");
        if (hero != null)
        {
            string txtScene = "";
            if (PlayerPrefs.GetInt("Lives") <= 1)
                txtScene = "GameOver";
            else
            {
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
                txtScene = "Game";
            }
            Destroy(hero);
            SceneManager.LoadSceneAsync(txtScene);
        }
    }
    IEnumerator CheckEmeny(GameObject enemy)
    {
        yield return new WaitForSeconds(2.5f);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (item == enemy)
                Destroy(enemy);
        }
        FieldGenerator.ChEnemy("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy").Length));
    }
}