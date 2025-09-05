using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] Image chargeUI;

    Rigidbody2D rb;
    Vector3 playerPos = Vector3.zero;

    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] float minCharge = 0f;
    [SerializeField] float maxCharge = 1f;
    [SerializeField] float chargePow = 0;
    [SerializeField] float drag = 1f;          // 減速の速さ（大きいほどすぐ止まる）

    float charge = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = drag;
    }

    void Update()
    {
        if (gameManager.isPlaying)
        {
            Charge();
            ChargeDash();
        }

        ClampPlayerPos();
    }

    void Charge()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            charge += Time.deltaTime;
            charge = Mathf.Clamp(charge, minCharge, maxCharge);
            chargePow = (charge / maxCharge) * 100;
            chargeUI.fillAmount = charge / maxCharge;
        }
    }

    void ChargeDash()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Vector2 dir = Vector2.zero;

            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            dir = new Vector2(x, y);

            if (dir != Vector2.zero)
            {
                // 瞬間的に加速
                rb.linearVelocity = dir * (chargePow * moveSpeed * 0.1f);
            }

            chargePow = 0;
            charge = 0;
            chargeUI.fillAmount = 0;
        }
    }

    void ClampPlayerPos()
    {
        playerPos = transform.position;
        playerPos.x = Mathf.Clamp(playerPos.x, -8.3f, 8.3f);
        playerPos.y = Mathf.Clamp(playerPos.y, -4.4f, 4.4f);
        transform.position = playerPos;
    }
}
