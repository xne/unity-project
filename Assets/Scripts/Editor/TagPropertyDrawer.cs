using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TagAttribute))]
class TagPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.String)
            property.stringValue = EditorGUI.TagField(position, label, property.stringValue);

        EditorGUI.EndProperty();
    }
}
