using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    #region Private Fields
    
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _zoomSpeed;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineTransposer _cinemachineTransposer;
    private Vector3 _targetFollowOffset;

    #endregion

    #region Unity: Start | Update
    
    private void Start()
    {
        _cinemachineTransposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        _targetFollowOffset = _cinemachineTransposer.m_FollowOffset;
        _moveSpeed = 10f;
        _rotationSpeed = 100f;
        _zoomSpeed = 5f;
    }

    private void Update()
    {
        CameraMovement();
        CameraRotation();
        CameraZoom();
    }
    
    #endregion

    #region CameraMovement
    
    private void CameraMovement()
    {
        var inputMoveDir = new Vector3(0,0,0);

        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = +1f;
        }
        
        var moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * (_moveSpeed * Time.deltaTime);
    }
    
    #endregion

    #region CameraRotation
    
    private void CameraRotation()
    {
        var inputRotateDir = new Vector3(0,0,0);
        
        if (Input.GetKey(KeyCode.Q))
        {
            inputRotateDir.y = -1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            inputRotateDir.y = +1f;
        }
        
        transform.eulerAngles += inputRotateDir * (_rotationSpeed * Time.deltaTime);
    }
    
    #endregion
    
    #region CameraZoom

    private void CameraZoom()
    {
        CinemachineTransposer cinemachineTransposer =_virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        var zoomAmount = 3f;

        if (Input.mouseScrollDelta.y > 0)
        {
            _targetFollowOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            _targetFollowOffset.y += zoomAmount;
        }
        
        _targetFollowOffset.y = Mathf.Clamp(_targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, _targetFollowOffset, Time.deltaTime * _zoomSpeed);
    }
    
    #endregion
}