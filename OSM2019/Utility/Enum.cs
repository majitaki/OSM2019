using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSM2019.Utility
{
    enum GUIEnum
    {
        AgentGUI,
        AnimationGUI,
        ExperimentGUI,
        LearningGUI,
        MainFormGUI,
        GraphGUI,
    }

    enum GraphEnum
    {
        WS,
        NewmanWS,
        ConnectedWS,
        BA,
        FastGnp,
        GnpRandom,
        DenseGnm,
        Gnm,
        ER,
        Binomial,
        RandomRegular,
        PowerLawCluster,
        RandomKernel,
        RandomLobster,
        Grid2D,
        Hexagonal,
        Triangular,
        Custom,
        Void
    }

    enum LayoutEnum
    {
        Circular,
        FruchtermanReingold,
        KamadaKawai,
        Random,
        Shell,
        Spectral,
        Spring,
        Square,
        Null
    }


    enum SeedEnum
    {
        AgentGenerateSeed,
        PlayStepSeed,
    }

    enum InitBeliefMode
    {
        NormalNarrow,
        Normal,
        NormalWide,
        NoRandom
    }

    enum CalcWeightMode
    {
        FavorMyOpinion,
        Equality
    }

    enum SampleAgentSetMode
    {
        RandomSetRate,
        RemainSet
    }

    enum AlgoEnum
    {
        AAT,
        AATG,
        AATfix,
        OSMonly,
        IWTori,
        IWTorionly,
        AATparticle,
        AATwindow,
        AATwindowparticle,
        AATfunction,
        AATfunctionparticle,
        AATfunctioniwt,
        AATinfo,
        AATinfostep
    }

    enum SensorWeightEnum
    {
        DependSensorRate,
        Custom,
        SameNoneSensor
    }

    enum BeliefUpdateFunctionEnum
    {
        Bayse,
        Particle
    }

    enum EnvDistributionEnum
    {
        Turara,
        Exponential

    }
}
