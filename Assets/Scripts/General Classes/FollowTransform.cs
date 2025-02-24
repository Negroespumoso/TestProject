using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public bool isActive;

    [SerializeField] private bool followPosition;
    [SerializeField] private bool followRotation;
    [SerializeField] private bool followScale;

    public Transform changeTarget;
    [SerializeField] private Transform followTransform;

    private void Update()
    {
        if(isActive)
        {
            if (followPosition) changeTarget.position = followTransform.position;
            if (followRotation) changeTarget.rotation = followTransform.rotation;
            if (followScale) changeTarget.localScale = followTransform.localScale;

        }
    }
}
