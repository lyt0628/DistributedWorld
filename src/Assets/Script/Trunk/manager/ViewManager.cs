

using GameLib;
using GameLib.View;
using QS.API;

class ViewManager : IGameManager
{
    public ManagerStatus Status =>ManagerStatus.Initialing;

    private IMessager _messager = new Messager();
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