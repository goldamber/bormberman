using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    bool _isMoving = false;
    bool _col = false;
    Vector2 _goal;
    Transform _prev;
    
    void Start()
    {
        _goal = transform.position;
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;

        if (_isMoving)
        {
            if (pos == _goal)
            {
                _isMoving = false;
                GetComponent<Animator>().SetInteger("Direction", 0);
            }
            else
            {
                if (gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    GetComponent<Animator>().SetInteger("Direction", (pos.x < _goal.x) ? 2 : 1);
                    GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(pos, _goal, speed * Time.deltaTime));
                }
            }
        }
        else
        {
            float length = 2;
            List<int> dirs = Directions(length);
            int dir = dirs[UnityEngine.Random.Range(0, dirs.Count)];

            switch (dir)
            {
                case 0:
                    _goal = pos + new Vector2(0, length);
                    break;
                case 1:
                    _goal = pos + new Vector2(length, 0);
                    break;
                case 2:
                    _goal = pos + new Vector2(0, -length);
                    break;
                case 3:
                    _goal = pos + new Vector2(-length, 0);
                    break;
            }

            _isMoving = true;
        }
    }
    void Move(float length)
    {
        Vector2 pos = transform.position;

        if (gameObject.GetComponent<Rigidbody2D>() != null)
            GetComponent<Rigidbody2D>().MovePosition(Vector2.MoveTowards(pos, _goal, speed * Time.deltaTime));
        List<int> dirs = Directions(length);
        int dir = dirs[UnityEngine.Random.Range(0, dirs.Count)];

        switch (dir)
        {
            case 0:
                _goal = pos + new Vector2(0, length);
                break;
            case 1:
                _goal = pos + new Vector2(length, 0);
                break;
            case 2:
                _goal = pos + new Vector2(0, -length);
                break;
            case 3:
                _goal = pos + new Vector2(-length, 0);
                break;
        }
    }
    List<int> Directions(float len)
    {
        List<int> res = new List<int>();
        Vector2 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);

        if (!Block.GetBlock(pos.x, pos.y + len))
            res.Add(0);
        if (!Block.GetBlock(pos.x + len, pos.y))
            res.Add(1);
        if (!Block.GetBlock(pos.x, pos.y - len))
            res.Add(2);
        if (!Block.GetBlock(pos.x - len, pos.y))
            res.Add(3);

        return res;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hero")
        {
            if (PlayerPrefs.GetInt("Lives") <= 1)
                StartCoroutine(KillHero(collision.gameObject, "GameOver"));
            else
            {
                PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives") - 1);
                StartCoroutine(KillHero(collision.gameObject, "Game"));
            }
        }
        else if (collision.gameObject.tag == "Enemy")
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Move(1);
        if (collision.gameObject.tag == "Box")
        {
            _col = true;
            StartCoroutine(Escape(collision));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box")
            _col = false;
    }

    IEnumerator Escape(Collision2D collision)
    {
        yield return new WaitForSeconds(1.2f);
        if (Math.Abs(gameObject.transform.position.x - collision.transform.position.x) <= 0.55f && _col && Math.Abs(gameObject.transform.position.y - collision.transform.position.y) <= 0.55f)
        {
            FieldGenerator.ChEnemy("Enemies: " + (GameObject.FindGameObjectsWithTag("Enemy").Length - 1));
            Destroy(gameObject);
        }
    }
    IEnumerator KillHero(GameObject col, string txtScene)
    {
        if (col.GetComponent<Rigidbody2D>() != null)
            Destroy(col.GetComponent<Rigidbody2D>());
        if (col.GetComponent<BoxCollider2D>() != null)
            Destroy(col.GetComponent<BoxCollider2D>());

        col.gameObject.GetComponent<Animator>().SetBool("IsDead", true);
        
        yield return new WaitForSeconds(1.2f);
        Destroy(col);
        SceneManager.LoadSceneAsync(txtScene);
    }
}