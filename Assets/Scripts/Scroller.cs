using System.Collections;
using UnityEngine;

public class Scroller : MonoBehaviour
{   
    [SerializeField]
    float scrollAmount;
    [SerializeField]
    float scrollTime;

    [SerializeField]
    float nextPos;    

    public void Scroll()
    {
        nextPos += scrollAmount;
        StartCoroutine(ScrollRoutiune());
    }

    IEnumerator ScrollRoutiune()
    {
        while (transform.position.z < nextPos)
        {
            transform.Translate(0, 0, scrollTime);
            yield return null;
        }

        yield break;
    }

    public void InitBlocks()
    {
        float totalLength = transform.childCount * (int)scrollAmount;

        for (int index = 0; index < transform.childCount; index++)
        {
            Block block = transform.GetChild(index).GetComponent<Block>();
            block.Init(totalLength);
        }
    }

    public Block GetBlock()
    {
        return transform.GetChild(0).GetComponent<Block>();
    }
}
