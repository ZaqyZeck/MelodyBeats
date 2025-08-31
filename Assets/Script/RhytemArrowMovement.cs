using Unity.VisualScripting;
using UnityEngine;

public class RhytemArrowMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f, perfectDistance = 0.3f;
    [SerializeField] private SuccessCounter counter;

    private Collider2D currentCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision; // simpan collider terakhir
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentCollision)
            currentCollision = null; // hapus kalau keluar
    }

    private void Update()
    {
        transform.position += (Vector3)(Vector2.right * speed * Time.deltaTime);

        if (currentCollision == null) return;

        string objName = currentCollision.gameObject.name;

        if (objName.Contains("Left") && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (checkDistance(currentCollision.gameObject)) counter.addPerfect();
            else counter.addSuccess();
            Destroy(currentCollision.gameObject);
        }
        else if (objName.Contains("Right") && Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (checkDistance(currentCollision.gameObject)) counter.addPerfect();
            else counter.addSuccess();
            Destroy(currentCollision.gameObject);
        }
        else if (objName.Contains("Up") && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (checkDistance(currentCollision.gameObject)) counter.addPerfect();
            else counter.addSuccess();
            Destroy(currentCollision.gameObject);
        }
        else if (objName.Contains("Down") && Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (checkDistance(currentCollision.gameObject)) counter.addPerfect();
            else counter.addSuccess();
            Destroy(currentCollision.gameObject);
        }
    }

    bool checkDistance(GameObject arrow)
    {
        float distance = Vector2.Distance(arrow.transform.position, transform.position);
        if (distance < perfectDistance)
        {
            return true;
        }
        return false;
    }

}
