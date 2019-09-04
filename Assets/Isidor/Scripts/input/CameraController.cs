using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private new Camera camera;

    public static CameraController Instance { get; private set; }

    public AnimationCurve cameraShakeCurve;

    private void Start()
    {
        Instance = this;
        camera = GetComponent<Camera>();
        cameraRect = GetCurrentCameraRect();
    }

    private void Update()
    {
        if (InputSystem.Attack())
        {
            CameraShake(5, 1);
        }
    }

    public void CameraShake(float intensity, float time)
    {
        intensity /= 100;
        if (intensity > currentIntensity)
        {
            currentIntensity += intensity;
            if (currentShake != null) StopCoroutine(currentShake);
            currentShake = StartCoroutine(Shake(intensity, time));
        }
    }

    Coroutine currentShake;
    float currentIntensity;

    private IEnumerator Shake(float intensity, float time)
    {
        float startTime = time;

        while (time > 0)
        {
            currentIntensity = intensity * cameraShakeCurve.Evaluate(1 - time / startTime);

            float maxSize = cameraRect.height;
            float minSize = maxSize / (1 + currentIntensity);

            float newSize = Random.Range(minSize, maxSize);
            
            float maxY = (maxSize - newSize);
            float maxX = maxY * aspectRatio;

            Vector2 newPos = cameraRect.min + new Vector2(Random.Range(0, maxX), Random.Range(0, maxY));

            SetTempCameraRect(new Rect(newPos, new Vector2(newSize * aspectRatio, newSize)));

            yield return null;
            time -= Time.deltaTime;
        }
        currentIntensity = 0;
        FixCamera();
    }


    public float aspectRatio => (float)Screen.width / Screen.height;

    public Rect GetCurrentCameraRect()
    {
        float top = transform.position.y - camera.orthographicSize;
        float halfWidth = camera.orthographicSize * aspectRatio;
        float left = transform.position.x - halfWidth;
        return new Rect(left, top, halfWidth * 2, camera.orthographicSize * 2);
    }

    public void SetTempCameraRect(Rect rect)
    {
        SetPosition(rect.center);
        camera.orthographicSize = rect.height / 2;
    }

    public void FixCamera()
    {
        SetTempCameraRect(cameraRect);
    }

    private Rect cameraRect;

    public Vector2 position
    {
        get
        {
            return cameraRect.position;
        }

        set
        {
            cameraRect.center = value;
        }
    }

    public float size
    {
        get
        {
            return cameraRect.height / 2;
        }

        set
        {
            Vector2 center = cameraRect.center;
            cameraRect.size = new Vector2(value * 2, value * 2 * aspectRatio);
            cameraRect.center = center;
            FixCamera();
        }
    }

    public Rect GetCameraRect()
    {
        return cameraRect;
    }

    public void SetCameraRect(Rect rect)
    {
        cameraRect = rect;
        FixCamera();
    }

    private void SetPosition(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, -10);
    }

    private void OnDrawGizmos()
    {
        if (camera != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(cameraRect.center.x, cameraRect.center.y, transform.position.z), new Vector3(cameraRect.width, cameraRect.height, 0));
        }
    }


    public void SetRoom(BoxCollider2D collider)
    {
        Rect rect = new Rect(collider.bounds.min, collider.bounds.max - collider.bounds.min);
        float aspect = (float)rect.width / rect.height;
        SetCameraRect();
    }

    
}
