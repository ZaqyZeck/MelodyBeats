using Unity.VisualScripting;
using UnityEngine;

public class RhytemArrowMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f, perfectDistance = 0.3f;
    private bool canMove = true;

    [SerializeField] private ScoreCounter counter;
    [SerializeField] private GameManager gameManager;

    private Collider2D currentCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision;

        if (currentCollision.gameObject.CompareTag("FinishLine"))
        {
            gameManager.FinishGame();
            canMove = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentCollision)
            currentCollision = null;
    }

    private void Update()
    {
        //System.DateTime now = System.DateTime.Now;
        //Debug.Log("Sekarang: " + now);

        //string formatted = now.ToString("HH:mm:ss");
        if (!canMove) return;

        transform.position += (Vector3)(Vector2.right * speed * Time.deltaTime);

        if (currentCollision == null) return;

        string objName = currentCollision.gameObject.name;

        if (objName.Contains("Left") && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RegisterSuccess();
        }
        else if (objName.Contains("Right") && Input.GetKeyDown(KeyCode.RightArrow))
        {
            RegisterSuccess();
        }
        else if (objName.Contains("Up") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            RegisterSuccess();
        }
        else if (objName.Contains("Down") && Input.GetKeyDown(KeyCode.DownArrow))
        {
            RegisterSuccess();
        }
    }

    void RegisterSuccess()
    {
        if (CheckDistance(currentCollision.gameObject)) counter.AddPerfect();
        else counter.AddSuccess();
        Destroy(currentCollision.gameObject);
        currentCollision = null;
    }

    bool CheckDistance(GameObject arrow)
    {
        float distance = Vector2.Distance(arrow.transform.position, transform.position);
        if (distance < perfectDistance)
        {
            return true;
        }
        return false;
    }

}
