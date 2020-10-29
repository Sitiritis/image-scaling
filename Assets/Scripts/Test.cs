using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private ParentRectTransformBounder Image;

    // Start is called before the first frame update
    void Start()
    {
        // Image.Position = new Vector2(-7000, -7000);
        // Image.Position = new Vector2(7000, 7000);
        Debug.Log(Input.gyro.attitude);
    }
}
