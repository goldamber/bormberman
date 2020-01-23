using UnityEngine;

public class Block : MonoBehaviour
{
    public static Block GetBlock(float x, float y)
    {
        Block[] blocks = FindObjectsOfType<Block>();

        foreach (Block item in blocks)
        {
            if (item.transform.position.x == x && item.transform.position.y == y)
                return item;
        }

        return null;
    }
    public static GameObject GetApprBlock(float x, float y)
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Box");
        float len = 0.3f;

        foreach (GameObject item in blocks)
        {
            if ((item.transform.position.y <= y + len && item.transform.position.y >= y - len) && (item.transform.position.x <= x + len && item.transform.position.x >= x - len))
                return item;
        }

        return null;
    }
}