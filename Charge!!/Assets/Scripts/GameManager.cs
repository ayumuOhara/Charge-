using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCntText;
    [SerializeField] TextMeshProUGUI playTimeText;

    int itemCnt = 0;

    public float playTime = 60.0f;

    public bool isPlaying = false;

    void Start()
    {
        isPlaying = true;
        StartCoroutine(Timer());
    }

    private void Update()
    {
        itemCntText.text = $"{itemCnt} pt";
        playTimeText.text = playTime.ToString("F1");
    }

    IEnumerator Timer()
    {
        while (true)
        {
            playTime -= Time.deltaTime;
            
            if (playTime <= 0)
            {
                isPlaying = false;
                playTime = 0;
                yield break;
            }
            
            yield return null;
        }
    }

    public void AddItemCnt(int add)
    {
        itemCnt += add;
    }

    public void RemoveItemCnt(int remove)
    {
        itemCnt -= remove;
    }
}
