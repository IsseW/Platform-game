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
        rooms = new RoomStack();
    }

    [System.Serializable]
    private struct Overlays
    {
        [SerializeField] private RectTransform left;
        [SerializeField] private RectTransform right;
        [SerializeField] private RectTransform top;
        [SerializeField] private RectTransform bottom;

        public void SetLeft(float size, float scale)
        {
            left.offsetMax = new Vector2(size * scale, left.offsetMax.y);
        }

        public void SetRight(float size, float scale)
        {
            right.offsetMin = new Vector2(size * scale, right.offsetMin.y);
        }

        public void SetTop(float size, float scale)
        {
            top.offsetMin = new Vector2(top.offsetMin.x, size * scale);
        }

        public void SetBottom(float size, float scale)
        {
            bottom.offsetMax = new Vector2(bottom.offsetMax.x, size * scale);
        }

        public void SetHorizontal(float size, float scale)
        {
            SetTop(size, scale);
            SetBottom(size, scale);
        }

        public void SetVertical(float size, float scale)
        {
            SetLeft(size, scale);
            SetRight(size, scale);
        }

        public void SetAll(float size, float scale)
        {
            SetHorizontal(size, scale);
            SetVertical(size, scale);
        }
    }

    [SerializeField]
    private Overlays overlays;

    private void Update()
    {
        if (InputSystem.Attack())
        {
            CameraShake(5, 1);
        }

        if (transform.hasChanged && updateOverlays)
        {
            transform.hasChanged = false;
            UpdateOverlays();
        }
    }
    private bool updateOverlays = true;

    private void UpdateOverlays()
    {
        if (currentRoom != null)
        {
            Rect rect = GetCurrentCameraRect();
            float scale = camera.WorldToScreenPoint(rect.min + Vector2.right).x;
            overlays.SetLeft(currentRoom.rect.xMin - rect.xMin, scale);
            overlays.SetRight(currentRoom.rect.xMax - rect.xMax, scale);
            overlays.SetTop(currentRoom.rect.yMax - rect.yMax, scale);
            overlays.SetBottom(currentRoom.rect.yMin - rect.yMin, scale);
        }
        else
        {
            overlays.SetAll(0, 1);
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

        float aspect = rect.width / rect.height;

        if (aspect > aspectRatio)
        {
            camera.orthographicSize = (rect.width / aspectRatio) / 2;
        }
        else
        {
            camera.orthographicSize = rect.height / 2;
        }

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
        cameraRect = GetCurrentCameraRect();
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

    Room currentRoom;

    RoomStack rooms;

    public void PushRoom(Room room)
    {
        if (room == currentRoom) return;

        rooms.PopRoom(room);

        if (currentRoom != null)
        {
            rooms.Push(currentRoom);
        }

        RefreshRoom(room);
    }

    public void PopRoom(Room room)
    {
        if (room == currentRoom)
        {
            RefreshRoom(rooms.Pop());
        }
        else
        {
            if (rooms.PopRoom(room))
                RefreshRoom(room);
            else
                RefreshRoom(null);
        }
    }

    private void RefreshRoom(Room newRoom)
    {
        if (currentRoom != null && newRoom != null)
        {
            StartCoroutine(FadeBetweenRooms(currentRoom, newRoom));
        }
        else if (newRoom != null)
        {
            currentRoom = newRoom;
            SetCameraRect(newRoom.rect);
        }
        else
        {
            currentRoom = null;
        }
    }

    private IEnumerator FadeBetweenRooms(Room from, Room to)
    {
        currentRoom = to;
        SetCameraRect(to.rect);
        yield return null;
    }

    class RoomStack
    {
        public RoomStack()
        {
            rooms = new List<Room>();
        }
        private List<Room> rooms;
        public Room Pop()
        {
            if (rooms.Count == 0) return null;
            Room t = rooms[rooms.Count - 1];
            rooms.RemoveAt(rooms.Count - 1);
            return t;
        }
        
        public void Push(Room room)
        {
            rooms.Add(room);
        }

        public bool PopRoom(Room room)
        {
            return rooms.Remove(room);
        }
    }
}
