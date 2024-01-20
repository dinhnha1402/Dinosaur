using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;
    public AnimatedSprite animatedSprite;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animatedSprite = GetComponent<AnimatedSprite>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        if(GameManager.Instance.ResumeButton.isActiveAndEnabled == false)
        {
            direction += Vector3.down * gravity * Time.deltaTime;

            if (character.isGrounded)
            {
                direction = Vector3.down;

                if (Input.GetButton("Jump"))
                {
                    direction = Vector3.up * jumpForce;
                }
            }

            character.Move(direction * Time.deltaTime);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

}
