using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnPhone()
    {
        animator.SetBool("PhoneClicked", true);
    }

    public void TurnOffPhone()
    {
        animator.SetBool("PhoneClicked", false);
    }
}
