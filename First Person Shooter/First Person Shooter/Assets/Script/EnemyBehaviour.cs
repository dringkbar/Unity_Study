using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public enum State
    {
        Idle,
        Follow,
        Die
    }

    public State state;
    public Transform target;

    public float moveSpeed = 3.0f;
    public float rotateSpeed = 3.0f;
    public float followRange = 10.0f;
    public float idleRange = 10.0f;

    public float health = 100.0f;
    private float currentHealth;
    

    IEnumerator IdleState()
    {
        Debug.Log("Idle: Enter");
        while (state == State.Idle)
        {
            if (GetDistance() < followRange)
            {
                state = State.Follow;
            }
            yield return 0;
        }
        Debug.Log("Idle: Exit");
        GoToNextState();
    }

    IEnumerator FollowState()
    {
        Debug.Log("Follow: Enter");
        while (state == State.Follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
            RotateTowardsTarget();

            if (GetDistance() > idleRange)
            {
                state = State.Idle;
            }
            yield return 0;
        }
        Debug.Log("Follow: Exit");
        GoToNextState();
    }

    IEnumerator DieState()
    {
        Debug.Log("Die: Enter");
        Destroy(this.gameObject);
        yield return 0;
    }

    public float GetDistance()
    {
        return (transform.position - target.transform.position).magnitude;
    }

    private void RotateTowardsTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(target.position - transform.position), 
            rotateSpeed * Time.deltaTime);
    }

    void GoToNextState()
    {
        string methodName = state.ToString() + "State";
        System.Reflection.MethodInfo info = 
            GetType().GetMethod(methodName, System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }

    private void Start()
    {
        GoToNextState();
        currentHealth = health;
    }

    public void TakeDamage()
    {
        float damageToDo = 100.0f - (GetDistance() * 5);

        if (damageToDo < 0)
            damageToDo = 0;
        if (damageToDo > health)
            damageToDo = health;

        currentHealth -= damageToDo;

        if (currentHealth <= 0)
        {
            state = State.Die;
        }
        else
        {
            followRange = Mathf.Max(GetDistance(), followRange);
            state = State.Follow;
        }
        print("Ow! - Current Health: " +currentHealth.ToString());
    }
}
