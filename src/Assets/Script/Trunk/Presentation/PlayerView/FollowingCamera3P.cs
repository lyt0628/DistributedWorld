using QS.GameLib.Util;
using UnityEngine;

public class FollowingCamera3P : MonoBehaviour
{
    [SerializeField] public Transform m_Target;

    /*
     * The distance between camera and target object
     */
    public float horizontalDistance = 5f;

    /*
     * The init angle arounding axe X
     */
    public float m_InitAngle = 80f;

    /*
     * Rotate speed/sensitive of Camera
     */
    public float sensitive = 1.5f;
    /*
     * The Delay of Camera rotate
     */
    public float rotateDelay = 5f;

    private void Awake()
    {
        var ctx = TrunkGlobal.Instance.GlobalDIContext;
        ctx.BindInstance("MainCamera", GetType(), this);

    }

    #region [[Private Fields]]
    /*
     * Record variable to store init offset
     */
    private Vector3 _m_InitOffset;


    /*
     * State varialbe
     */
    private Vector3 _m_Offset;
    private float _m_RotateY = 0f;
    private float _m_RotateX = 0f;
    private Quaternion lastRotation = Quaternion.identity;

    #endregion

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        var eulerDistance = horizontalDistance / Mathf.Cos(m_InitAngle * Mathf.Deg2Rad);

        _m_InitOffset = new Vector3(0, 0, eulerDistance);

        _m_Offset = _m_InitOffset;
        _m_RotateX = m_InitAngle;
    }

    // UpdateIfNeed is called once per frame
    void LateUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Clamp rotation bound 
        _m_RotateY = MathUtil.Clamp(_m_RotateY, -180f, 180f);
        _m_RotateX = MathUtil.Clamp(_m_RotateX, -180f, 180f);

        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        if (hor != 0 || ver != 0)
        {
            _m_RotateY += hor * sensitive;
            _m_RotateX += ver * sensitive;

            //if (_m_RotateY > 80) _m_RotateY = 80;
            //if (_m_RotateY < -80) _m_RotateY = -80;
            if (_m_RotateX > 80) _m_RotateX = 80;
            if (_m_RotateX < -30) _m_RotateX = -30;

            if (_m_RotateX < 20)
            {
                float perc = (30 + _m_RotateX) / 50;
                if (perc < 0.25f) perc = 0.25f;
                _m_Offset = perc * _m_InitOffset;
                //Debug.Log("CCamera Offset:::" + _m_Offset);
            }

        }

        var quat = Quaternion.Euler(_m_RotateX, _m_RotateY, 0);
        quat = Quaternion.Lerp(lastRotation, quat, rotateDelay * Time.deltaTime);
        lastRotation = quat;


        transform.position = m_Target.position - (quat * _m_Offset);
        //transform.LookAt(m_Target); // Use AimContraint Instead
    }

}
