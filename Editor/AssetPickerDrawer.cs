using UnityEditor;
using UnityEngine;
using UnityPickers.Utility;

namespace UnityPickers
{
	[CustomPropertyDrawer(typeof(AssetPickerAttribute))]
	[CustomPropertyDrawer(typeof(ScriptableObject), true)]
	public class AssetPickerDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var assetType = fieldInfo.FieldType;
			if (assetType.IsUnityCollection())
			{
				var tempType = assetType.GetElementType();
				if (tempType == null)
                {
					System.Type[] genericTypes = assetType.GetGenericArguments();
                    if (genericTypes.Length > 0)
                    {
                        assetType = genericTypes[0];
                    }
                    else
                    {
						assetType = null;
                    }
                }
				else
                {
					assetType = tempType;
                }
			}

			if (assetType == null)
				return;

			var a = fieldInfo.GetAttribute<AssetPickerAttribute>();

			AssetPicker.PropertyField(
				position, property, fieldInfo,
				label, assetType,
				he => a == null || he.Path.Contains(a.Path)
			);
		}

	}
}