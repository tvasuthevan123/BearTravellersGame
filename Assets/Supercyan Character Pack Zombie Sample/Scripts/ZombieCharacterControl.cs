using UnityEngine;
using System.Collections;

public class ZombieCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        Tank,
        Direct,
        Stop
    }

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Tank;

    [SerializeField] private Transform player;
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip defaultSound;



    private Vector3 m_currentDirection = Vector3.zero;
    private bool playerInRange = false;

    private void Awake()
    {
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void FixedUpdate()
    {
        switch (m_controlMode)
        {
            case ControlMode.Tank:
                TankUpdate();
                break;
            
            case ControlMode.Stop:
                // Debug.Log("Attacking");
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }
    }

    private void TankUpdate()
    {
        Vector3 distanceVector = player.position - transform.position;

        if(distanceVector.magnitude < 1.6){
            m_animator.SetFloat("MoveSpeed", 0);
            m_animator.SetTrigger("Attack");
        }
        else{
            Vector3 direction = distanceVector.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * m_turnSpeed);

            m_animator.SetFloat("MoveSpeed", m_moveSpeed);
        }
    }

    void AttackPlayer(){
        StartCoroutine(PlayAttackSound());
        if(playerInRange){
            playerHealth.TakeDamage(20f);
        }
    }

    IEnumerator PlayAttackSound(){
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = attackSound;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        audio.Play();
        audio.clip = defaultSound;
        audio.loop = true;
    }

    void DisableMovement(){
        m_controlMode = ControlMode.Stop;
    }

    void EnableMovement(){
        m_controlMode = ControlMode.Tank;
    }

    public void Die(){
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        m_controlMode = ControlMode.Stop;
        m_animator.SetTrigger("Dead");
        
    }

    public void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=true;
    }

    public void OnTriggerExit(Collider collider){
        if(collider.gameObject.tag == "Player")
            playerInRange=false;
    }
}
