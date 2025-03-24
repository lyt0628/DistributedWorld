using NUnit.Framework;

//namespace GameLib.DI.Test {
namespace GameLib.DI.Test
{
    class InjectionlessConstructorBindingTest
    {
        public static void Failed()
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void Default_Constructor_Binding_With_Sington_Scope()
        {
            var ctx = IDIContext.New();
            ctx.Bind<Sington>();

            var obj1 = ctx.GetInstance<Sington>();
            var obj2 = ctx.GetInstance<Sington>();

            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreEqual(obj1, obj2);
        }

        class Sington { }

        [Test]
        public void Constructor_Binding_With_Prototype_Scope_Works()
        {
            var ctx = IDIContext.New();
            ctx.Bind<Proto>();

            var obj1 = ctx.GetInstance<Proto>();
            var obj2 = ctx.GetInstance<Proto>();

            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);

            Assert.AreNotEqual(obj1, obj2);
        }

        [Scope(Value = ScopeFlag.Prototype)]
        [Injected]
        class Proto { }

        class NoEmptyCtor
        {
            public NoEmptyCtor(string arg) { }
        }
        [Test]
        public void EmptyConstructor_Is_Needed()
        {

            var ctx = IDIContext.New();
            try
            {
                ctx.Bind<NoEmptyCtor>();
            }
            catch (DIException e)
            {
                Assert.IsTrue(e.Message.Contains("No Empty Constructor Found for: "));
                return;
            }
            Failed();

        }

        [Test]
        public void Bind_Is_Reenterable()
        {
            var ctx = IDIContext.New();
            ctx.Bind<Sington>()
                .Bind<Sington>();
            var instance = ctx.GetInstance<Sington>();
            Assert.IsNotNull(instance);
        }

    }
}


