using UnityEngine;

/// <summary>
/// Zusehen, dass ein Collider am Kopf einer Leiter nur von oben, nicht von unten angesprochen wird
/// 
/// Nach zwei Stunden Gefummel: "Good enough" state achieved.
/// </summary>

public class LadderCollider : MonoBehaviour
{
    private GameObject playerSprite = null;
    //private float detectionOffset = 0f; // Feinjustierung
    private float playerHalfSize = 0f;
    private float playerCurrYPos = 0f;
    //private float playerUpperBound = 0f;
    private float ladderUpperBound = 0f;

    private bool verbose = false;

    void Awake()
    {
        playerSprite = GameObject.Find("Player");
    }


    private void Update()
    {
        playerHalfSize = playerSprite.GetComponent<SpriteRenderer>().bounds.min.y;
        playerCurrYPos = playerSprite.GetComponent<Transform>().position.y;
        ladderUpperBound = gameObject.GetComponent<BoxCollider2D>().bounds.max.y;

        //float topOfPlayerSprite = playerCurrYPos + playerHalfSize + detectionOffset;
        bool ignore = false;

        if (playerCurrYPos < ladderUpperBound)
        {
            ignore = true;
            Physics2D.IgnoreCollision(
                playerSprite.GetComponent<Collider2D>(),
                gameObject.GetComponent<Collider2D>(),
                ignore);

            if (verbose)
                Debug.Log($"Ignore Coll: {ignore} | playerHalfSize: {playerHalfSize} | playerCurrYPos: {playerCurrYPos} | ladderUpperBound: {ladderUpperBound}"); // topOfPlayerSprite: {topOfPlayerSprite} |
        }
        else
        {
            ignore = false;
            Physics2D.IgnoreCollision(
                playerSprite.GetComponent<Collider2D>(),
                gameObject.GetComponent<Collider2D>(),
                ignore);

            if (verbose)
                Debug.Log($"Ignore Coll: {ignore} | playerHalfSize: {playerHalfSize} | playerCurrYPos: {playerCurrYPos} | ladderUpperBound: {ladderUpperBound}"); // topOfPlayerSprite: {topOfPlayerSprite} |
        }
    }
}
