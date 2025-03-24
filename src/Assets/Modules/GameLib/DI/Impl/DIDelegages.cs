


namespace GameLib.DI
{

    delegate object Builder();
    delegate object Construct(params object[] args);
    delegate IBinding BindingLookup(Key key);
    delegate void Injector(object instance);
    delegate void SpecSetter(object instance, object value);

}