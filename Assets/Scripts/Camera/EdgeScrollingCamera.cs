using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class EdgeScrollingCamera : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float edgeSize = 20f;

    private CinemachineVirtualCamera vcam;
    private Vector3 targetPosition;

    private void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        targetPosition = transform.position;
    }
    
    private void Update()
    {
        Vector3 moveDir = Vector3.zero;
        Vector2 mousePos = Input.mousePosition;

        // 检测边缘
        if (mousePos.x <= edgeSize) moveDir.x = 1;
        if (mousePos.x >= Screen.width - edgeSize) moveDir.x = -1;
        if (mousePos.y <= edgeSize) moveDir.z = 1;
        if (mousePos.y >= Screen.height - edgeSize) moveDir.z = -1;

        // 移动相机
        targetPosition += moveDir.normalized * scrollSpeed * Time.deltaTime;
        transform.position = targetPosition;
    }
}
