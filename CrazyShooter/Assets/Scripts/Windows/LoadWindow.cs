using System;
using System.Collections;
using SMC.Windows;
using UnityEngine;
using UnityEngine.UI;

public class LoadWindow : Window<LoadWindowSetup>
{
    [SerializeField] private Image filler;
    private float _loadSpeed = 1f;
    public override void Setup(LoadWindowSetup setup)
    {
        filler.fillAmount = 0;
        StartCoroutine(LoadData(setup.onLoadAction));
    }

    //Пока нет баланса будет загрушка
    private IEnumerator LoadData(Action onLoadAction)
    {
        while (filler.fillAmount < 1)
        {
            filler.fillAmount  = Mathf.Lerp(0, 1, Time.time);
            yield return null;
        }

        onLoadAction?.Invoke();
        StopAllCoroutines();
    }
}

public class LoadWindowSetup : WindowSetup
{
    public Action onLoadAction;
}
