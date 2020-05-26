using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class NarrativeText : SingletonPrefabBehaviour<NarrativeText>
{
    public float perCharacterDelay = .1f;
    public float popInTime = .2f;
    
    private Coroutine _showTextRoutine = null;
    
    private Canvas _Canvas => _canvas ? _canvas : GetComponent<Canvas>();
    private Canvas _canvas;
    
    private Image _TextContainer => _textContainer ? _textContainer : GetComponentInChildren<Image>();
    private Image _textContainer;

    private Text _Text => _text ? _text : GetComponentInChildren<Text>();
    private Text _text = null;

    [SerializeField] float _activeYPosition = 25;
    [SerializeField] float _hiddenYPosition = -300;

    public void ShowText(string text)
    {
        if (_showTextRoutine != null) StopCoroutine(_showTextRoutine);
        
        _showTextRoutine = StartCoroutine(AnimateText(text));
    }

    private IEnumerator AnimateText(string text)
    {
        _Text.text = "";
        float elapsedTime = 0;
        _Canvas.enabled = true;
        float startingY = _TextContainer.transform.position.y;
        while (elapsedTime < popInTime) {
            elapsedTime += Time.deltaTime;
            var targetY = Mathf.Lerp(_hiddenYPosition, _activeYPosition, elapsedTime / popInTime);
            ((RectTransform)_TextContainer.transform).anchoredPosition = new Vector3(0, targetY, 0);
            yield return null;
        }
        
        foreach (var character in text) {
            _Text.text += character;
            yield return new WaitForSeconds(perCharacterDelay);
        }

        yield return new WaitForSeconds(2f);
        elapsedTime = 0;
        while (elapsedTime < popInTime) {
            elapsedTime += Time.deltaTime;
            var targetY = Mathf.Lerp(startingY + _hiddenYPosition, startingY, elapsedTime / popInTime);
            ((RectTransform)_TextContainer.transform).anchoredPosition = new Vector3(0, targetY, 0);
            yield return null;
        }

        _Canvas.enabled = false;
    }
}
