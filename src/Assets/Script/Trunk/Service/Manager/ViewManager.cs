using GameLib.Pattern.Message;
using GameLib.View;
using QS.Api;

class ViewManager : IGameManager
{
    public ManagerStatus Status => ManagerStatus.Initialing;

    private readonly IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private IView hpView;

    public void Startup()
    {

        hpView = new HpView();

        hpView.Preload();

    }

    public void Update()
    {
        hpView.OnUpdate();
    }
}