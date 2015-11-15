using UnityEngine;
using System.Collections;
using Pathfinding;

public class ChangeErosion : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        // This holds all graph data
        AstarData data = AstarPath.active.astarData;
        // This creates a Grid Graph
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        // Setup a grid graph with some values
        gg.erodeIterations = 0;

        // Updates internal size from the above values
        gg.UpdateSizeFromWidthDepth();
        // Scans all graphs, do not call gg.Scan(), that is an internal method
        AstarPath.active.Scan();

        Debug.Log(other.name);
        //AstarPath.active.
    }

}
