using UnityEngine;
using FMOD.Studio;
using FMODUnity;
public class WobbleBehavior : MonoBehaviour
{
    private FMOD.Studio.EventInstance playerWobble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerWobble = FMODUnity.RuntimeManager.CreateInstance(FMODEvents.instance.wobble);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSound();
    }


    public void UpdateSound() 
    {
        Vector3 curVelocity = this.GetComponent<Rigidbody>().linearVelocity;
        if(curVelocity.x > 0 || curVelocity.z > 0)
        {
            PLAYBACK_STATE playbackState;
            playerWobble.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                // Debug.Log("wobble");
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerWobble, this.transform);
                playerWobble.start();
            }
        }
        else 
        {
            playerWobble.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
