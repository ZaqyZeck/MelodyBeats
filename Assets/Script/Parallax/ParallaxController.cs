using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    private float textureUnitSizeX;
    private float textureUnitSizeY;
    private float parentWidth;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        // Ambil komponen sprite renderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Ambil texture dari sprite renderer
        texture = spriteRenderer.sprite.texture;
        textureUnitSizeX = texture.width / spriteRenderer.sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / spriteRenderer.sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (infiniteHorizontal)
        {
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offestPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector2(cameraTransform.position.x + offestPositionX, transform.position.y);
            }

        }

        if (infiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offestPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector2(transform.position.x, cameraTransform.position.y + offestPositionY);
            }

        }
    }
}
