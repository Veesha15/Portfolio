using UnityEngine;

[ExecuteInEditMode]
public class EditorSetTriggerPos : MonoBehaviour // attached to prefab
{
    private enum TriggerPos
    {
        Top,
        Bottom,
        Left,
        Rigt
    }

    [SerializeField] CircleCollider2D triggerCollider;
    [SerializeField] Transform triggerIndicator; // red X image
    [SerializeField] TriggerPos triggerPos; // choose option in Inspector


    private void Update()
    { 
        if (!Application.isPlaying)
        {
            switch (triggerPos)
            {
                case TriggerPos.Top:
                    triggerCollider.offset = new Vector2(0, 1);
                    triggerIndicator.localPosition = new Vector3(0, 1, 0);
                    break;

                case TriggerPos.Bottom:
                    triggerCollider.offset = new Vector2(0, -1);
                    triggerIndicator.localPosition = new Vector3(0, -1, 0);
                    break;

                case TriggerPos.Rigt:
                    triggerCollider.offset = new Vector2(1, 0);
                    triggerIndicator.localPosition = new Vector3(1, 0, 0);
                    break;

                case TriggerPos.Left:
                    triggerCollider.offset = new Vector2(-1, 0);
                    triggerIndicator.localPosition = new Vector3(-1, 0, 0);
                    break;
            }
        }
    }


}
