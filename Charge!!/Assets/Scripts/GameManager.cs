using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemCntText;
    [SerializeField] TextMeshProUGUI playTimeText;

    [SerializeField] GameObject countDownObj;
    [SerializeField] GameObject goSpriteObj;

    [SerializeField] Image countDownImage;
    [SerializeField] Sprite[] countDownSprites;


    int itemCnt = 0;

    public float playTime = 60.0f;

    public bool isPlaying = false;

    void Start()
    {
        StartCoroutine(GameStart());
    }

    private void Update()
    {
        itemCntText.text = $"{itemCnt} pt";
        playTimeText.text = playTime.ToString("F1");
    }

    IEnumerator GameStart()
    {
        var countDown = 3.0f;

        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            countDown -= Time.deltaTime;

            if (countDown < 0)
            {
                countDownObj.SetActive(false);
                goSpriteObj.SetActive(true);

                yield return new WaitForSeconds(0.5f);
                goSpriteObj.SetActive(false);
                isPlaying = true;
                break;
            }

            if (countDown >= 3)
            {
                countDownImage.sprite = countDownSprites[2];
            }
            else if(countDown >= 2)
            {
                countDownImage.sprite = countDownSprites[1];
            }
            else
            {
                countDownImage.sprite = countDownSprites[0];
            }

            yield return null;
        }

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

    public void AddItemCnt(ItemGenerator.ITEM_TYPE item)
    {
        switch (item)
        {
            case ItemGenerator.ITEM_TYPE.NOMAL:
                itemCnt += 10;
                break;
            case ItemGenerator.ITEM_TYPE.RARE:
                itemCnt += 30;
                break;
            case ItemGenerator.ITEM_TYPE.BOMB:
                itemCnt -= 5;
                break;
        }
    }

    public void RemoveItemCnt(int remove)
    {
        itemCnt -= remove;
    }
}
