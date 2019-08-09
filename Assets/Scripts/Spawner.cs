using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Public
    public float GridGap = 1.5f;
    public int CardNum = 6;
    public int MaxCardNum = 16;
    public GameObject[] Prefabs;
    public GameObject CardsContainerPrefab; // The prefab used to create CardsContainer
    public GameObject CardsContainer;

    // Private
    private List<Vector3> Grid; // Create a grid of Vector3 positions to place cards
    private List<CardPair> CardPairs; // Store all created cards here
    private struct CardPair {
        public GameObject CardOne;
        public GameObject CardTwo;

        public CardPair(GameObject One, GameObject Two) {
            CardOne = One;
            CardTwo = Two;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Make sure that CardNum is even
        if (CardNum % 2 != 0)
            throw new UnityException("CardNum must be an EVEN number!");

        // Start the game loop
        GameLoop(true);
    }

    public void GameLoop(bool Initial)
    {   
        // Increase the number of cards unless its the Initial game loop or CardNum exceeds the maximum
        if (!Initial && !(CardNum >= MaxCardNum))
            CardNum += 2;

        // Create the lists
        Grid = new List<Vector3>(CardNum);
        CardPairs = new List<CardPair>(CardNum);

        // Create the cards container so we can attach the cards to it later
        if (CardsContainer)
            Destroy(CardsContainer);
        CardsContainer = Instantiate(CardsContainerPrefab, Vector3.zero, Quaternion.identity);
        HideContainer(); // Hide until its ready

        MakeGrid();
        SpawnCards();
    }

    // Spawns a number of cards in the scene
    private void SpawnCards()
    {
        // Make a local list of prefabs for use so we dont remove objects from the global one
        List<GameObject> PrefabsList = new List<GameObject>(Prefabs);

        for (; 0 < Grid.Count;) // The grid count decreases since we are removing items
        {
            // First get 2 positions and remove them from the list
            Vector3 CardOnePos = Grid[Random.Range(0, Grid.Count - 1)];
            Grid.Remove(CardOnePos);
            Vector3 CardTwoPos = Grid[Random.Range(0, Grid.Count - 1)];
            Grid.Remove(CardTwoPos);

            // Pick a prefab and remove it from the list of prefabs
            GameObject CardPrefab = PrefabsList[Random.Range(0, PrefabsList.Count - 1)];
            PrefabsList.Remove(CardPrefab);

            // Instantiate the cards and add them to the CardPairs
            GameObject CardOne = Instantiate(CardPrefab, CardOnePos, Quaternion.identity);
            GameObject CardTwo = Instantiate(CardPrefab, CardTwoPos, Quaternion.identity);
            CardOne.name = "Card";
            CardTwo.name = "Card";
            CardOne.transform.parent = CardsContainer.transform;
            CardTwo.transform.parent = CardsContainer.transform;
            CardPair Pair = new CardPair(CardOne, CardTwo);
            CardPairs.Add(Pair);
        }

        // The container is now ready so we show it
        ContainerAnimateIn();
    }

    // Makes a grid of an even number of transforms positioned around the center of the screen
    private void MakeGrid()
    {
        // Looks better visually with those
        float HalfGridGap = GridGap / 2;
        float DoubleGridGap = GridGap * 2;

        if (CardNum == 6)
            for (int i = 0; i < 2; i ++) // Loop 2 times - 2 rows
            {
                float Ycoord = i < 1 ? GridGap / 2 : GridGap / -2;
                Grid.Add(new Vector3(-GridGap, Ycoord));
                Grid.Add(new Vector3(0.0f, Ycoord));
                Grid.Add(new Vector3(GridGap, Ycoord));
            }
        else if (CardNum == 8)
            for (int i = 0; i < 2; i ++) // Loop 2 times - 2 rows
            {
                float Ycoord = i < 1 ? GridGap / 2 : GridGap / -2;
                Grid.Add(new Vector3(-GridGap -HalfGridGap, Ycoord));
                Grid.Add(new Vector3(-HalfGridGap, Ycoord));
                Grid.Add(new Vector3(HalfGridGap, Ycoord));
                Grid.Add(new Vector3(GridGap + HalfGridGap, Ycoord));
            }
        else if (CardNum == 10)
            for (int i = 0; i < 2; i ++) // Loop 2 times - 2 rows
            {
                float Ycoord = i < 1 ? GridGap / 2 : GridGap / -2;
                Grid.Add(new Vector3(-DoubleGridGap, Ycoord));
                Grid.Add(new Vector3(-GridGap, Ycoord));
                Grid.Add(new Vector3(0.0f, Ycoord));
                Grid.Add(new Vector3(GridGap, Ycoord));
                Grid.Add(new Vector3(DoubleGridGap, Ycoord));
            }
        else if (CardNum == 12)
            for (int i = 0; i < 3; i ++) // Loop 3 times - 3 rows
            {
                float Ycoord = i < 1 ? GridGap : (i < 2 ? 0.0f : -GridGap);
                Grid.Add(new Vector3(-GridGap -HalfGridGap, Ycoord));
                Grid.Add(new Vector3(-HalfGridGap, Ycoord));
                Grid.Add(new Vector3(HalfGridGap, Ycoord));
                Grid.Add(new Vector3(GridGap + HalfGridGap, Ycoord));
            }
        else if (CardNum == 14)
            for (int i = 0; i < 2; i ++) // Loop 2 times - 2 rows
            {
                float Ycoord = i < 1 ? -HalfGridGap : HalfGridGap;
                Grid.Add(new Vector3(-GridGap, Ycoord));
                Grid.Add(new Vector3(-DoubleGridGap, Ycoord));
                Grid.Add(new Vector3(-GridGap * 3, Ycoord));
                Grid.Add(new Vector3(0.0f, Ycoord));
                Grid.Add(new Vector3(GridGap, Ycoord));
                Grid.Add(new Vector3(DoubleGridGap, Ycoord));
                Grid.Add(new Vector3(GridGap * 3, Ycoord));
            }
        else if (CardNum == 16)
            for (int i = 0; i < 4; i ++) // Loop 4 times - 4 rows
            {
                float Ycoord = i == 0 ? -GridGap -HalfGridGap : i == 1 ? -HalfGridGap : i == 2 ? HalfGridGap : GridGap + HalfGridGap;
                if (i == 0 || i == 3)
                {
                    Grid.Add(new Vector3(-GridGap, Ycoord));
                    Grid.Add(new Vector3(0.0f, Ycoord));
                    Grid.Add(new Vector3(GridGap, Ycoord));
                }
                else
                {
                    Grid.Add(new Vector3(-DoubleGridGap, Ycoord));
                    Grid.Add(new Vector3(-GridGap, Ycoord));
                    Grid.Add(new Vector3(0.0f, Ycoord));
                    Grid.Add(new Vector3(GridGap, Ycoord));
                    Grid.Add(new Vector3(DoubleGridGap, Ycoord));
                }
            }
        // If none of these presets were chosen then throw an error
        else
            throw new UnityException("CardNum must have one of the following values: 6, 8, 10, 12, 14 or 16");
    }

    // Hide and show (animate) the container when needed
    public void HideContainer()
    {
        CardsContainer.SetActive(false);
    }
    public void ContainerAnimateIn()
    {
        CardsContainer.SetActive(true);
        CardsContainer.GetComponent<Animator>().SetTrigger("AnimateIn");
    }
    public void ContainerAnimateOut()
    {
        CardsContainer.GetComponent<Animator>().SetTrigger("AnimateOut");
    }
}
