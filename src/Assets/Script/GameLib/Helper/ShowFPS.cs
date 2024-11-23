
using UnityEngine;
public class ShowFPS : MonoBehaviour
{
    public static ShowFPS Instance { private set; get; }


    void OnEnable()
    {
        Instance = this;
    }


    void OnDisable()
    {
        Instance = null;
    }

    public float UpdateInterval = 0.5F;
    private float _lastInterval;
    private int _frames = 0;
    private float _fps;


    private readonly GUIStyle _style = new();



    void Start()
    {

        Application.targetFrameRate = 300;
        _lastInterval = Time.realtimeSinceStartup;
        _frames = 0;


        _style.fontSize = 10;
        _style.normal.textColor = new Color(0, 255, 0, 255);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 200), "FPS:" + _fps.ToString("f2"), _style);
    }

    void Update()
    {
        ++_frames;
        if (Time.realtimeSinceStartup > _lastInterval + UpdateInterval)
        {
            _fps = _frames / (Time.realtimeSinceStartup - _lastInterval);
            _frames = 0;
            _lastInterval = Time.realtimeSinceStartup;
        }
    }
}
