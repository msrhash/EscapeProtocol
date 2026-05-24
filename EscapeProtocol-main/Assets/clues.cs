using UnityEngine;

public class Clue : MonoBehaviour
{
    // The type of clue: "CatClue" or "SamClue"
    public string clueType;  // Can be set in Unity Inspector for each clue

    // This function is called when another collider enters the trigger area of the clue
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object (collider) is the correct character
        if (other.CompareTag("Cat") && clueType == "catclue")
        {
            // Call the method to handle clue collection
            CollectClue();
        }
        else if (other.CompareTag("Sam") && clueType == "samclue")
        {
            // Call the method to handle clue collection
            CollectClue();
        }
    }

    // Method to collect the clue
    private void CollectClue()
    {
        // Log to the console that the clue has been collected
        Debug.Log(clueType + " collected!");

        // Destroy the clue object (or you could disable it if you want to reuse it later)
        Destroy(gameObject);
    }
}
