

using GameLib;
using GameLib.View;
using QS.API;

class ViewManager : IGameManager
{
    public ManagerStatus Status =>ManagerStatus.Initialing;

    private IMessager _messager = new Messager();
    public IMessager Messager => _messager;

    private IView imageView;
    private IView hpView;
    public void Startup()
    {

        imageView = new ImageView();
        hpView = new HpView();

        imageView.Preload();
        hpView.Preload();

        imageView.OnInit();
        hpView.OnInit();
    }

    public void Update()
    {
        imageView.OnUpdate();
        hpView.OnUpdate();
    }
}