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

    public Types type;
    public int waypointNumber;

}
