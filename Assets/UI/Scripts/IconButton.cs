using System;
using UnityEngine.UIElements;

namespace CosmicLander.UI
{
    [UxmlElement]
    public partial class IconButton : VisualElement
    {
        public event Action Clicked;

        public IconButton()
        {
            AddToClassList("icon-button");

            var face = new VisualElement { pickingMode = PickingMode.Ignore };
            face.AddToClassList("icon-button__face");
            Add(face);

            var icon = new VisualElement { pickingMode = PickingMode.Ignore };
            icon.AddToClassList("icon-button__icon");
            face.Add(icon);

            this.AddManipulator(new Clickable(() => Clicked?.Invoke()));
        }
    }
}
