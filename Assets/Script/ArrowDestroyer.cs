using UnityEngine;

public class ArrowDestroyer : MonoBehaviour
{
    [SerializeField] private ScoreCounter counter;
    [SerializeField] private GameObject target;

    [Header("Follow Settings")]
    [SerializeField] private float speed = 5f; // kecepatan mengejar
    [SerializeField] private float offsetX = -5f; // offset ke kiri

    private MovementScript playerMovement;

    private void Awake()
    {
        playerMovement = target.GetComponent<MovementScript>();
    }

    private void Update()
    {
        if (target == null) return;

        Vector3 targetPos = target.transform.position + new Vector3(offsetX, 0, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ArrowBox"))
        {
            counter.AddFailed();

            playerMovement.addEnergy(-25);
            playerMovement.AddCombo(false);

            //if (playerMovement != null && playerMovement.GetCurrentCollision() == collision)
            //{
            //    playerMovement.ClearCollision();
            //}

            Destroy(collision.gameObject);
        }
    }
}
