using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public Character Character;
    [SerializeField]
    private GameObject _linePrefab;
    [SerializeField]
    private float _dragSensivity = 1.2f;

    private LineRenderer _line;
    private Camera _mainCamera;
    private Vector3 _mouseOffset;
    private float _mouseZCoord;
    private Vector3 _firstPos;
    private Renderer _meshRenderer;
    private Transform _childOther;
    private bool _isDraggable = false;

    public abstract void PerformAction();

    private void Awake()
    {
        _mainCamera = Camera.main;
        _meshRenderer = GetComponent<Renderer>();
        _childOther = transform.GetChild(0);
    }

    private void Start()
    {
        if (!Character.isEnemy(transform)) _isDraggable = true;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _mouseZCoord;

        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDown()
    {
        if (!_isDraggable) return;

        Cursor.visible = false;
        _line = Instantiate(_linePrefab, Vector3.zero, Quaternion.identity).GetComponent<LineRenderer>();

        _mouseZCoord = _mainCamera.WorldToScreenPoint(transform.position).z;
        _mouseOffset = transform.position - GetMouseWorldPosition();

        _firstPos = transform.position;
        _line.SetPosition(0, _firstPos);

        _meshRenderer.enabled = false;
        _childOther.gameObject.SetActive(false);
    }

    private void OnMouseDrag()
    {
        if (!_isDraggable) return;

        Vector3 newPos = (GetMouseWorldPosition() + _mouseOffset) * _dragSensivity;
        transform.position = new Vector3(newPos.x, _firstPos.y, newPos.z);

        _line.SetPosition(1, transform.position);
    }

    private void OnMouseUp()
    {
        if (!_isDraggable) return;

        gameObject.AddComponent<Rigidbody>();
        Destroy(_line.gameObject);
        Cursor.visible = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
            transform.position = _firstPos;
            _meshRenderer.enabled = true;
            _childOther.gameObject.SetActive(true);
        }
    }
}
