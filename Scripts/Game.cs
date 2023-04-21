using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text score = null;

    private float points = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            points += 1f;
            score.text = "Score: " + points.ToString();
        }
    }
}
