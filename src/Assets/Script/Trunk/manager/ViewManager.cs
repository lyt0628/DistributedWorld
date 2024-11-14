

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
    private IView _mainView;
    private IView _inventoryView;
    //private IViewNode _tabView;


    public void Startup()
    {

        imageView = new ImageView();
        hpView = new HpView();
        _mainView = new MainView();
        //_tabView = new TabView();
        _inventoryView = new InventoryView();

        imageView.Preload();
        hpView.Preload();
        _mainView.Preload();
        //_tabView.Preload();
        _inventoryView.Preload();

        //_tabView.Show();
        _inventoryView.OnInit();
    }

    public void Update()
    {
        if (imageView.Initialed) { imageView.OnUpdate(); }
        if(hpView.Initialed) { hpView.OnUpdate(); }
        if (_mainView.Initialed) { _mainView.OnUpdate(); }
        //if (_tabView.Initialed) { _tabView.OnUpdate(); }
        if(_inventoryView.Initialed){ _inventoryView.OnUpdate(); }
    }
}