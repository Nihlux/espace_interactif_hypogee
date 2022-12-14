using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class endGoal : MonoBehaviour
{
    // Start is called before the first frame update
   
        // The game object to set active on collision
        public GameObject tableau;
        public GameObject personnage;
    public GameObject recommencer;
    public GameObject quitter;
    // This function is called when the object collides with another object

    private void Start()
    {
        Debug.Log("Button clicked 8 times.");
    }
    void OnTriggerEnter2D(Collider2D personnage)
    {
        // Print the name of the object that we collided with
        Debug.Log("Collision detected with object: " + personnage.gameObject.name);
        tableau.SetActive(true);
    }
    public void resetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
        Debug.Log("Button clicked 8 times.");
    }

    public void Quitter()
    {
        Debug.Log("Quitte le jeu");
        Application.Quit();
    }
}
