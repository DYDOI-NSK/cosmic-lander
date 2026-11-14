using System.Globalization;
using UnityEngine.UIElements;

namespace CosmicLander.UI
{
    [UxmlElement]
    public partial class ScoreLabel : VisualElement
    {
        static readonly NumberFormatInfo Format = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
        };

        readonly Label value;

        public ScoreLabel()
        {
            AddToClassList("score-label");

            var title = new Label("SCORE");
            title.AddToClassList("label-muted");
            Add(title);

            value = new Label("0");
            value.AddToClassList("score-label__value");
            Add(value);
        }

        public void SetValue(int score)
        {
            value.text = score.ToString("N0", Format);
        }
    }
}
