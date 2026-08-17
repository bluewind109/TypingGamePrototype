using System;
using System.Collections.Generic;
using UnityEngine;

public class SentenceManager : MonoBehaviour
{
	public Action<List<Skill>> onSkillsTyped;

	[Header("Components")]
	[SerializeField] private Transform _sentenceContainer;
	[SerializeField] private SentenceDisplay _sentenceDisplayPrefab;
	[SerializeField] private ActiveSentenceDisplay _typedSentenceDisplay;
	[SerializeField] private WordInput _wordInput;

	private List<Sentence> _sentences = new List<Sentence>();
	private List<Skill> _availableSkills;
	private TypedSentence _typedSentence;

	void Start()
	{
		_wordInput.LetterTyped += OnLetterTyped;
		_wordInput.BackspaceTyped += OnBackspaceTyped;
		_wordInput.EnterTyped += OnEnterTyped;
	}

	public void Initialize(List<Skill> availableSkills)
	{
		_availableSkills = availableSkills;
		foreach (Skill skill in _availableSkills)
		{
			Sentence sentence = new Sentence(
				skill.Name,
				Instantiate(_sentenceDisplayPrefab, _sentenceContainer)
			);

			_sentences.Add(sentence);
		}

		_typedSentence = new TypedSentence(_typedSentenceDisplay);
	}

	public void ToggleInput(bool enabled)
	{
		_wordInput.ToggleInput(enabled);
	}

	public void UpdateGameplay()
	{
		_wordInput.UpdateInput();
	}

	public void ResetTypedSentence()
	{
		_typedSentence.Clear();
	}

	private void OnLetterTyped(char letter)
	{
		_typedSentence.AddLetter(letter);
	}

	private void OnBackspaceTyped()
	{
		_typedSentence.RemoveLastLetter();
	}

	private void OnEnterTyped()
	{
		// Check if typed sentence matches any available action
		CheckTypedSentence();

	}

	/// <summary>
	/// - Split the sentence into words and check each word against the available actions.<br/>
	/// - Checks if the typed sentence matches any of the available actions.<br/>
	/// - If matched, remove the matched word from the typed sentence and add to the found actions list.<br/>
	/// - Continue to check the remaining typed sentence for more matches until no more matches are found.<br/>
	/// - Pass the found actions list to the onActionsTyped event for further processing.<br/>
	/// - Clear the typed sentence after processing.
	/// </summary>
	private void CheckTypedSentence()
	{
		List<string> typedWords = _typedSentence.GetWords();
		Debug.Log($"Typed words: <color=green>{string.Join(", ", typedWords)}</color>");

		List<Skill> foundSkills = new List<Skill>();
		foreach (string typedWord in typedWords)
		{
			Skill matchedSkill = FindMatchedSkill(typedWord);
			if (matchedSkill == null) continue;

			// TODO If matched, remove the matched word from the displayed typed sentence
			// _typedSentence.RemoveWord(typedWord);

			foundSkills.Add(matchedSkill);
		}

		onSkillsTyped?.Invoke(foundSkills);
		ResetTypedSentence();
	}

	private Skill FindMatchedSkill(string typedWord)
	{
		if (string.IsNullOrEmpty(typedWord)) return null;

		foreach (Skill skill in _availableSkills)
		{
			if (string.Equals(typedWord, skill.Name, StringComparison.OrdinalIgnoreCase))
			{
				Debug.Log($"Found matching action: <color=green>{skill.Name}</color>");
				return skill;
			}
		}

		Debug.Log($"No matching action found for typed word: <color=red>{typedWord}</color>");
		return null;
	}
}

