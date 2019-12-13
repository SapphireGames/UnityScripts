using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public GameObject StartPoint; // исходная точка
    [Range(0.1f, 5.0f)]
    public float Speed; // скорость персонажа
    [SerializeField]
    private GameObject TempPoint; // следующая точка для перехода
    public Persons _person;
    private bool IsStarted; // игрок выбрал, какие двери закрыть/открыть - клик по персонажу - игра начата

    public enum Persons
    {
        Common,
        Eater,
        Business,
        Gamer
    }

    public enum Types
    {
        Intersection, // перекресток
        Common, // обычная точка
        Trap, // ловушка
        Final // финиш
    }

    void Start()
    {
        transform.position = StartPoint.transform.position;
        IsStarted = false;
        if (_person.Equals(null)) _person = Persons.Common;
    }

    public void Begin()
    {
        if (!IsStarted)
        {
            foreach (GameObject Point in GameObject.FindGameObjectsWithTag("PathPoint"))
            {
                Point.GetComponent<PathPoint>().SetPoints();
            }
            TempPoint = StartPoint.GetComponent<PathPoint>().achievablePoints[0];
        }
        IsStarted = true;
    }

    void Update()
    {
        if (IsStarted)
        {
            if (TempPoint != null && TempPoint.GetComponent<PathPoint>().Achievable)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), TempPoint.transform.position, Speed * Time.deltaTime);
            }
            else
            {
                TempPoint = null;
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), StartPoint.transform.position, Speed * Time.deltaTime);
            }
        }
        if (TempPoint != null && transform.position == TempPoint.transform.position)
        {
            if (TempPoint.GetComponent<PathPoint>().achievablePoints.Count > 0)
            {
                bool isFound = false; // to check if we have already updated next point
                //TempPoint = TempPoint.GetComponent<PathPoint>().achievablePoints[Random.Range(0, TempPoint.GetComponent<PathPoint>().achievablePoints.Count)];
                foreach (GameObject point in TempPoint.GetComponent<PathPoint>().achievablePoints)
                {
                    if (point.GetComponent<Distractor>() != null)
                    {
                        if (point.GetComponent<Distractor>().IsDistracted(_person))
                        {
                            TempPoint = point;
                            isFound = true;
                        }
                        else
                        {
                            TempPoint = TempPoint.GetComponent<PathPoint>().achievablePoints[Random.Range(0, TempPoint.GetComponent<PathPoint>().achievablePoints.Count)];
                            isFound = true;
                        }
                    }
                }
                if (isFound == false) TempPoint = TempPoint.GetComponent<PathPoint>().achievablePoints[Random.Range(0, TempPoint.GetComponent<PathPoint>().achievablePoints.Count)];
            }
        }
    }
}
