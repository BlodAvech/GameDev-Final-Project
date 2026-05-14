using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private float speed;
    private float maxSpeed;
    public float Speed
    {
        get => speed;
        set
        {
            speed = value;
            animator.SetFloat("Speed" , speed/maxSpeed);
        }
    }

    public float MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }
}
