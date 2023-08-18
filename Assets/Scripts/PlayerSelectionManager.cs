using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionManager : MonoBehaviour
{
    public GameObject[] characters; // An array of GameObjects representing the characters
    public GameObject[] selectionIndicators; // An array of GameObjects representing the selection indicators (child UI images) for each button

    private int selectedCharacterIndex = 0; // Default to selecting the first character

    private void Start()
    {
        // Deactivate all characters and selection indicators initially
        foreach (GameObject character in characters)
        {
            character.SetActive(false);
        }

        foreach (GameObject indicator in selectionIndicators)
        {
            indicator.SetActive(false);
        }

        // Activate the first character and its selection indicator
        characters[selectedCharacterIndex].SetActive(true);
        selectionIndicators[selectedCharacterIndex].SetActive(true);
    }

    public void OnCharacterButtonClick(int index)
    {

        // If the same character button is clicked again, do nothing
        if (selectedCharacterIndex == index)
            return;

        // Deactivate the currently selected character and its selection indicator
        characters[selectedCharacterIndex].SetActive(false);
        selectionIndicators[selectedCharacterIndex].SetActive(false);

        // Activate the newly selected character and its selection indicator
        characters[index].SetActive(true);
        selectionIndicators[index].SetActive(true);
        this.GetComponent<Player>().SetAnimator(characters[index].GetComponent<Animator>());

        // Update the selectedCharacterIndex
        selectedCharacterIndex = index;
    }
}
