using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Transform sprite;

    public float energy = 100;
    [SerializeField] private RectTransform energtUI;

    public int combo;
    [SerializeField] private Text textCombo;

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

        SetEnergyUI();

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
        else if (currentCollision.gameObject.CompareTag("DeathBox"))
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
        if (CheckDistance(currentCollision.gameObject))
        {
            addEnergy(20);
            counter.AddPerfect();
        }  
        else
        {
            addEnergy(10);
            counter.AddSuccess();
        }

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
        //if(moveUp) ChangeDirection();
        planeMode = !planeMode;
        arrowMode = !arrowMode;
        
        CreateLinePoint();

        if (planeMode)
        {
            blockBody.gravityScale = 0;
            
        }   
        else
        {
            //transform.rotation = Quaternion.identity; // kenapa saat aku tambahkan kode ini method jadi rusak
            blockBody.gravityScale = 3;
            sprite.rotation = Quaternion.identity;
            
        }  
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

    private void SetEnergyUI()
    {
        energy -= Time.deltaTime * 5;

        if (energy < 0) energy = 0;

        float maxWidth = 500f;
        Vector2 size = energtUI.sizeDelta;
        size.x = maxWidth * (energy / 100f);
        energtUI.sizeDelta = size;
    }

    public void addEnergy(float energyPlus)
    {
        if (combo > 5) energyPlus +=10;
        if (energy + energyPlus > 100) energy = 100;
        else energy += energyPlus;

        AddCombo(true);
    }

    public void AddCombo(bool success)
    {
        if (success)
        {
            combo++;
        }
        else
        {
            combo = 0;
        }

        textCombo.text = $"combo = {combo}";
    }
}
