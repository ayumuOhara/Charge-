using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemGenerator.ITEM_TYPE itemType;
    [SerializeField] Sprite[] itemSprites;

    GameManager gameManager;
    SpriteRenderer spriteRenderer;

    AudioClip getSE;
    [SerializeField] AudioClip[] SE;

    public void Initialize(ItemGenerator.ITEM_TYPE type)
    {
        itemType = type;
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch(type)
        {
            case ItemGenerator.ITEM_TYPE.NOMAL:
                spriteRenderer.sprite = itemSprites[0];
                getSE = SE[0];
                break;
            case ItemGenerator.ITEM_TYPE.RARE:
                spriteRenderer.sprite = itemSprites[1];
                getSE = SE[1];
                break;
            case ItemGenerator.ITEM_TYPE.BOMB:
                spriteRenderer.sprite = itemSprites[2];
                getSE = SE[2];
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
            AudioSource a = collision.gameObject.GetComponent<AudioSource>();
            a.PlayOneShot(getSE);
            gameManager.AddItemCnt(itemType);
            Destroy(this.gameObject);
        }
    }
}
