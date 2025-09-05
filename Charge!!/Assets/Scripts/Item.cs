using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemGenerator.ITEM_TYPE itemType;
    [SerializeField] Sprite[] itemSprites;

    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    public void Initialize(ItemGenerator.ITEM_TYPE type)
    {
        itemType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch(type)
        {
            case ItemGenerator.ITEM_TYPE.NOMAL:
                spriteRenderer.sprite = itemSprites[0];
                break;
            case ItemGenerator.ITEM_TYPE.RARE:
                spriteRenderer.sprite = itemSprites[1];
                break;
            case ItemGenerator.ITEM_TYPE.BOMB:
                spriteRenderer.sprite = itemSprites[2];
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.AddItemCnt(itemType);
            Destroy(this.gameObject);
        }
    }
}
