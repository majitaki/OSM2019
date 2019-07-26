using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
  [JsonObject]
  public class ExperimentSetting
  {
    [JsonProperty("round")]
    public int Round { get; set; }
    [JsonProperty("step")]
    public int Step { get; set; }
    [JsonProperty("opinion_intro_interval")]
    public int OpinionIntroInteval { get; set; }
    [JsonProperty("opinion_intro_rate")]
    public double OpinionIntroRate { get; set; }
    [JsonProperty("belief_updater")]
    public string BeliefUpdater { get; set; }
    [JsonProperty("sensor_weight_mode")]
    public string SensorWeightMode { get; set; }
    [JsonProperty("graph_type")]
    public string GraphType { get; set; }
    public int NetworkSize { get; set; }
    [JsonProperty("sensor_size")]
    public int SensorSize { get; set; }
    [JsonProperty("algorithm")]
    public string Algorithm { get; set; }
    [JsonProperty("dim")]
    public int Dim { get; set; }
    [JsonProperty("env_dist_mode")]
    public string EnvDistMode { get; set; }
    [JsonProperty("sensor_rate")]
    public double SensorRate { get; set; }
    [JsonProperty("sensor_size_mode")]
    public string SensorSizeMode { get; set; }
    [JsonProperty("target_awareness")]
    public double TargetAwareness { get; set; }
    [JsonProperty("env_dist_weight")]
    public double EnvDistWeight { get; set; }
    [JsonProperty("sample_size")]
    public int SampleSize { get; set; }
    [JsonProperty("awa_rate_window_size")]
    public int AwaRateWindowSize { get; set; }
    [JsonProperty("common_curiocity")]
    public double CommonCuriocity { get; set; }
    [JsonProperty("is_dynamic")]
    public bool IsDynamic { get; set; }
    [JsonProperty("info_weight_rate")]
    public double InfoWeightRate { get; set; }
  }
}
