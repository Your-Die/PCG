namespace Mutiny.Grammar.Tests
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    public class GrammarTreeTests
    {
        [Test]
        public void FlatTree()
        {
            var tokens = new IToken[]
            {
                new Terminal("I "),
                new Terminal("like "),
                new Terminal("Moles")
            };

            var tree = BuildSingleLayerTree(tokens);

            for (var index = 0; index < tokens.Length; index++)
            {
                var token = tokens[index];
                var node = tree.RootLayer[index];    

                var terminalNode = node as TerminalNode;
                
                Assert.That(terminalNode, Is.Not.Null);
                Assert.That(terminalNode.Token, Is.EqualTo(token));
            }
        }

        [Test]
        public void OpenNodeTest()
        {
            var animalToken = new NonTerminal("animal")
                .AddReplacement("mole")
                .AddReplacement("dog");

            var originToken = new NonTerminal(Constants.OriginSymbol)
                .AddReplacement(
                    new Terminal("I "),
                    new Terminal("like "),
                    animalToken);
            
            var animalRule = new GrammarRule(animalToken);
            var originRule= new GrammarRule(originToken);
            
            var tree = new GrammarTree(new Grammar(originRule, animalRule));

            var expectedReplacement = originToken.Replacements.First();
            Assert.That(tree.RootLayer, Has.Count.EqualTo(expectedReplacement.Tokens.Count));
            Assert.That(tree.RootLayer[2], Is.TypeOf<OpenNode>());
        }


        [Test]
        public void CloseNodeTest()
        {
            // Arrange
            var animalToken = new NonTerminal("animal")
                .AddReplacement("dog");
            
            var originToken = new NonTerminal(Constants.OriginSymbol)
                .AddReplacement(animalToken);
            
            var animalRule = new GrammarRule(animalToken);
            var originRule = new GrammarRule(originToken);
            
            var grammar = new Grammar(originRule, animalRule);
            var tree = new GrammarTree(grammar);

            // Act
            tree.SetReplacement(0, 0);

            // Assert
            Assert.That(tree.RootLayer, Has.Count.EqualTo(1));
            var node = tree.RootLayer.First();

            Assert.That(node, Is.TypeOf<ClosedNode>());
        }

        [Test]
        public void NodeClosedEvent()
        {
            // Arrange
            var animalToken = new NonTerminal("animal")
                .AddReplacement("dog");
            
            var originToken = new NonTerminal(Constants.OriginSymbol)
                .AddReplacement(animalToken);
            
            var animalRule = new GrammarRule(animalToken);
            var originRule = new GrammarRule(originToken);
            
            var grammar = new Grammar(originRule, animalRule);
            var tree = new GrammarTree(grammar);

            var eventInvoked = false;
            tree.NodeClosed += _ => eventInvoked = true;
            tree.SetReplacement(0, 0);
            
            Assert.IsTrue(eventInvoked);
        }
        
        [Test]
        public void CloseTerminalNodeTest()
        {
            var tree = BuildSingleLayerTree(new Terminal("I "));
            Assert.Throws<ArgumentException>(() => tree.SetReplacement(0, 0));
        }
        

        private static GrammarTree BuildSingleLayerTree(params IToken[] tokens)
        {
            var symbol = new NonTerminal(Constants.OriginSymbol).AddReplacement(tokens);
            var rule = new GrammarRule(symbol);
            var grammar = new Grammar(rule);
            
            return new GrammarTree(grammar);
        }
    }
}