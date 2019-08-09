using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    // Public variables
    public string Card;

    // Private variables
    private Animator Anim;
    private static CardController Controller;
    private bool Locked = false;

    // Start is called before the first frame update
    void Start()
    {
        Anim = this.GetComponent<Animator>();

        // Find the card controller
        if (!Controller) {
            Controller = GameObject.Find("CardController").GetComponent<CardController>();
        }
    }

    // When mouse is pressed play the animation and add the card to the controller
    void OnMouseDown()
    {
        if (Controller.DisableAllCards) return; // If we are in the process of checking 2 cards
        if (Locked) return; // Dont execute again if the card is already locked

        Anim.Play(Card);
        Controller.AddCard(this.gameObject);
        Locked = true;
    }

    // Animates out the card, also unlocks it
    public void CloseCard()
    {
        Locked = false;
        Anim.SetTrigger("AnimateBack");
    }

    // Lock the card completely. Used when the card was matched and should no longer be available
    public void LockCard()
    {
        Locked = true;
    }
}
