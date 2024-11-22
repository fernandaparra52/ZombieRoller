using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {
    public GameObject selectedZombie;
    public List<GameObject> zombies;
    public Vector3 selectedSize;
    public Vector3 defaultSize;
    public TMP_Text scoreText;
    private int selectedZombiePosition = 0;
    private int score = 0;
	public float fallLimit = -10f; // Posición mínima en Y para considerar que cayeron

   void Start() {
	SelectZombie(selectedZombie);
    scoreText.text = "Score: " + score;
    // Ajustar la altura de cada zombi aleatoriamente
    foreach (GameObject zombie in zombies) {
        float randomHeight = Random.Range(0.8f, 1.5f); // Generar una altura aleatoria
        zombie.transform.position = new Vector3(zombie.transform.position.x, randomHeight, zombie.transform.position.z); // Cambiar la altura
    }

    
}


    void Update () {
        if (Input.GetKeyDown("left")) {
            GetZombieLeft();
        }
        if (Input.GetKeyDown("right")) {
            GetZombieRight();
        }
        if (Input.GetKeyDown("up")) {
            PushUp();
        }
		   CheckIfZombiesFell(); // Verifica si los zombis cayeron
    }

    void SelectZombie(GameObject newZombie) {
        selectedZombie.transform.localScale = defaultSize;
        selectedZombie = newZombie;
        newZombie.transform.localScale = selectedSize;
    }

    void GetZombieLeft() {
        if (selectedZombiePosition == 0) {
            selectedZombiePosition = 3;
            SelectZombie(zombies[3]);
        } else {
            selectedZombiePosition = selectedZombiePosition - 1;
            GameObject newZombie = zombies[selectedZombiePosition];
            SelectZombie(newZombie);
        }
    }

    void GetZombieRight() {
        if (selectedZombiePosition == 3) {
            selectedZombiePosition = 0;
            SelectZombie(zombies[0]);
        } else {
            selectedZombiePosition = selectedZombiePosition + 1;
            GameObject newZombie = zombies[selectedZombiePosition];
            SelectZombie(newZombie);
        }
    }

    void PushUp() {
        Rigidbody rb = selectedZombie.GetComponent<Rigidbody>();
        rb.AddForce(0, 0, 10, ForceMode.Impulse);
    }

    public void AddPoints() {
        score = score + 1;
        scoreText.text = "Score: " + score;
    }
	 void CheckIfZombiesFell()
    {
        foreach (GameObject zombie in zombies)
        {
            if (zombie.transform.position.y > fallLimit)
            {
                return; // Al menos un zombi no ha caído
            }
        }

        // Si todos los zombis cayeron
        RestartGame();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia el nivel actual
    }
}
