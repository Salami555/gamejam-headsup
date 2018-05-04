using System;
using UnityEngine;

public class ShockWaveRenderer : MonoBehaviour
{
    public Material Mat;
    public float speed;

    private Vector4[] hitPoints = new Vector4[10];
    private int hitPointHead = 0;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, Mat);
    }

    private void Start()
    {
        for (var i = 0; i < hitPoints.Length; i++)
        {
            hitPoints[i] = new Vector4(0, 0, 999999, 0);
        }

        Mat.SetVectorArray("_HitPoints", hitPoints);
    }

    private void Update()
    {
        for (var i = 0; i < hitPoints.Length; i++)
        {
            hitPoints[i].z += Time.deltaTime * speed;
        }

        Mat.SetVectorArray("_HitPoints", hitPoints);

        if (Input.GetMouseButtonDown(0))
        {
            float radius = 4.0f;
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MakeWave(pos);
        }
    }

    public void MakeWave(Vector2 worldPosition, float strength = 1.0f)
    {
        Vector2 viewportSpace = Camera.main.WorldToViewportPoint(worldPosition);
        hitPoints[hitPointHead].x = viewportSpace.x;
        hitPoints[hitPointHead].y = viewportSpace.y;
        hitPoints[hitPointHead].z = 0;
        hitPoints[hitPointHead].w = strength;

        hitPointHead++;
        if (hitPointHead >= hitPoints.Length) hitPointHead = 0;
    }
}