using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public int blueCluesCollected;
    public int redCluesCollected;
    public bool cctvDisabled;
    public float speed;
    protected Rigidbody2D rb;
    protected Vector2 input;

    public void CollectClue(string type)
    {
        if (type == "Red") redCluesCollected++;
        if (type == "Blue") blueCluesCollected++;
    }

    public bool CanProceed(string character)
    {
        if (character == "Sam") return blueCluesCollected >= 3 && cctvDisabled;
        if (character == "Cat") return redCluesCollected >= 3 && cctvDisabled;
        return false;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }
}

