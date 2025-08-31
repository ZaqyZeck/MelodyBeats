using UnityEngine;



public class ArrowDestroyer : MonoBehaviour
{
    [SerializeField] private SuccessCounter counter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ArrowBox")
        {
            counter.addFailed();
            Destroy(collision.gameObject);
        }
    }
}
