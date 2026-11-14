using UnityEngine;
using UnityEngine.UIElements;

namespace CosmicLander.UI
{
    [UxmlElement]
    public partial class SpeedGauge : VisualElement
    {
        readonly Label value;
        readonly Label status;
        readonly VisualElement ticks;
        readonly VisualElement zoneSafe,
            zoneCaution,
            zoneDanger;
        readonly VisualElement pointer,
            arrow;

        float max = 60f,
            safe = 20f,
            danger = 30f;

        public SpeedGauge()
        {
            AddToClassList("panel");
            AddToClassList("speed-gauge");

            var title = new Label("SPEED");
            title.AddToClassList("label-muted");
            Add(title);

            var reading = new VisualElement();
            reading.AddToClassList("speed-gauge__reading");
            Add(reading);

            value = new Label("0");
            value.AddToClassList("speed-gauge__value");
            reading.Add(value);

            var unit = new Label("km/h");
            unit.AddToClassList("speed-gauge__unit");
            reading.Add(unit);

            status = new Label("SAFE");
            status.AddToClassList("speed-gauge__status");
            Add(status);

            var body = new VisualElement();
            body.AddToClassList("speed-gauge__body");
            Add(body);

            ticks = new VisualElement();
            ticks.AddToClassList("speed-gauge__ticks");
            body.Add(ticks);

            var scale = new VisualElement();
            scale.AddToClassList("speed-gauge__scale");
            body.Add(scale);

            var zones = new VisualElement();
            zones.AddToClassList("speed-gauge__zones");
            scale.Add(zones);

            zoneSafe = MakeZone("speed-gauge__zone--safe", zones);
            zoneCaution = MakeZone("speed-gauge__zone--caution", zones);
            zoneDanger = MakeZone("speed-gauge__zone--danger", zones);

            pointer = new VisualElement();
            pointer.AddToClassList("speed-gauge__pointer");
            scale.Add(pointer);

            arrow = new VisualElement();
            arrow.AddToClassList("speed-gauge__arrow");
            arrow.generateVisualContent += DrawArrow;
            scale.Add(arrow);

            Configure(max, safe, danger);
            SetSpeed(0f);
        }

        static VisualElement MakeZone(string cls, VisualElement parent)
        {
            var z = new VisualElement();
            z.AddToClassList("speed-gauge__zone");
            z.AddToClassList(cls);
            parent.Add(z);
            return z;
        }

        static void DrawArrow(MeshGenerationContext ctx)
        {
            var p = ctx.painter2D;
            p.fillColor = Color.white;
            p.BeginPath();
            p.MoveTo(new Vector2(0f, 8f));
            p.LineTo(new Vector2(11f, 0f));
            p.LineTo(new Vector2(11f, 16f));
            p.ClosePath();
            p.Fill();
        }

        // Границы зон в км/ч: 0..safe безопасно, safe..danger осторожно, danger..max опасно
        public void Configure(float maxKmh, float safeKmh, float dangerKmh)
        {
            max = maxKmh;
            safe = safeKmh;
            danger = dangerKmh;

            SetZone(zoneSafe, 0f, safe);
            SetZone(zoneCaution, safe, danger);
            SetZone(zoneDanger, danger, max);

            ticks.Clear();
            foreach (float t in new[] { 0f, safe, danger, max })
            {
                var tick = new Label(Mathf.RoundToInt(t).ToString());
                tick.AddToClassList("speed-gauge__tick");
                tick.style.bottom = Length.Percent(t / max * 100f);
                ticks.Add(tick);
            }
        }

        void SetZone(VisualElement zone, float from, float to)
        {
            zone.style.bottom = Length.Percent(from / max * 100f);
            zone.style.height = Length.Percent((to - from) / max * 100f);
        }

        public void SetSpeed(float kmh)
        {
            kmh = Mathf.Clamp(kmh, 0f, max);
            Length pos = Length.Percent(kmh / max * 100f);
            pointer.style.bottom = pos;
            arrow.style.bottom = pos;
            value.text = Mathf.RoundToInt(kmh).ToString();

            bool isDanger = kmh > danger;
            bool isCaution = !isDanger && kmh > safe;
            EnableInClassList("speed--danger", isDanger);
            EnableInClassList("speed--caution", isCaution);
            EnableInClassList("speed--safe", !isDanger && !isCaution);
            status.text =
                isDanger ? "DANGER"
                : isCaution ? "CAUTION"
                : "SAFE";
        }
    }
}
