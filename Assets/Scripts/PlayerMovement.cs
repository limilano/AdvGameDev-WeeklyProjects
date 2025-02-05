// https://www.youtube.com/watch?v=b1uoLBp2I1w
using UnityEngine;
using FMOD.Studio;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private LayerMask BounceMask;
    [SerializeField] private LayerMask WobbleMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;

    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;
    [SerializeField] private float BounceForce;
    private bool isBouncing = false;

    // audio
    // private EventInstance playerFootsteps;
    

    private void Start()
    {
        // playerFootsteps = AudioManager.instance.CreateInstance(FMODEvents.instance.playerWalking);
    }

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        CheckBounce();
        MovePlayerCamera();
        // UpdateSound();
    }

    private void MovePlayer() 
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.linearVelocity = new Vector3(MoveVector.x, PlayerBody.linearVelocity.y, MoveVector.z);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.jump, this.transform.position);
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.bounce, this.transform.position);
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

    private void UpdateSound()
    {
        // start footsteps event if the player has an x velocity and is on the ground
        // if (PlayerBody.linearVelocity != Vector3.zero && Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
        // {
        //     // get the playback state
        //     PLAYBACK_STATE playbackState;
        //     playerFootsteps.getPlaybackState(out playbackState);
        //     if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        //     {
        //         playerFootsteps.start();
        //     }
        // }
        // // otherwise, stop the footsteps event
        // else 
        // {
        //     playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        // }

        
    }


}