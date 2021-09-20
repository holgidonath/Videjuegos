using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingTurtle : MonoBehaviour
{
    private Animator anim;
    int animLayer = 0;
    private Collider2D boxCol;
    private bool isSinking = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        boxCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName("Turtle Sink") &&
            anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime >= 0.45f)
        {
            isSinking = true;
            toggleCollider();
        }
        else if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName("Turtle Move") &&
            anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime >= 0.1f)
        {
            isSinking = false;
            toggleCollider();
        }
    }

    void toggleCollider()
    {
        if (isSinking)
        {
            boxCol.enabled = false;
        }
        else
        {
            boxCol.enabled = true;
        }
    }

    IEnumerator waitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
