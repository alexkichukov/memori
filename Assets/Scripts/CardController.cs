using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    // Public
    public bool DisableAllCards;
    public float AdditionalTimeBase = 2.0f;
    public int AdditionalScoreBase = 10;
    public GameOverMenu GameOver;

    // Private
    private GameObject CardOne;
    private GameObject CardTwo;
    private const float WaitTime = 0.9f;
    private int LockedCards = 0;

    // Default value to prevent bugs since the variable needs to be public
    void Start()
    {
        DisableAllCards = false;
    }


    // Add a card to the 'state'
    public void AddCard(GameObject card)
    {
        // Assign the card that is being added to one of the 2 spots
        if (!CardOne) 
        {
            CardOne = card;
        }
        else if (!CardTwo)
        {
            CardTwo = card;
            // Now we have 2 cards so we check them and disable all cards
            DisableAllCards = true;
            StartCoroutine(CheckCards());
        }
    }

    // Ends the game
    public IEnumerator EndGame()
    {
        // Animate out the card container
        GameObject.Find("Spawner").GetComponent<Spawner>().ContainerAnimateOut();
        yield return new WaitForSeconds(1.2f);

        // Show game over menu
        GameOver.ShowMenu();
    }


    // Checks if the 2 cards in the 'state' are the same
    private IEnumerator CheckCards()
    {
        if (CardOne.tag == CardTwo.tag)
        {
            // Increase the locked cards counter
            LockedCards += 2;
            // Get the spawner
            Spawner Spawn = GameObject.Find("Spawner").GetComponent<Spawner>();
            // Add additional time based on the difficulty
            Timer GameTimer = GameObject.Find("Timer").GetComponent<Timer>();
            GameTimer.AddTime(AdditionalTimeBase * (Spawn.CardNum < 10 ? 1 : Spawn.CardNum < 12 ? 1.5f : 1.25f));
            // Add score
            Score GameScore = GameObject.Find("Score").GetComponent<Score>();
            GameScore.AddScore(AdditionalScoreBase * (Spawn.CardNum / 2));

            // If the amount of locked cards equals the amount of total cards then the round is over
            if (LockedCards == Spawn.CardNum)
            {
                // The player got a whole stack of cards done so reward him a little more
                GameTimer.AddTime(AdditionalTimeBase * (Spawn.CardNum < 10 ? 1 : Spawn.CardNum < 12 ? 1.5f : 1.5f));
                GameScore.AddScore(AdditionalScoreBase * (Spawn.CardNum / 2));

                // Wait for last card to animate fully
                yield return new WaitForSeconds(0.6f);

                // Restart game loop and reset the locked cards counter
                Spawn.ContainerAnimateOut();
                yield return new WaitForSeconds(0.8f); // Wait for the container to animate out
                Spawn.GameLoop(false);
                LockedCards = 0;
            }
        }
        else
        {
            // Wait for the animation of the second card to finish
            yield return new WaitForSeconds(WaitTime);

            CardOne.GetComponent<Animate>().CloseCard();
            CardTwo.GetComponent<Animate>().CloseCard();
        }

        ResetState();
    }

    // Resets the 'state' by nulling the two cards enabling all cards
    private void ResetState()
    {
        CardOne = null;
        CardTwo = null;
        DisableAllCards = false;
    }

}
