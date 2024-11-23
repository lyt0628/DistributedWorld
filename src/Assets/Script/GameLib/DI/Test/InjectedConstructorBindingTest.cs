using NUnit.Framework;



namespace GameLib.DI.Test
{

    class A
    {
    }
    class B
    {
        [Injected]
        public A a;
    }
    class C
    {
        [Injected]
        public A a;
    }

    interface IAnim { }

    class Cat : IAnim { }
    class CatHolder
    {
        [Injected]
        public IAnim cat;
    }

    class Dog : IAnim { }

    class DogHolder
    {
        [Injected(Name = "Dog")]

        public IAnim dog;
    }

    interface IKing { }

    [Priority(Value = 1)]
    class RedKing : IKing { }
    class BlackKing : IKing { }
    class Kingdom
    {
        [Injected]
        public IKing king;
    }

    class InjectedConstructorBindingTest
    {
        [Test]
        public void Concrete_Class_Injection_Works()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(A))
                .Bind(typeof(B));

            var b = ctx.GetInstance<B>(typeof(B));
            Assert.IsNotNull(b);
            Assert.IsNotNull(b.a);
        }

        [Test]
        public void Interface_Injected_By_Drivered_Class()
        {
            var ctx = IDIContext.New();

            ctx.Bind(typeof(Cat))
                .Bind(typeof(CatHolder));

            var holder = ctx.GetInstance<CatHolder>(typeof(CatHolder));
            Assert.IsNotNull(holder);
            Assert.IsNotNull(holder.cat);
        }

        [Test]
        public void Injection_Resolved_By_Name()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(Cat))
                .Bind(typeof(Dog))
                .Bind(typeof(DogHolder));
            var holder = ctx.GetInstance<DogHolder>(typeof(DogHolder));
            Assert.IsNotNull(holder);
            Assert.IsNotNull(holder.dog);
            Assert.IsTrue(typeof(Dog) == holder.dog.GetType());
        }

        [Test]
        public void Injecion_Resolved_By_Priority()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(RedKing))
                .Bind(typeof(BlackKing))
                .Bind(typeof(Kingdom));

            var kingdom = ctx.GetInstance<Kingdom>(typeof(Kingdom));
            Assert.IsNotNull(kingdom);
            Assert.IsTrue(typeof(RedKing) == kingdom.king.GetType());
        }


        class Head
        {
            [Injected]
            public Tail tail;
        }
        class Tail
        {
            [Injected]
            public Head head;
        }

        [Test]
        public void Circur_Dependencies_Can_Be_Sloved()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(Head))
            .Bind(typeof(Tail));

            var head = ctx.GetInstance<Head>(typeof(Head));
            Assert.IsNotNull(head);
            Assert.IsNotNull(head.tail);
            var tail = ctx.GetInstance<Tail>(typeof(Tail));
            Assert.IsNotNull(tail.head);

        }

        [Test]
        public void SetterInjection_Can_Be_Scoped()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(A))
                .Bind(typeof(B))
                .Bind(typeof(C));
            var b = ctx.GetInstance<B>(typeof(B));
            var c = ctx.GetInstance<C>(typeof(C));
            Assert.AreEqual(b.a, c.a);
        }



        class D
        {
            [Injected]
            public A a1;
            [Injected]
            public A a2;
        }
        [Test]
        public void Explicit_Scope_Bind_Works()
        {
            var ctx = IDIContext.New();
            ctx.Bind(typeof(A), ScopeFlag.Prototype)
                .Bind(typeof(D));
            var d = ctx.GetInstance<D>(typeof(D));
            Assert.IsNotNull(d);
            Assert.IsNotNull(d.a1);
            Assert.IsNotNull(d.a2);
            Assert.AreNotEqual(d.a1, d.a2);
        }


    }
}