using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float speed = 1f, perfectDistance = 0.3f;
    public bool moveUp = true;

    public bool planeMode, arrowMode;// toggle untuk arah
    private Vector2 direction;

    //Dari RhytehmArrowMovement:

    private bool canMove = true;

    [SerializeField] private ScoreCounter counter;
    [SerializeField] private GameManager gameManager;
    private Collider2D currentCollision;

    //new

    [SerializeField] private Rigidbody2D blockBody;
    [SerializeField] private LineController lineController;
    [SerializeField] private GameObject lineParent;

    void Start()
    {
        // arah awal: kanan +45 derajat
        SetDirection(45f);
        planeMode = true;
        arrowMode = false;

        CreateLinePoint();
    }

    void Update()
    {
        if (!canMove) return;

        if (planeMode)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeDirection();
                CreateLinePoint();
            }
            // Gerakkan object terus ke arah yang sudah ditentukan
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
        else
        {
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision;

        if (currentCollision.gameObject.CompareTag("FinishLine"))
        {
            gameManager.FinishGame();
            canMove = false;
        }
        else if(currentCollision.gameObject.CompareTag("SwitchBox"))
        {
            SwitchMode();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == currentCollision)
            currentCollision = null;
    }

    private void ChangeDirection()
    {
        if (moveUp)
            SetDirection(-45f); // kanan-bawah
        else
            SetDirection(45f);  // kanan-atas

        moveUp = !moveUp;
    }

    private void SetDirection(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
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

    void SwitchMode()
    {
        planeMode = !planeMode;
        arrowMode = !arrowMode;
        
        CreateLinePoint();

        if (planeMode)
        {
            blockBody.gravityScale = 0;
            
        }
            
        else blockBody.gravityScale = 3;
    }

    void CreateLinePoint()
    {
        GameObject newLine = Instantiate(new GameObject());
        newLine.transform.SetParent(lineParent.transform);
        newLine.transform.position = transform.position;
        lineController.RemoveNewestLine();
        lineController.AddLine(newLine.transform);
        if (planeMode) lineController.AttachPlayer();
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
