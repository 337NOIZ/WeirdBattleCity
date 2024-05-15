using UnityEngine;

public sealed class CameraResolution : MonoBehaviour
{
    private new Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();

        Rect camera_rect = camera.rect;

        float camera_rect_height = ((float)Screen.width / Screen.height) / ((float)16 / 9);

        float camera_rect_width = 1f / camera_rect_height;

        if (camera_rect_height < 1)
        {
            camera_rect.height = camera_rect_height;

            camera_rect.y = (1f - camera_rect_height) / 2f;
        }

        else
        {
            camera_rect.width = camera_rect_width;

            camera_rect.x = (1f - camera_rect_width) / 2f;
        }

        camera.rect = camera_rect;
    }
}