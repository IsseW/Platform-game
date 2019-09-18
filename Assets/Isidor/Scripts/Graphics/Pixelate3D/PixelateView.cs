using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelateView : MonoBehaviour
{

    private RenderTexture texture;
    public Camera cam;
    private Camera mainCam;
    public RawImage image;

    private void Start()
    {
        mainCam = CameraController.Instance.GetCamera();
        texture = new RenderTexture(mainCam.pixelWidth / 2, mainCam.pixelHeight / 2, 1);
        texture.antiAliasing = 1;
        texture.filterMode = FilterMode.Point;

        texture.Create();
        cam.targetTexture = texture;
        image.texture = texture;
        image.gameObject.SetActive(true);
    }
}
