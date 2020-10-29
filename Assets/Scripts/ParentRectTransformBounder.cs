using UnityEngine;

/// <summary>
/// Bounds <c>RectTransform</c> of the current game object to
/// <c>RectTransform</c> of the given game object so that the current game
/// object does not leave "blank areas" on the bounding game object as the
/// current is being rescaled.
/// </summary>
public class ParentRectTransformBounder : MonoBehaviour
{
    [SerializeField] private RectTransform boundingRectTransform;
    private RectTransform currentRectTransform;

    void Awake()
    {
        currentRectTransform = GetComponent<RectTransform>();

        if (currentRectTransform == null)
        {
            Debug.LogError("The ParentRectTransformBounder must be placed on a game object with RectTransform");
        }

        CenterImage();
    }

    public float MinHorizontalBoundary =>
        MaxHorizontalBoundary - (
            (currentRectTransform.rect.width * currentRectTransform.lossyScale.x) -
            (boundingRectTransform.rect.width * boundingRectTransform.lossyScale.x)
        );
    public float MaxHorizontalBoundary =>
        boundingRectTransform.position.x -
        (boundingRectTransform.rect.width * boundingRectTransform.lossyScale.x) / 2;
    public float MinVerticalBoundary =>
        MaxVerticalBoundary - (
            (currentRectTransform.rect.height * currentRectTransform.lossyScale.y) -
            (boundingRectTransform.rect.height * boundingRectTransform.lossyScale.y)
        );
    public float MaxVerticalBoundary =>
        boundingRectTransform.position.y -
        (boundingRectTransform.rect.height * boundingRectTransform.lossyScale.y) / 2;


    public Rect BoundaryRect => new Rect(
        MinHorizontalBoundary,
        MinVerticalBoundary,
        currentRectTransform.rect.width * currentRectTransform.lossyScale.x,
        currentRectTransform.rect.height * currentRectTransform.lossyScale.y
    );

    public Vector2 Position
    {
        get => currentRectTransform.position;
        set
        {
            var currentMinHorizontalBoundary = MinHorizontalBoundary;
            var currentMaxHorizontalBoundary = MaxHorizontalBoundary;
            var currentMinVerticalBoundary = MinVerticalBoundary;
            var currentMaxVerticalBoundary = MaxVerticalBoundary;

            var newX = value.x <= currentMinHorizontalBoundary ? currentMinHorizontalBoundary :
                value.x >= currentMaxHorizontalBoundary ? currentMaxHorizontalBoundary :
                value.x;
            var newY = value.y <= currentMinVerticalBoundary ? currentMinVerticalBoundary :
                value.y >= currentMaxVerticalBoundary ? currentMaxVerticalBoundary :
                value.y;

            currentRectTransform.position = new Vector3(newX, newY, currentRectTransform.position.z);
        }
    }

    public float X
    {
        get => currentRectTransform.position.x;
        set => Position = new Vector2(value, Position.y);
    }

    public float Y
    {
        get => currentRectTransform.position.y;
        set => Position = new Vector2(Position.x, value);
    }

    public void CenterImage()
    {
        var boundingRect = boundingRectTransform.rect;
        var boundingScale = boundingRectTransform.lossyScale;

        Position = BoundaryRect.center - new Vector2(
            (boundingRect.width * boundingScale.x) / 2,
            (boundingRect.height * boundingScale.y) / 2
        );
    }

    public void SetScale(float scale)
    {
        currentRectTransform.localScale = new Vector3(scale, scale, scale);
        CenterImage();
    }
}
