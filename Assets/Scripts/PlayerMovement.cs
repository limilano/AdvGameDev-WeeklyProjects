// https://www.youtube.com/watch?v=b1uoLBp2I1w
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private LayerMask BounceMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;

    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;
    [SerializeField] private float BounceForce;
    private bool isBouncing = false;

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        CheckBounce();
        MovePlayerCamera();
    }

    private void MovePlayer() 
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask) || Physics.CheckSphere(FeetTransform.position, 0.1f, BounceMask))
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }

        
    }

    private void CheckBounce()
    {
        // if(Physics.CheckSphere(FeetTransform.position, 0.1f, BounceMask))
        if(!isBouncing && Physics.CheckSphere(FeetTransform.position, 0.1f, BounceMask))
        {
            isBouncing = true;
            Invoke("NotBouncing", .2f);
            PlayerBody.linearVelocity = Vector3.zero;
            PlayerBody.AddForce(Vector3.up * BounceForce, ForceMode.Impulse);
        }
    }

    void NotBouncing() 
    {
        isBouncing = false;
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}