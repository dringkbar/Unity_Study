using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {
    private bool beenHit = false;
    private Animator animator;
    private GameObject parent;
    private bool activated;
    private Vector3 originalPos;

    public float moveSpeed = 1f;
    public float frequency = 5f;
    public float magnitude = 0.1f;

	// Use this for initialization
	void Start () {
        parent = transform.parent.gameObject;
        originalPos = parent.transform.position;
        animator = parent.GetComponent<Animator>();
	}
    void OnHidden()
    {
        parent.transform.position = originalPos;
        activated = false;
    }
    public IEnumerator HideTarget()
    {
        yield return new WaitForSeconds(.5f);
        iTween.MoveBy(parent.gameObject, iTween.Hash("y", (originalPos.y - parent.transform.position.y), 
            "easeType", "easeOutQuad", "loopType", "none", "time", "0.5", 
            "oncomplete", "OnHidden", "oncompletetarget", gameObject));

    }

    private void Awake()
    {
        GameController._instance.targets.Add(this);
    }

    private void OnMouseDown()
    {
        if (!beenHit && activated)
        {
            GameController._instance.IncreaseScore();
            beenHit = true;
            animator.Play("Flip");
            StopAllCoroutines();
            StartCoroutine(HideTarget());
        }
    }

    public void ShowTarget()
    {
        if (!activated)
        {
            activated = true;
            beenHit = false;
            animator.Play("Idel");
            iTween.MoveBy(parent, iTween.Hash("y", 1.4, "easeType", "easeInOutExpo", "time", 0.5,"oncomplete","OnShown","oncompletetarget",gameObject));
        }
    }

    void OnShown()
    {
        StartCoroutine("MoveTarget");
    }

    IEnumerator MoveTarget()
    {
        var relativeEndPos = parent.transform.position;
        if (transform.eulerAngles == Vector3.zero)
        {
            relativeEndPos.x = 6;
        }
        else { relativeEndPos.x = -6; }

        var movementTime = Vector3.Distance(parent.transform.position, relativeEndPos) * moveSpeed;
        var pos = parent.transform.position;
        var time = 0f;

        while (time < movementTime)
        {
            time += Time.deltaTime;

            pos += parent.transform.right * Time.deltaTime * moveSpeed;
            parent.transform.position = pos + (parent.transform.up * Mathf.Sin(Time.time * frequency) * magnitude);
            yield return new WaitForSeconds(0);
        }
        StartCoroutine(HideTarget());
    }
}
