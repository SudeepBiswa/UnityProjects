using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuizSystem : MonoBehaviour
{
    public int answersPerQuestion;

    public string[] questions;
    public string[] answers;
    public string[] wrongAnswers;

    public GameObject wall;
    public GameObject sphere;
    //public GameObject questionText;

    public float spacing; // Desired space between spheres

    private float wallWidth;
    private float wallHeight;
    private float sphereDiameter;

    private string state;

    private int index;
    private float score;


    void Start()
    {
        index = 0;
        state = "FIRST";

        //get the the x size of the wall and sphere to be able to give the spheres an even spacing
        Renderer wallRenderer = wall.GetComponent<Renderer>();
        wallWidth = wallRenderer.bounds.size.x;
        Renderer sphereRenderer = sphere.GetComponent<Renderer>();
        sphereDiameter = sphereRenderer.bounds.size.x;
    }
    void Update()
    {
        if (wall.activeSelf)
        {
            if (state.Equals("ACTIVE"))
            {
                //if the wall is active and the staqte is active then loop through the spheres empty object and check if the sphere that was activated held the correct. reward the player
                //if the answer was correct and increase the index change state to transition. if the answer was not correct then dont reward player.
                foreach (Transform sp in transform.Find("Spheres"))
                {
                    GameObject activator = sp.transform.Find("Activator").gameObject;
                    TextMeshProUGUI answerText = sp.transform.Find("Canvas").transform.Find("answer").GetComponent<TextMeshProUGUI>();
                    if (!activator.activeSelf)
                    {
                        if (answerText.text.Equals(answers[index]))
                        {
                            index++;
                            score += 1f / questions.Length;
                            
                            state = "TRANSITION";
                            break;
                        }
                        else
                        {
                            index++;
                            state = "TRANSITION";
                            break;
                        }
                    }
                }
            }
            else if (state.Equals("OVER"))
            {
                quizFinished();
            }
            else if (state.Equals("TRANSITION"))
            {
                TransitionToNextQuestion();
            }
            else if (state.Equals("FIRST"))
            {
                transform.Find("StartQuiz").gameObject.SetActive(false);
                InitializeSpheres();
            }
        }
    }

    void DisplayQuestion()
    {
        //displays questions as long the index is not more than the amount of questions
        if (!(index < questions.Length))
        {
            state = "OVER";
        }
        else
        {
            transform.Find("wall").transform.Find("Canvas").transform.Find("question").GetComponent<TextMeshProUGUI>().text = questions[index];
        }
    }

    void InitializeSpheres()
    {
        //creates the sphere with the specificed spacing by using the following math.
        float totalRequiredWidth = (answersPerQuestion * sphereDiameter) + ((answersPerQuestion - 1) * spacing);
        if (totalRequiredWidth <= wallWidth)
        {
            for (int i = 0; i < answersPerQuestion; i++)
            {
                float xPos = (i * (sphereDiameter + spacing)) - (totalRequiredWidth / 2) + (sphereDiameter / 2);
                Vector3 spawnPosition = new Vector3(wall.transform.position.x + xPos, wall.transform.position.y - 0.7f, wall.transform.position.z - 0.2f);
                Instantiate(sphere, spawnPosition, Quaternion.identity, transform.Find("Spheres"));
            }
            state = "TRANSITION";
        }
        else
        {
            Debug.LogError("The spheres and spacing exceed the width of the wall.");
        }
    }

    void TransitionToNextQuestion()
    {
        //displays the question and chooses a random orb to hold the correct answer and assigns random wrong answers to the remaining orbs. then proceeds to the active state
        int count = 0;
        int randomValue = UnityEngine.Random.Range(0, answersPerQuestion);
        DisplayQuestion();

        foreach (Transform sp in transform.Find("Spheres"))
        {

            
            GameObject activator = sp.transform.Find("Activator").gameObject;
            TextMeshProUGUI answerText = sp.transform.Find("Canvas").transform.Find("answer").GetComponent<TextMeshProUGUI>();
            activator.SetActive(false);
            moveSphere(sp.gameObject, "TrDown", false);
            moveSphere(sp.gameObject, "TrUp", true);

           // activator.SetActive(true);
            changeAnswers(sp.gameObject, answerText, count, randomValue);
            
            count++;
            activator.SetActive(true);
        }
        
        new WaitForSeconds(1);
        state = "ACTIVE";
    }
    void quizFinished()
    {
        //displays the score once the player gets through all questions
        float percentageScore = score * 100;

        transform.Find("wall").transform.Find("Canvas").transform.Find("question").GetComponent<TextMeshProUGUI>().text = "Score:\n" + percentageScore.ToString("F2") + "%";
        transform.Find("Spheres").gameObject.SetActive(false);
        GameObject tryAgain = transform.Find("TryAgain").gameObject;

        //activeates the try again cube if the score was not a 100%
        if (!tryAgain.activeSelf && score < 1)
        {
            print("activate try again");
            tryAgain.SetActive(true);
        }
        else if (tryAgain.transform.Find("Activator").gameObject.activeSelf && score < 1)
        {
            //resets the quiz if the player clicks try again.
            print("try again");
            score = 0;
            index = 0;
            transform.Find("Spheres").gameObject.SetActive(true);
            tryAgain.transform.Find("Activator").gameObject.SetActive(false);
            tryAgain.SetActive(false);
            state = "TRANSITION";
        }
        else if (score == 1)
        {
            //activates the interaction to close/complete the quiz if the player got a 100%
            transform.Find("wall").Find("Interactable").gameObject.SetActive(true);
        }
    }

    void moveSphere(GameObject obj, string animation, bool state)
    {
        Animator mAnimator = obj.GetComponent<Animator>();
        if(mAnimator != null)
        {
                print("activate anim");
                mAnimator.SetTrigger(animation);
        }
    }
    void changeAnswers(GameObject obj, TextMeshProUGUI answerText, int count, int randomValue)
    {
        if (answerText == null)
        {
            Debug.LogError("Answer TextMeshPro is missing in one of the spheres.");
        }
        if (count == randomValue)
        {
            answerText.text = answers[index];
        }
        else
        {
            answerText.text = wrongAnswers[UnityEngine.Random.Range(0, wrongAnswers.Length)];
        }
    }
}