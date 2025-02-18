using UnityEngine;
using UnityEngine.UIElements;

public class PostionTestRuntime : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<UIDocument>();
    }
}