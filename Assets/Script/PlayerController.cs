using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Доступ к контроллеру из других объектов
    public static PlayerController GetInstance ()
    {
        if (_instance==null)
        {
            Debug.LogError("Missing playerController");
        }
        return _instance;
    }
    private static PlayerController _instance;
    private void OnEnable()
    {
        _instance = this;
    }
    #endregion

    [SerializeField] private Animator myAnimator;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float speed;
    [SerializeField] private float sideSpeed;
    [SerializeField] private Rigidbody myRb;

    bool controllable;
    public void PrepareToFinish ()
    {
        controllable = false;
    }

    bool finished;
    public bool Finished => finished;
    public void Finish ()
    {
        finished = true;
        speed = 0;
        myRb.velocity = Vector3.zero;
        Vector3 targetPosition = LevelController.GetInstance().GetTargetPosition();
        transform.rotation.SetLookRotation(new Vector3 (targetPosition.x, 0 ,targetPosition.z));
        myAnimator.SetTrigger("Win");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            controllable = false;
            speed = 0;
            myAnimator.SetTrigger("Lose");
        }        
    }

    private void Start()
    {
        myRb.velocity = new Vector3(0, 0, speed);
        InAir = false;
        controllable = true;
        finished = false;

        score = 0;
    }

    private void Update()
    {
        scoreText.fontSize = SizeFromTimer(scoreAnimationTimer);
        scoreAnimationTimer = Mathf.Max(0, scoreAnimationTimer - Time.deltaTime);

        InAir = !CheckGround(landingDistance);

        if (speed != 0)
        {
            if (controllable)
            {
                if (transform.position.x > 2.21f)
                {
                    myRb.velocity = new Vector3(Mathf.Min(0f, sideSpeed * Input.GetAxis("Horizontal")), myRb.velocity.y, speed);
                }
                else if (transform.position.x < -2.21f)
                {
                    myRb.velocity = new Vector3(Mathf.Max(0f, sideSpeed * Input.GetAxis("Horizontal")), myRb.velocity.y, speed);
                }
                else
                {
                    myRb.velocity = new Vector3(sideSpeed * Input.GetAxis("Horizontal"), myRb.velocity.y, speed);
                }

                if (Input.GetKeyDown(KeyCode.Space) & CheckGround(groundedDistance))
                {
                    Jump();
                }
                else if (Input.GetKeyDown("r"))
                {
                    myAnimator.SetTrigger("Rolling");
                }
            }
            else
            {
                myRb.velocity = new Vector3(0, myRb.velocity.y, speed);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("restart");
                UnityEngine.SceneManagement.SceneManager.LoadScene("PlayableLevel");
            }
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.22f, 2.22f), transform.position.y, transform.position.z);
    }

    #region Jumping
    bool inAir = false;
    bool InAir
    {
        get => inAir;
        set
        {
            inAir = value;
            myAnimator.SetBool("InAir", value);
        }
    }

    [SerializeField] private float groundedDistance;
    [SerializeField] private float landingDistance;
    bool CheckGround (float distance)
    {
        return (Physics.Raycast(transform.position, Vector3.down, distance, 1 << 8));
    }

    private void Jump ()
    {
        myRb.velocity = new Vector3(myRb.velocity.x, jumpSpeed, myRb.velocity.z);
        myAnimator.SetTrigger("Jump");
    }
    #endregion

    #region Score
    [SerializeField] TMPro.TMP_Text scoreText;
    private float scoreAnimationTimer = 0;
    private float SizeFromTimer (float x)
    {
        return 36 + 96 * x - 96 * x * x;
    }
    private int _score;
    private int score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = ""+value;
            scoreAnimationTimer = 1f;
        }
    }
    public void RaiseScore()
    {
        score++;
        myAnimator.SetInteger("ShurikensToThrow", myAnimator.GetInteger("ShurikensToThrow") + 1);
    }
    #endregion

    #region Rolling
    [SerializeField] CapsuleCollider capsColliderRunning;
    [SerializeField] CapsuleCollider capsColliderRolling;

    public void StartRolling ()
    {
        capsColliderRunning.enabled = false;
        capsColliderRolling.enabled = true;
    }

    public void EndRolling ()
    {
        capsColliderRunning.enabled = true;
        capsColliderRolling.enabled = false;
    }
    #endregion
}
