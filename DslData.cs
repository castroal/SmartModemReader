using System.Text;

namespace SmartModemReader
{
    public class DslData
    {
        public bool IsUp { get; internal set; }
        public string UpTime { get; internal set; }
        public string DslType { get; internal set; }
        public string DslMode { get; internal set; }
        public float MaxLineRateUp { get; internal set; }
        public float MaxLineRateDown { get; internal set; }
        public float LineRateUp { get; internal set; }
        public float LineRateDown { get; internal set; }
        public float Uploaded { get; internal set; }
        public float Downloaded { get; internal set; }
        public float OutPowerUp { get; internal set; }
        public float OutPowerDown { get; internal set; }
        public float[] AttenuationUp { get; internal set; }
        public float[] AttenuationDown { get; internal set; }
        public float NoiseMarginUp { get; internal set; }
        public float NoiseMarginDown { get; internal set; }

        public string ToPrettyString()
        {
            var str = new StringBuilder();
            str.AppendLine("xDSL statistics:");
            str.AppendLine($"Link online:       {IsUp}");
            str.AppendLine($"Link uptime:       {UpTime}");
            str.AppendLine($"xDSL type:         {DslType}");
            str.AppendLine($"xDSL mode:         {DslMode}");
            str.AppendLine($"Max rate up:       {MaxLineRateUp}");
            str.AppendLine($"Max rate down:     {MaxLineRateDown}");
            str.AppendLine($"Rate up:           {LineRateUp}");
            str.AppendLine($"Rate down:         {LineRateDown}");
            str.AppendLine($"Uploaded:          {Uploaded}");
            str.AppendLine($"Downloaded:        {Downloaded}");
            str.AppendLine($"Out power up:      {OutPowerUp}");
            str.AppendLine($"Out power down:    {OutPowerDown}");
            str.AppendLine($"Attenuation up:    {string.Join(",", AttenuationUp)}");
            str.AppendLine($"Attenuation down:  {string.Join(",", AttenuationDown)}");
            str.AppendLine($"Noise margin up:   {NoiseMarginUp}");
            str.AppendLine($"Noise margin down: {NoiseMarginDown}");
            return str.ToString();
        }
    }
}