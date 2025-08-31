using UnityEngine;



public class ArrowDestroyer : MonoBehaviour
{
    [SerializeField] private ScoreCounter counter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowBox")
        {
            counter.AddFailed();
            Destroy(collision.gameObject);
        }
    }
}
