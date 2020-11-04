using System;
using UnityEngine;

/// <summary>
/// Bounds <c>RectTransform</c> of the current game object to
/// <c>RectTransform</c> of the given game object so that the current game
/// object does not leave "blank areas" on the bounding game object as the
/// current is being rescaled.
/// </summary>
public class ParentRectTransformBounder : MonoBehaviour
{
    [SerializeField] public RectTransform boundingRectTransform; // TODO: change to private
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

    public float CurrentWidth =>
        currentRectTransform.rect.width * currentRectTransform.lossyScale.x;
    public float CurrentHeight =>
        currentRectTransform.rect.height * currentRectTransform.lossyScale.y;

    public float BoundingHeight =>
        boundingRectTransform.rect.height * boundingRectTransform.lossyScale.y;
    public float BoundingWidth =>
        boundingRectTransform.rect.width * boundingRectTransform.lossyScale.x;

    public float MinHorizontalBoundary
    {
        get
        {
            var boundingWidth = BoundingWidth;
            var currentWidth = CurrentWidth;

            return MaxHorizontalBoundary - Math.Max(0, currentWidth - boundingWidth);
        }
    }

    public float MaxHorizontalBoundary
    {
        get
        {
            var boundingWidth = BoundingWidth;
            var currentWidth = CurrentWidth;

            return boundingWidth >= currentWidth
                ? (currentRectTransform.position.x - (currentWidth / 2))
                : (boundingRectTransform.position.x - (boundingWidth / 2));
        }
    }

    public float MinVerticalBoundary
    {
        get
        {
            var boundingHeight = BoundingHeight;
            var currentHeight = CurrentHeight;

            return MaxVerticalBoundary - Math.Max(0, currentHeight - boundingHeight);
        }
    }

    public float MaxVerticalBoundary
    {
        get
        {
            var boundingHeight = BoundingHeight;
            var currentHeight = CurrentHeight;

            return boundingHeight >= currentHeight
                ? (currentRectTransform.position.y - (currentHeight / 2))
                : (boundingRectTransform.position.y - (boundingHeight / 2));
        }
    }

    // public Rect BoundaryRect => new Rect(
    //     MinHorizontalBoundary,
    //     MinVerticalBoundary,
    //     CurrentWidth,
    //     CurrentHeight
    // );

    public Vector2 Position
    {
        get => currentRectTransform.position;
        set
        {
            var horizontalOffset = CurrentWidth / 2;
            var verticalOffset = CurrentHeight / 2;

            var currentMinHorizontalBoundary = MinHorizontalBoundary + horizontalOffset;
            var currentMaxHorizontalBoundary = MaxHorizontalBoundary + horizontalOffset;
            var currentMinVerticalBoundary = MinVerticalBoundary + verticalOffset;
            var currentMaxVerticalBoundary = MaxVerticalBoundary + verticalOffset;

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

    public void CenterImage() =>
        Position = boundingRectTransform.position;

    public void SetScale(float scale)
    {
        currentRectTransform.localScale = new Vector3(scale, scale, scale);
        CenterImage();
    }
}
