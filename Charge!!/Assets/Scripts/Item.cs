using UnityEngine;

public class Item : MonoBehaviour
{
    ItemGenerator.ITEM_TYPE itemType;

    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    public void Initialize(ItemGenerator.ITEM_TYPE type)
    {
        itemType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch(type)
        {
            case ItemGenerator.ITEM_TYPE.NOMAL:
                spriteRenderer.color = Color.white;
                break;
            case ItemGenerator.ITEM_TYPE.RARE:
                spriteRenderer.color = Color.softYellow;
                break;
            case ItemGenerator.ITEM_TYPE.BOMB:
                spriteRenderer.color = Color.black;
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
            switch(itemType)
            {
                case ItemGenerator.ITEM_TYPE.NOMAL:
                    gameManager.AddItemCnt(10); break;
                case ItemGenerator.ITEM_TYPE.RARE:
                    gameManager.AddItemCnt(30); break;
                case ItemGenerator.ITEM_TYPE.BOMB:
                    gameManager.RemoveItemCnt(5); break;
            }

            Destroy(this.gameObject);
        }
    }
}
