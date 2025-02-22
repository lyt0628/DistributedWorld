
using UnityEngine;

public class MyUVOffsetController : MonoBehaviour
{

    public int m_MaterialIndex = 0;
    public float m_XOffset;
    public float m_YOffset;

    private Vector2 _UVOffset;
    private Renderer _renderer;
    void Start()
    {
        _UVOffset = Vector2.zero;
        _renderer = GetComponent<Renderer>();
    }

    // UpdateIfNeed is called once per frame
    void Update()
    {
        _UVOffset.x += m_XOffset * Time.fixedDeltaTime;
        _UVOffset.y += m_YOffset * Time.fixedDeltaTime;
        _renderer.materials[m_MaterialIndex].mainTextureOffset = _UVOffset;
    }
}
