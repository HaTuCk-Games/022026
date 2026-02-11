using UnityEngine;
using UnityEngine.UI;
using AK.Wwise;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public  int _speed = 2;
    [SerializeField] private Animator animator;

    private Rigidbody rb;
    private Vector3 moveInput;
    public bool walking;
    public bool isplaywalking = false;
    private ViewModel viewModel;
    public AK.Wwise.Event soundEvent;
    public AK.Wwise.Event soundEvent2;
    public float minStepInterval = 2f;
    public int Speed
    {
        get => _speed;
        set
        {
            if (_speed == value) return;
            _speed = value;
            if (viewModel != null) viewModel.Speed = _speed.ToString();
        }
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    private void Start()
    {
        viewModel = FindObjectOfType<ViewModel>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        moveInput = new Vector3(horizontal, 0f, vertical);
        walking = moveInput.magnitude > 0.05f; 
        animator.SetBool("walking", walking);

        if (Input.GetKeyDown(KeyCode.Space))

        {
            animator.SetTrigger("Attack");
            PlaySound2();
        }
        if (viewModel != null) viewModel.Speed = _speed.ToString();
        if (walking && !isplaywalking)
        {
            StartCoroutine(PlayStepWithDelay());
        }
    }
    public void AttackUIButton()
    {
        animator.SetTrigger("Attack");
        PlaySound2();
    }

    void FixedUpdate()
    {
        if (moveInput.magnitude >= 0.05f)
        {
            Vector3 movement = moveInput.normalized * _speed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
            Quaternion targetRotation = Quaternion.LookRotation(moveInput, Vector3.up);
            rb.MoveRotation(targetRotation);
        }
    }
    
    public void PlaySound2()
    {
        if (soundEvent != null)
        {
            soundEvent2.Post(gameObject);
        }
        else
        {
            Debug.LogWarning("Событие WWise не задано!");
        }
    }
    IEnumerator PlayStepWithDelay()
    {
        isplaywalking = true;
        soundEvent.Post(gameObject);
        yield return new WaitForSeconds(minStepInterval);
        isplaywalking = false;
    }
}
