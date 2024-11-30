using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        ScaleBackgroundToCamera();
    }
    void Update()
    {
        ScaleBackgroundToCamera();
    }
    void ScaleBackgroundToCamera()
    {
       
        Camera mainCamera = Camera.main;
        if (mainCamera == null || !mainCamera.orthographic)
        {
            Debug.LogError("Camera chính không tồn tại hoặc không phải camera Orthographic!");
            return;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("no sprite!");
            return;
        }
        float backgroundWPixels = spriteRenderer.sprite.texture.width;
        float backgroundHPixels = spriteRenderer.sprite.texture.height;


        float HUnits = mainCamera.orthographicSize * 2f; 
        float WUnits = HUnits * mainCamera.aspect; 
        float pixPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        float WPix = WUnits * pixPerUnit;
        float HPix = HUnits * pixPerUnit;

        float scaleX = WPix / backgroundWPixels;
        float scaleY = HPix / backgroundHPixels;


        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
