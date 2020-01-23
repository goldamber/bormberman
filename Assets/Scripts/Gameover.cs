using UnityEngine;
using UnityEngine.UI;

public class Gameover : MonoBehaviour
{
    public Text txtScore;

    void Start ()
    {
        txtScore.text = "Your score: " + PlayerPrefs.GetInt("Score");
	}
}