using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageNumberUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    private float floatSpeed = 2f;
    private float lifeTime = 0.8f;

    private Action<DamageNumberUI> returnToPoolAction;

    public void Setup(float damageAmount, Action<DamageNumberUI> returnAction)
    {
        textMesh.text = Mathf.CeilToInt(damageAmount).ToString();
        textMesh.alpha = 1f;
        returnToPoolAction = returnAction;
        StartCoroutine(AnimateAndReturn());
    }
    private void Update()
    {
        
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    private IEnumerator AnimateAndReturn()
    {
        float elapsedTime = 0f;
        Color startColor = textMesh.color;

        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;

            
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / lifeTime);
            textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

            yield return null; 
        }

        returnToPoolAction?.Invoke(this);
    }
}
