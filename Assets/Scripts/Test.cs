using System;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private ParentRectTransformBounder Image;
    [SerializeField] private Text xText;
    [SerializeField] private Text yText;
    [SerializeField] private Text zText;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        Image.SetScale(2);
    }

    private void Update()
    {
        var currentPosition = Input.gyro.rotationRate;

        xText.text = $"X: {currentPosition.x}";
        yText.text = $"Y: {currentPosition.y}";
        zText.text = $"Z: {currentPosition.z}";

        Image.Position += new Vector2(currentPosition.y, -currentPosition.x) * 50;

        var minBoundary = new Vector3(Image.MinHorizontalBoundary, Image.MinVerticalBoundary);
        var maxBoundary = new Vector3(Image.MaxHorizontalBoundary, Image.MaxHorizontalBoundary);
        Debug.DrawLine(
            minBoundary,
            maxBoundary,
            Color.green
        );
        var posP = PerpendicularLine(minBoundary, maxBoundary);
        Debug.DrawLine(
            posP.Item1,
            posP.Item2,
            Color.green
        );

        var boundingMin = Image.boundingRectTransform.position - new Vector3(
            Image.BoundingWidth / 2,
            Image.BoundingHeight / 2
        );
        var boundingMax = Image.boundingRectTransform.position + new Vector3(
            Image.BoundingWidth / 2,
            Image.BoundingHeight / 2
        );
        Debug.DrawLine(
            boundingMin,
            boundingMax,
            Color.yellow
        );
        var bp = PerpendicularLine(boundingMin, boundingMax);
        Debug.DrawLine(
            bp.Item1,
            bp.Item2,
            Color.yellow
        );
    }

    public static (Vector2, Vector2) PerpendicularLine(Vector2 min, Vector2 max) =>
        (new Vector2(max.x, min.y), new Vector2(min.x, max.y));
}
