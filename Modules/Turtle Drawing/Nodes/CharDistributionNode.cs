namespace Text
{
    using Chinchillada.Distributions;
    using Generators;
    using Sirenix.Serialization;

    public class CharDistributionNode : GeneratorNode<int>
    {
        [OdinSerialize] private IDistribution<int> distribution;
        
        protected override int GenerateInternal() => this.distribution.Sample(this.Random);
    }
}