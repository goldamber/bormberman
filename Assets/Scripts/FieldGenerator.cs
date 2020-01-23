using UnityEngine;
using UnityEngine.UI;

public class FieldGenerator : MonoBehaviour
{
    public Canvas Field;
    public Canvas Items;

    public GameObject Box;
    public GameObject IceBox;
    public GameObject Hero;
    public GameObject OrangeEnemy;
    public GameObject BlueEnemy;
    public GameObject RedEnemy;
    public GameObject Grass;

    public Text txtLives;
    public Text txtLevel;
    public Text txtScore;
    public Text txtEnemies;

    static Text tmpScore;
    static Text tmpEnemies;

    void Start ()
    {
        tmpScore = txtScore;
        tmpEnemies = txtEnemies;
        float width = Field.GetComponent<RectTransform>().rect.width;
        float height = Field.GetComponent<RectTransform>().rect.height;
        int cellSize = 70;
        int count = 21;

        int len = 4;
        int level = PlayerPrefs.GetInt("Level");
        if (!PlayerPrefs.HasKey("Level"))
            level = 1;

        for (int i = 0; i < level + 1; i++)
        {
            Instantiate(OrangeEnemy, new Vector3(Random.Range(-len, len), Random.Range(-len, len)), new Quaternion(), Items.transform);
        }
        for (int i = 0; i < (level - 2) + 1; i++)
        {
            Instantiate(BlueEnemy, new Vector3(Random.Range(-len, len), Random.Range(-len, len)), new Quaternion(), Items.transform);
        }
        for (int i = 0; i < (level - 5) + 1; i++)
        {
            Instantiate(RedEnemy, new Vector3(Random.Range(-len, len), Random.Range(-len, len)), new Quaternion(), Items.transform);
        }

        for (int i = 0; i < height - cellSize; i += cellSize)
        {
            for (int j = 0; j < width - cellSize; j += cellSize)
            {
                GameObject obj = ((i == 0) || (i + cellSize > height - cellSize) || (j == 0) || ((j / cellSize) % 2 == 0 && (i / cellSize) % 2 == 0) || (j + cellSize > width - cellSize)) ? Box : Grass;
                if (obj == Grass)
                {
                    if (Random.Range(-100, 100) % 3 == 0 && (i != cellSize && j != cellSize) && Random.Range(-100, 100) > 0 && count > 0 && GameObject.FindGameObjectsWithTag("Door").Length == 0)
                    {
                        obj = IceBox;
                        obj.tag = "Door";
                        count--;
                    }
                }

                Instantiate(obj, Field.transform);
            }
        }
        
        txtScore.text = "Score: " + PlayerPrefs.GetInt("Score");
        txtEnemies.text = "Enemies: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        txtLevel.text = "Level: " + PlayerPrefs.GetInt("Level");
        txtLives.text = "Lives: " + PlayerPrefs.GetInt("Lives");        

        Instantiate(Hero, Items.transform);
    }

    public static void ChEnemy(string txt)
    {
        tmpEnemies.text = txt;
    }
    public static void ChScore(int num)
    {
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + num);
        tmpScore.text = "Score: " + PlayerPrefs.GetInt("Score");
    }
}