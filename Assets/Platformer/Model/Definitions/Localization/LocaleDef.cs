using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Platformer.Model.Definitions.Localization
{
	[CreateAssetMenu(menuName = "Defs/LocaleDef", fileName = "LocalDef")]
	public class LocaleDef : ScriptableObject
	{



		[SerializeField] private string _url;
		[SerializeField] private string _fileName;
		[SerializeField] private List<LocaleItem> _localeItems;

		private UnityWebRequest _request;

		public Dictionary<string, string> GetData()
		{

			var dictionary = new Dictionary<string, string>();
			foreach (var localeItem in _localeItems)
			{
				dictionary.Add(localeItem.Key, localeItem.Value);
			}
			return dictionary;
		}

		[ContextMenu("Update locale from URL")]
		public void UpdateLocaleFromURL()
		{
			if (_request != null) return;

			_request = UnityWebRequest.Get(_url);

			_request.SendWebRequest().completed += OnWebDataLoaded;
		}

#if UNITY_EDITOR
		[ContextMenu("Update locale From Resources")]
		public void UpdateLocaleFromResources()
		{
			var path = UnityEditor.EditorUtility.OpenFilePanel("Choose locale file","","tsv");
		
			if(path.Length!=0)
			{
				var data = File.ReadAllText(path);
				ParseData(data);
			}
			//var file = Resources.Load<TextAsset>($"Locales/LocaleFiles/{_fileName}");
			
			//var rows = file.text.Split('\n');
			//_localeItems.Clear();
			//foreach (var row in rows)
			//{
			//	AddLocaleItem(row);
			//}
		}
#endif

		private void OnWebDataLoaded(AsyncOperation operation)
		{
			if (operation.isDone)
			{
				var data = _request.downloadHandler.text;
				ParseData(data);

				_request = null;
			}

		}

		private void ParseData(string data)
		{
			var rows = data.Split('\n');
			_localeItems.Clear();
			foreach (var row in rows)
			{
				AddLocaleItem(row);
			}
		}

		private void AddLocaleItem(string row)
		{
			try
			{
				var parts = row.Split('\t');
				_localeItems.Add(new LocaleItem { Key = parts[0], Value = parts[1] });
			}
			catch (Exception e)
			{
				Debug.LogError($"Can't parse row: {row}.\n{e}");
			}
		}

		[Serializable]
		private class LocaleItem
		{
			public string Key;
			public string Value;
		}


	}
}