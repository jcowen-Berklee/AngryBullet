//from Github/Ranux

using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _isDragged = false;
    private bool _isFired = false;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private float _bulletRevertSpeed = 1f;
    private Vector3 _originalPosition;
    private float _multiplier = 10f;
    private const float ImpulseIncreaseValue = 10f;
    private const float LeftBorder = -18f;
    private const float RightBorder = 28f;

    private const float LineWidth = 1.0f;
    private Vector2 _originalMousePosition;

    void Start()
    {
        _startPosition = transform.position;
        _startRotation = transform.rotation;
    }

    void Update()
    {
        if (_isDragged)
        {
            // follow the mouse cursor
            Vector3 hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(hitPoint.x, hitPoint.y, 0);
            float impulse = (_originalPosition.x - transform.position.x) + ImpulseIncreaseValue;

            GetComponent<Rigidbody>().useGravity = true;

            // bullet direction
   
            Vector3 direction = new Vector3(_originalPosition.x - transform.position.x, _originalPosition.y - transform.position.y, 0);
			GetComponent<Rigidbody>().velocity = direction*impulse;
			Debug.DrawLine(_originalPosition, transform.position, Color.red);
            
        }

        if (_isDragged && Input.GetMouseButtonUp(0) || Vector2.Distance(_originalPosition, transform.position) > 4f)
        {
            _isDragged = false;
            _isFired = true;
        }

        // check game area boundaries
        if ((transform.position.x < LeftBorder) || (transform.position.x > RightBorder))
        {
            Reset();
        }
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isDragged = true;
            _originalPosition = transform.position;
            _originalMousePosition = Input.mousePosition;
        }
        
    }

    void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _isFired = false;
		GetComponent<Rigidbody>().useGravity = false;

        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }


    void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 60, 25), "Restart"))
        {
            Application.LoadLevel("Level1");
        }
    }
}