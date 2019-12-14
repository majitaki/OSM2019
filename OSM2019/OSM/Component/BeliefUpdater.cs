using MathNet.Numerics.LinearAlgebra;
using OSM2019.OSM;
using OSM2019.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019
{
  class BeliefUpdater
  {
    public double SensorWeight { get; private set; }
    public SensorWeightEnum SensorWeightMode { get; private set; }
    public BeliefUpdateFunctionEnum MyBeliefUpdateFunctionMode { get; private set; }

    public BeliefUpdater()
    {
      this.MyBeliefUpdateFunctionMode = BeliefUpdateFunctionEnum.Bayse;
    }
    public BeliefUpdater SetBeliefUpdateFunctionMode(BeliefUpdateFunctionEnum func_mode)
    {
      this.MyBeliefUpdateFunctionMode = func_mode;
      return this;
    }
    public BeliefUpdater SetSensorWeightMode(SensorWeightEnum sw_mode, double value = 0.0)
    {
      this.SensorWeightMode = sw_mode;
      switch (this.SensorWeightMode)
      {
        case SensorWeightEnum.DependSensorRate:
          break;
        case SensorWeightEnum.Custom:
          this.SensorWeight = value;
          break;
        case SensorWeightEnum.SameNoneSensor:
          break;
      }
      return this;
    }

    public Vector<double> UpdateBelief(OSMBase osm, Message message)
    {
      Vector<double> receive_op;
      var pre_belief = message.ToAgent.Belief;
      var weight = message.GetToWeight();

      if (message.ToAgent.IsSensor && message.ToAgent.IsDetermined() && osm.GetType() == typeof(OSM_Only)) return pre_belief;

      if (message.Subject.SubjectName != message.ToAgent.MySubject.SubjectName)
      {
        var to_subject = message.ToAgent.MySubject;
        receive_op = message.Subject.ConvertOpinionForSubject(message.Opinion, to_subject);
      }
      else
      {
        receive_op = message.Opinion.Clone();
      }


      Vector<double> updated_belief = null;

      if (message.FromAgent.AgentID < 0)
      {
        switch (this.SensorWeightMode)
        {
          case SensorWeightEnum.DependSensorRate:
            updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, osm.MyEnvManager.SensorWeight, receive_op, this.MyBeliefUpdateFunctionMode);
            break;
          case SensorWeightEnum.Custom:
            updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, this.SensorWeight, receive_op, this.MyBeliefUpdateFunctionMode);
            break;
          case SensorWeightEnum.SameNoneSensor:
            updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op, this.MyBeliefUpdateFunctionMode);
            break;
          case SensorWeightEnum.FollowEnvDistWeight:
            updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, osm.MyEnvManager.MyCustomDistribution.MyDistWeight, receive_op, this.MyBeliefUpdateFunctionMode);
            break;
          default:
            break;
        }
      }
      else
      {
        //updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op, this.MyBeliefUpdateFunctionMode);
        updated_belief = osm.MyAggFuncs.UpdateBelief(pre_belief, weight, receive_op, BeliefUpdateFunctionEnum.Bayse);
      }

      Debug.Assert(updated_belief != null);
      return updated_belief;
    }
  }
}
