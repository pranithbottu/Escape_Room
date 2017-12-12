using UnityEngine;

public class PrimitiveCreator : MonoBehaviour
{
    private SteamVR_TrackedController _controller;
    private PrimitiveType _currentPrimitiveType = PrimitiveType.Sphere;

    [SerializeField]
    GameObject person;

    public bool reverse;

    public void Update()
    {
        if(_controller.triggerPressed)
        {
            runForward();
        }
    }

    private void OnEnable()
    {
        _controller = GetComponent<SteamVR_TrackedController>();
        //_controller.TriggerClicked += HandleTriggerClicked;
        //_controller.PadClicked += HandlePadClicked;
    }

    private void OnDisable()
    {
        //_controller.TriggerClicked -= HandleTriggerClicked;
        //_controller.PadClicked -= HandlePadClicked;
    }

    #region Primitive Spawning
    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        runForward();
    }

    private void runForward()
    {
        float speed = Time.deltaTime * 3.0f;
        if (reverse)
        {
            speed *= -1; 
        }
        Vector3 targetDirection = new Vector3(0f, 0f, speed);
        targetDirection = Camera.main.transform.TransformDirection(targetDirection);
        targetDirection.y = 0.0f;

        person.transform.Translate(targetDirection);
    }

    private void SpawnCurrentPrimitiveAtController()
    {
        var spawnedPrimitive = GameObject.CreatePrimitive(_currentPrimitiveType);
        spawnedPrimitive.transform.position = transform.position;
        spawnedPrimitive.transform.rotation = transform.rotation;

        spawnedPrimitive.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        if (_currentPrimitiveType == PrimitiveType.Plane)
            spawnedPrimitive.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }
    #endregion

    #region Primitive Selection
    private void HandlePadClicked(object sender, ClickedEventArgs e)
    {
        if (e.padY < 0)
            SelectPreviousPrimitive();
        else
            SelectNextPrimitive();
    }

    private void SelectNextPrimitive()
    {
        _currentPrimitiveType++;
        if (_currentPrimitiveType > PrimitiveType.Quad)
            _currentPrimitiveType = PrimitiveType.Sphere;
    }

    private void SelectPreviousPrimitive()
    {
        _currentPrimitiveType--;
        if (_currentPrimitiveType < PrimitiveType.Sphere)
            _currentPrimitiveType = PrimitiveType.Quad;
    }
    #endregion
}