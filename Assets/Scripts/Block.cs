using UnityEngine;

public class Block : MonoBehaviour
{
    public int answerID;

    [SerializeField]
    Character[] characters;

    float totalLength;

    int Select()
    {
        characters[answerID].gameObject.SetActive(false);
        answerID = Random.Range(0, characters.Length);
        characters[answerID].gameObject.SetActive(true);

        return answerID;
    }

    public void Init(float length)
    {
        totalLength = length;
        Select();
    }

    public void Next()
    {
        transform.Translate(0, 0, totalLength * -1, Space.Self);
        Select();
    }

    public void Success()
    {
        characters[answerID].Shot(() => Next());
        transform.SetAsLastSibling();
    }

    public void Fail()
    {

    }
}
