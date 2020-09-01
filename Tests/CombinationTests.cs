using System.Linq;
using Foundation.Algorithms;
using NUnit.Framework;

namespace Mutiny.Grammar.Tests
{
    [TestOf(nameof(Combinations))]
    public static class CombinationTests
    {
        [Test]
        public static void Singleton()
        {
            var items = new[] {1};

            var combinations = Combinations.Generate(items).ToArray();
            
            Assert.That(combinations, Has.Length.EqualTo(2));
            Assert.That(combinations[0], Has.Length.EqualTo(0));
            Assert.That(combinations[1][0], Is.EqualTo(1));
        }

        [Test]
        public static void Combinations123()
        {
            var items = new[] {1, 2, 3};
            
            var combinations = Combinations.Generate(items).ToArray();
            
            Assert.That(combinations, Has.Length.EqualTo(8));
            Assert.IsTrue(ContainsCombination(combinations, new int[0]));
            Assert.IsTrue(ContainsCombination(combinations, 1));
            Assert.IsTrue(ContainsCombination(combinations, 2));
            Assert.IsTrue(ContainsCombination(combinations, 3));
            Assert.IsTrue(ContainsCombination(combinations, 1, 2));
            Assert.IsTrue(ContainsCombination(combinations, 1, 3));
            Assert.IsTrue(ContainsCombination(combinations, 2, 3));
            Assert.IsTrue(ContainsCombination(combinations, 1, 2, 3));
        }

        private static bool ContainsCombination(int[][] combinations, params int[] expectedCombination)
        {
            for (var i = 0; i < combinations.Length; i++)
            {
                var combination = combinations[i];
                if (combination.Length != expectedCombination.Length)
                    continue;

                var matched = true;
                for (var j = 0; j < combination.Length; j++)
                {
                    if (combination[j] == expectedCombination[j])
                        continue;
                    
                    matched = false;
                    break;
                }

                if (matched)
                    return true;
            }

            return false;
        }
    }
}