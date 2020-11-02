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
    }

    private void Update()
    {
        var currentPosition = Input.gyro.rotationRate;

        xText.text = $"X: {currentPosition.x}";
        yText.text = $"Y: {currentPosition.y}";
        zText.text = $"Z: {currentPosition.z}";

        Image.Position += new Vector2(currentPosition.y, -currentPosition.x) * 50;
    }
}
