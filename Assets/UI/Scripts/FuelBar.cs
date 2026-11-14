using UnityEngine;
using UnityEngine.UIElements;

namespace CosmicLander.UI
{
    [UxmlElement]
    public partial class FuelBar : VisualElement
    {
        const int SegmentCount = 10;
        const int LowFuelPercent = 25;

        readonly VisualElement[] segments = new VisualElement[SegmentCount];
        readonly Label value;

        public FuelBar()
        {
            AddToClassList("panel");
            AddToClassList("fuel-bar");

            var header = new VisualElement();
            header.AddToClassList("fuel-bar__header");
            Add(header);

            var icon = new VisualElement();
            icon.AddToClassList("fuel-bar__icon");
            header.Add(icon);

            var title = new Label("FUEL");
            title.AddToClassList("label-muted");
            title.AddToClassList("fuel-bar__title");
            header.Add(title);

            value = new Label("0%");
            value.AddToClassList("fuel-bar__value");
            header.Add(value);

            var row = new VisualElement();
            row.AddToClassList("fuel-bar__segments");
            Add(row);

            for (int i = 0; i < SegmentCount; i++)
            {
                var seg = new VisualElement();
                seg.AddToClassList("fuel-bar__segment");
                if (i > 0)
                    seg.style.marginLeft = 4;
                row.Add(seg);
                segments[i] = seg;
            }

            var warning = new Label("LOW FUEL - LAND NOW");
            warning.AddToClassList("fuel-bar__warning");
            Add(warning);
        }

        public void SetValue(float normalized)
        {
            int percent = Mathf.RoundToInt(Mathf.Clamp01(normalized) * 100f);
            int filled = Mathf.CeilToInt(percent / 10f);

            for (int i = 0; i < SegmentCount; i++)
                segments[i].EnableInClassList("is-on", i < filled);

            value.text = percent + "%";
            EnableInClassList("fuel-bar--low", percent <= LowFuelPercent);
        }
    }
}
