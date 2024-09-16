using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCameraCtlThird : MonoBehaviour
{
    [SerializeField]public Transform m_Target;
    public float m_RotateSpeed = 1.5f;
    public float m_Distance = 20f;
    public float m_InitAngle = 45f;

    private Vector3 _m_InitOffset;
    private Vector3 _m_Offset;
    private float _m_RotateY = 0f;
    private float _m_RotateX = 0f;

    // Start is called before the first frame update
    void Start()
    {
       

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //_m_InitOffset = m_Target.position - transform.position ;
        _m_InitOffset = new Vector3(0, 0, m_Distance);

        _m_Offset = _m_InitOffset;
        _m_RotateX = m_InitAngle;
    }


    // Update is called once per frame
    void LateUpdate()
    {

        m_Rotate();
    }

    private void m_Rotate()
    {
        if (_m_RotateY > 180) _m_RotateY -= 180;
        if (_m_RotateY < -180) _m_RotateY += 180;
        if (_m_RotateX > 180) _m_RotateX -= 180;
        if (_m_RotateX < -180) _m_RotateX += 180;


        float hor = Input.GetAxis("Mouse X");
        float ver = -Input.GetAxis("Mouse Y");
        if(hor != 0 || ver != 0)
        {
            _m_RotateY += hor * m_RotateSpeed;
            _m_RotateX += ver * m_RotateSpeed;

            if (_m_RotateY > 80) _m_RotateY = 80;
            if (_m_RotateY < -80) _m_RotateY = -80;
            if (_m_RotateX > 80) _m_RotateX = 80;
            if (_m_RotateX < -30) _m_RotateX = -30;

            if(_m_RotateX < -0)
            {
                float perc = (30f - Mathf.Abs(_m_RotateX)) / 30f;
                if (perc < 0.25f) perc = 0.25f;
                _m_Offset = perc * _m_InitOffset;
               // Debug.Log("CCamera Offset:::" + _m_Offset);
            }

        }
        Quaternion quat = Quaternion.Euler(_m_RotateX, _m_RotateY, 0);
        transform.position = m_Target.position - (quat * _m_Offset);
        //transform.LookAt(m_Target); // Use AimContraint Instead
    }

}
