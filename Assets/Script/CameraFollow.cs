using UnityEngine;

public class CameraFllow:MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody2D targetRb;

    [Header("Dead Zone")]
    [SerializeField] private float deadZoneWidth=1.2f;
    [SerializeField] private float deadZoneHeight = 1.2f;

    [Header("Look Ahead")]
    [SerializeField] private float lookAheadDistance = 3.0f;
    [SerializeField] private float lookAheadSmoothing = 3.0f;

    [Header("Smooth Damp")]
    [SerializeField] private float smoothTime = 0.2f;

    [Header("Clamp")]
    [SerializeField] private float maxX = 20.0f;
    [SerializeField] private float minX = 0.0f;
    [SerializeField] private float maxY = 5.0f;
    [SerializeField] private float minY = 0.0f;

    private float currentLookAheadX = 0.0f;
    private Vector3 velocity;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        Vector3 currentPos = transform.position;
        Vector3 targetPos = target.position;

        float desiredX=currentPos.x;
        float desiredY=currentPos.y;

        float targetLookAhead = 0.0f;

        if (Mathf.Abs(targetRb.linearVelocity.x) > 0.1f) 
        {
            targetLookAhead = Mathf.Sign(targetRb.linearVelocity.x) * lookAheadDistance;
        }

        currentLookAheadX = Mathf.Lerp(
            currentLookAheadX, 
            targetLookAhead, 
            lookAheadSmoothing * Time.deltaTime);



        float deltaX = (targetPos.x + currentLookAheadX) - currentPos.x;
        float deltaY = targetPos.y - currentPos.y;

        if (Mathf.Abs(deltaX) > deadZoneWidth)
        {
            desiredX = deltaX > 0.0f ?
                (targetPos.x + currentLookAheadX) - deadZoneWidth :
                (targetPos.x + currentLookAheadX) + deadZoneWidth;
            
        }
        if(Mathf.Abs(deltaY) > deadZoneHeight)
        {
            desiredY = deltaY > 0.0f ? 
                targetPos.y - deadZoneHeight : 
                targetPos.y + deadZoneHeight;
        }

        Vector3 desiredPosition = new Vector3(desiredX, desiredY, -15.0f);

        Vector3 smoothedPos  = Vector3.SmoothDamp(
        currentPos,
        desiredPosition,
        ref velocity,
        smoothTime * Time.deltaTime);

        smoothedPos.x = Mathf.Clamp(smoothedPos.x, minX, maxX);
        smoothedPos.y = Mathf.Clamp(smoothedPos.y, minY, maxY);

        transform.position = smoothedPos;

    }
}
