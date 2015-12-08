using UnityEngine;
using System.Collections;

public class WaypointType : MonoBehaviour {

    public enum Types
    {
        walkToNextWaypointOnPathEnd,
        stayOnPathEnd,
        elevatorEntrance,
        elevator,
        elevatorExit,
        walkDirectPath
    };

    public enum SoundType
    {
        none,
        playOneSound
    }

    public Types type;
    public SoundType sType;
    public int waypointNumber;
    public float waitTime = 0;
    public AudioClip soundClip;

}
