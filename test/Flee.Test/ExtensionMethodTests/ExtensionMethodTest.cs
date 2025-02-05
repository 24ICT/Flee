using NUnit.Framework;

namespace Flee.ExtensionMethodTests
{
    using Flee.ExtensionMethodTests.ExtensionMethodTestData;
    using global::Flee.PublicTypes;
    using global::Flee.Test.Infrastructure;

    /// <summary>The extension method test.</summary>
    [TestFixture]
    public class ExtensionMethodTest : ExpressionTests
    {
        [Test]
        public void TestExtensionMethodCallOnOwner()
        {
            var result = GetExpressionContext().CompileDynamic("SayHello()").Evaluate();
            Assert.That("Hello World", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnProperty()
        {
            var result = GetExpressionContext().CompileDynamic("Sub.SayHello()").Evaluate();
            Assert.That("Hello SubWorld", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnOwnerWithArguments()
        {
            var result = GetExpressionContext().CompileDynamic("SayHello(\"!!!\")").Evaluate();
            Assert.That("Hello World!!!", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnOwnerWithArgumentsOnOverload()
        {
            var result = GetExpressionContext().CompileDynamic("SayHello(true)").Evaluate();
            Assert.That("Hello dear World", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnOwnerWithArgumentsOnClassOverload()
        {
            var result = GetExpressionContext().CompileDynamic("SayHello(2)").Evaluate();
            Assert.That("hello hello World", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnPropertyWithArguments()
        {
            var result = GetExpressionContext().CompileDynamic("Sub.SayHello(\"!!!\")").Evaluate();
            Assert.That("Hello SubWorld!!!", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnPropertyWithArgumentsOnClassOverload()
        {
            var result = GetExpressionContext().CompileDynamic("Sub.SayHello(2)").Evaluate();
            Assert.That("hello hello SubWorld", Is.EqualTo(result));
        }

        [Test]
        public void TestExtensionMethodCallOnPropertyWithArgumentsOnOverload()
        {
            var result = GetExpressionContext().CompileDynamic("Sub.SayHello(\"!!!\")").Evaluate();
            Assert.That("Hello SubWorld!!!", Is.EqualTo(result));
        }

        /// <summary>
        /// check that methods are not ambiguous.
        /// </summary>
        [Test]
        public void TestExtensionMethodMatchArguments()
        {
            var result = GetExpressionContext().CompileDynamic("MatchParams(1, 2.3f, 2.3)").Evaluate();
            Assert.That("FFD", Is.EqualTo(result));
            result = GetExpressionContext().CompileDynamic("MatchParams(3.4,4.4,2.3)").Evaluate();
            Assert.That("DDD", Is.EqualTo(result));
            result = GetExpressionContext().CompileDynamic("MatchParams(1,2,3)").Evaluate();
            Assert.That("III", Is.EqualTo(result));
            result = GetExpressionContext().CompileDynamic("MatchParams(1u,2,3)").Evaluate();
            Assert.That("UII", Is.EqualTo(result));
        }


        private static ExpressionContext GetExpressionContext()
        {
            var expressionOwner = new TestData { Id = "World" };
            var context = new ExpressionContext(expressionOwner);
            context.Imports.AddType(typeof(TestDataExtensions));
            context.ParserOptions.FunctionArgumentSeparator = ',';
            context.ParserOptions.DecimalSeparator = '.';
            context.ParserOptions.RecreateParser();
            return context;
        }
    }
}
