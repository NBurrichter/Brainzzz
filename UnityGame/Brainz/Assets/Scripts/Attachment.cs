using UnityEngine;
using System.Collections;

public class Attachment : MonoBehaviour
{
    public Transform PseudoParent { get; set; }
    private Vector3 lastFramePosition;


    private void Start()
    {
        lastFramePosition = PseudoParent.position;
    }

    // move object with the parent
    private void LateUpdate()
    {
        if (!PseudoParent)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(PseudoParent.position - lastFramePosition);

        //Do this last
        lastFramePosition = PseudoParent.position;
    }

}
