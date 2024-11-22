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

       // Kích thước ảnh background (pixel)
        float backgroundWidthPixels = 2048f;
        float backgroundHeightPixels = 2048f;

        // Lấy kích thước viewport của camera dựa trên pixel
        float cameraHeightUnits = mainCamera.orthographicSize * 2f; // Chiều cao camera tính bằng đơn vị Unity
        float cameraWidthUnits = cameraHeightUnits * mainCamera.aspect; // Chiều rộng camera tính bằng đơn vị Unity

        // Số pixel tương ứng với 1 đơn vị Unity
        float pixelsPerUnit = GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;

        // Quy đổi viewport của camera từ đơn vị Unity sang pixel
        float cameraWidthPixels = cameraWidthUnits * pixelsPerUnit;
        float cameraHeightPixels = cameraHeightUnits * pixelsPerUnit;

        // Tính scale để khớp background với camera
        float scaleX = cameraWidthPixels / backgroundWidthPixels;
        float scaleY = cameraHeightPixels / backgroundHeightPixels;


        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}
