using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RatingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI ratingText;
    public GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        ratingText.SetText(" ");
    }

    // Update is called once per frame
    void Update()
    {
        GiveRating(manager.GetComponent<ScoreScript>().score);

    }
    void GiveRating(float score)
    {
        if (score == -1)
        {
            ratingText.SetText("L, YOU DIED");
        }
        else if (score == 0)
        {
            ratingText.SetText("Rating : Really,so trash.");

        }
        else if (score > 0 && score <= 100 && score < 250)
        {
            ratingText.SetText("Rating : Eh, not bad");
        }
        else if (score >= 250 && score < 500)
        {
            ratingText.SetText("Rating : More, do more!");
        }
        else
        {
            ratingText.SetText("Rating : XQC Level Plays");
        }
    }
}
