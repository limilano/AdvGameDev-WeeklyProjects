using UnityEngine;
using FMOD.Studio;
public class WobbleBehavior : MonoBehaviour
{
    private EventInstance playerWobble;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerWobble = AudioManager.instance.CreateInstance(FMODEvents.instance.wobble);
        playerWobble.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(playerWobble,  GetComponent<Transform>(), GetComponent<Rigidbody>());
        
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
                Debug.Log("wobble");
                playerWobble.start();
            }
        }
        else 
        {
            playerWobble.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
