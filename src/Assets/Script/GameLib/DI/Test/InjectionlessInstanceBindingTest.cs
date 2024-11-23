using NUnit.Framework;


namespace GameLib.DI.Test
{
    public class InjectionlessInstanceBindingTest
    {
        [Test]
        public void Instance_Binding_Got()
        {

            var instance = new Bean();

            var ctx = IDIContext.New();
            ctx.BindInstance(instance);

            var obj = ctx.GetInstance<Bean>(typeof(Bean));
            Assert.IsNotNull(obj);
        }
        class Bean { }



        [Test]
        public void Instance_Binding_Is_Sington()
        {
            var instance = new Bean();
            var ctx = IDIContext.New();
            ctx.BindInstance(instance);
            var obj1 = ctx.GetInstance<Bean>(instance.GetType());
            var obj2 = ctx.GetInstance<Bean>(instance.GetType());

            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreEqual(obj1, obj2);
        }

        [Test]
        public void Nameless_Instance_Bindings_Resoved_By_Priority()
        {
            var instance1 = new Bean();
            var instance2 = new Bean();
            var ctx = IDIContext.New();
            try
            {
                ctx.BindInstance(instance1);
                ctx.BindInstance(instance2);
                ctx.GetInstance<Bean>(typeof(Bean));
            }
            catch (DIException e)
            {
                Assert.IsTrue(
                    e.Message.Contains("Same Priority Type was Found: "));
                return;
            }
            InjectionlessConstructorBindingTest.Failed();
        }


        [Test]
        public void Named_Instance_Bindings_Resolved_By_Name()
        {
            var instance1 = new Bean();
            var instance2 = new Bean();
            var ctx = IDIContext.New();
            ctx.BindInstance("bean1", instance1)
                .BindInstance("bean2", instance2);

            var bean1 = ctx.GetInstance<Bean>("bean1", typeof(Bean));
            Assert.AreEqual(instance1, bean1);

            var bean2 = ctx.GetInstance<Bean>("bean2", typeof(Bean));
            Assert.AreEqual(instance2, bean2);
        }

        [Test]
        public void Nameless_GetBinding_Would_Not_Resolve_By_Name()
        {
            var instance1 = new Bean();
            var instance2 = new Bean();
            var ctx = IDIContext.New();
            ctx.BindInstance("bean1", instance1)
                .BindInstance("bean2", instance2);
            try
            {
                ctx.GetInstance<Bean>(typeof(Bean));
            }
            catch (DIException e)
            {
                Assert.IsTrue(
                    e.Message.Contains("Same Priority Type was Found: "));
                return;
            }
            InjectionlessConstructorBindingTest.Failed();
        }
    }

}
