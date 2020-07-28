using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Globalization;

public class TimeManager : MonoBehaviour
{
    public UnityEvent newDay;

    private string format = "yyyy-MM-dd-HH-mm-ss";

    private DateTime lastEnter;

    /* Не актуально */
    //private int error;

    private void Start()
    {
        /* Загружаем данные, если они есть */
        var data = Load.LoadTimeData();
        //TimeData data = null; /* Для сброса сохранений времени */

        /* Первый вход */
        if (data == null)
        {
            /* Установить разницу часовых поясов */
            Debug.Log("Первый вход, сохранение данных");
            //StartCoroutine(SetLocalToLondon());
        }
        else /* Мы нашли сохраненные данные */
        {
            /* Вытаскиваем */
            lastEnter = DateTime.Parse(data.lastEnter);
        }
        /* С сохраненными данными разобрались */
        /* Проверяем сколько времени прошло с прошлого входа */
        StartCoroutine(CheckTime());
    }

    /* Приводим к одному часовому поясу */
    /* Не актуально */
    private IEnumerator SetLocalToLondon()
    {
        WWW www = new WWW("http://dotamini.eu5.net/serverTime/time.php");
        yield return www;

        string time = www.text;

        DateTime localTime = DateTime.Now;
        DateTime LondonTime = DateTime.ParseExact(time, format, CultureInfo.InvariantCulture);

        //error = localTime.Hour - LondonTime.Hour;
        lastEnter = localTime;
    }

    private IEnumerator CheckTime()
    {
        WWW www = new WWW("http://dotamini.eu5.net/serverTime/time.php");
        yield return www;

        if (www.error != null)
        {
            /* Указывать на то, что нет соединения с интернетом, а оно обязательно */
            Debug.LogWarning("Нет соединения и ентернетом");
            yield break;
        }
        else
        {
            string time = www.text;

            if (lastEnter.Year >= 2020)
            {
                var nowEnterTime = DateTime.ParseExact(time, format, CultureInfo.InvariantCulture);
                var previousEnterTime = lastEnter;

                /* Проверка значений */
                Debug.Log("nowEnterTime - " + nowEnterTime);
                Debug.Log("previousEnterTime - " + previousEnterTime);

                /* Не актуально */
                //previousEnterTime = previousEnterTime.AddHours(-error); /* -error т.к. если local>London, то нужно вычесть, чтоб local=London и наоборот */

                TimeSpan timeSpan = nowEnterTime - previousEnterTime;

                Debug.LogFormat("С прошлого входа прошло {0} часов {1} минут {2} секунд",
                    timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                if (nowEnterTime.Day != previousEnterTime.Day)
                    newDay.Invoke();
            }

            lastEnter = DateTime.ParseExact(time, format, CultureInfo.InvariantCulture); /* Последний раз заход был сейчас */
            Save.SaveTimeData(this); /* сохраняем новые значения */
        }
    }

    public string GetLastEnter()
    {
        return lastEnter.ToString();
    }
}
