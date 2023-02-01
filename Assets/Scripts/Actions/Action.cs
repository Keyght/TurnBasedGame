using UnityEngine;

public enum Flag
{
    ATTACKING,
    HEALING,
    DEFENDING
}

public abstract class Action : MonoBehaviour
{
    public Flag Flag;
    [SerializeField]
    private GameObject _linePrefab;
    [SerializeField]
    private float _dragSensivity = 1.2f;

    protected Character _target;
    private LineRenderer _line;
    private Camera _mainCamera;
    private Vector3 _mouseOffset;
    private float _mouseZCoord;
    private Vector3 _firstPos;
    private Renderer _meshRenderer;
    private Transform _childOther;
    protected bool _isDraggable = false;
    protected bool _isSelfTargeted = false;
    protected bool _isEnemyTargeted = false;

    public abstract void PerformAction();

    private void Awake()
    {
        _mainCamera = Camera.main;
        _meshRenderer = GetComponent<Renderer>();
        _childOther = transform.GetChild(0);
    }

    protected void Start()
    {
        if (!Character.isEnemy(transform.position)) _isDraggable = true;
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

    public void SetTarget(Character target, out bool performable)
    {
        if (_isSelfTargeted)
        {
            if (target.gameObject.Equals(transform.parent.gameObject))
            {
                _target = target;
                performable = true;
            }
            else
            {
                ResetAction();
                performable = false;
            }
        }
        else if (!_isDraggable)
        {
            _target = target;
            performable = true;
        }
        else if (_isEnemyTargeted)
        {
            if (Character.isEnemy(target.transform.position))
            {
                _target = target;
                performable = true;
            }
            else
            {
                ResetAction();
                performable = false;
            }
        }
        else
        {
            performable = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            ResetAction();
        }
    }

    private void ResetAction()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        transform.position = _firstPos;
        transform.rotation = Quaternion.identity;
        _meshRenderer.enabled = true;
        _childOther.gameObject.SetActive(true);
    }
}
