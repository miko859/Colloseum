using UnityEngine;

/// <summary>
/// Class for fireball spells.
/// </summary>
public class Fireball : Ball
{
    private void Start()
    {
        manaCost = 10; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Activate(); 
        }
    }

    private void CastFireball()
    {
        Rigidbody fireballInstance = Instantiate(rb, transform.position + transform.forward, Quaternion.LookRotation(transform.forward));
        fireballInstance.velocity = transform.forward * speed; 
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other); 
        Destroy(gameObject); 
    }
}
