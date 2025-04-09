using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;
    private float maxBoundX;
    private float minBoundX;
    private float maxBoundY;
    private float minBoundY;

    protected override void Start()
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        minBoundX = background.sizeDelta.x / 2;
        maxBoundX = rect.sizeDelta.x - minBoundX;
        minBoundY = background.sizeDelta.y / 2;
        maxBoundY = rect.sizeDelta.y - minBoundY;
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition = new(
                Mathf.Clamp(background.anchoredPosition.x + difference.x, minBoundX, maxBoundX),
                Mathf.Clamp(background.anchoredPosition.y + difference.y, minBoundY, maxBoundY));
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}