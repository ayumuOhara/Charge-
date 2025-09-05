using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    public enum ITEM_TYPE
    {
        NOMAL,
        RARE,
        BOMB,
    }

    [SerializeField] GameObject item;
    [SerializeField] GameManager gameManager;

    Camera cam;

    float camHeight = 0f;
    float camWidth = 0f;
    Vector2 camPos = Vector2.zero;

    float time = 0;
    float[] generateTime = { 1.5f, 1.0f, 0.5f, 0.3f };

    float[] normalPer = { 50.0f, 45.0f, 40.0f, 15.0f };
    float[] rarePer = { 35.0f, 40.0f, 50.0f, 70.0f };
    float[] bombPer = { 15.0f, 15.0f, 10.0f, 15.0f };

    int level = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;

        // カメラのサイズ（orthographicSize は縦方向の半分のサイズ）
        camHeight = cam.orthographicSize * 2f;
        camWidth = camHeight * cam.aspect;

        // カメラの中心座標
        camPos = cam.transform.position;

        for (int i = 0; i < 10; i++)
        {
            Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isPlaying)
        {
            LevelUpper();

            time += Time.deltaTime;
            if (time > generateTime[level])
            {
                Generate();
                time = 0;
            }
        }        
    }

    void Generate()
    {
        var rndCnt = Random.Range(1, 3);

        for (int i = 0; i < rndCnt; i++)
        {
            float x = Random.Range(camPos.x - camWidth / 2f, camPos.x + camWidth / 2f);
            float y = Random.Range(camPos.y - camHeight / 2f, camPos.y + camHeight / 2f);
            var pos = new Vector2(x, y);

            GameObject g = Instantiate(item, pos, Quaternion.identity);

            var total = normalPer[level] + rarePer[level] + bombPer[level];
            float[] itemPercent = { normalPer[level], rarePer[level], bombPer[level] };

            float randomPoint = Random.value * total;
            for (int j = 0; j < itemPercent.Length; j++)
            {
                if (randomPoint < itemPercent[j])
                {
                    Item itemComp = g.GetComponent<Item>();

                    // enum の i番目を取得
                    ITEM_TYPE type = (ITEM_TYPE)j;

                    // Itemに渡す
                    itemComp.Initialize(type);

                    break;
                }
                else
                {
                    randomPoint -= itemPercent[j];
                }
            }
        }
    }

    void LevelUpper()
    {
        if (gameManager.playTime <= 10.0f)
        {
            level = 3;
        }
        else if (gameManager.playTime <= 25.0f)
        {
            level = 2;
        }
        else if (gameManager.playTime <= 40.0f)
        {
            level = 1;
        }
        else
        {
            level = 0;
        }
    }
}
