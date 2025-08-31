using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 1f;
    public bool moveUp = true;   // toggle untuk arah

    private Vector2 direction;

    void Start()
    {
        // arah awal: kanan +45 derajat
        SetDirection(45f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (moveUp)
                SetDirection(-45f); // kanan-bawah
            else
                SetDirection(45f);  // kanan-atas

            moveUp = !moveUp;
        }

        // Gerakkan object terus ke arah yang sudah ditentukan
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void SetDirection(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "target")
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space))
    //        {
    //            if (moveUp)
    //                SetDirection(-45f); // kanan-bawah
    //            else
    //                SetDirection(45f);  // kanan-atas

    //            moveUp = !moveUp;
    //        }
    //    }
    //}
}
